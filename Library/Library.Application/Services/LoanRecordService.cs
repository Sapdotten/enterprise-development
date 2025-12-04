using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

/// <summary>
/// Application service for managing loan record operations.
/// </summary>
public class LoanRecordService(
    IRepository<LoanRecord, int> loanRecordRepository,
    IMapper mapper,
    IRepository<Book, int> bookRepository,
    IRepository<Reader, int> readerRepository
) : ILoanRecordService
{
    /// <summary>
    /// Creates a new loan record from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing loan record data. Must not be null.</param>
    /// <returns>The created loan record as a LoanRecordGetDTO.</returns>
    public async Task<LoanRecordGetDto> CreateAsync(LoanRecordCreateDto dto)
    {
        var newLoanRecord = mapper.Map<LoanRecord>(dto);
        await loanRecordRepository.CreateAsync(newLoanRecord);
        return mapper.Map<LoanRecordGetDto>(newLoanRecord);
    }

    /// <summary>
    /// Retrieves a loan record by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The ID of the loan record to retrieve.</param>
    /// <returns>The loan record as a LoanRecordGetDTO if found.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public async Task<LoanRecordGetDto> GetAsync(int dtoId)
    {
        var loanRecord = await loanRecordRepository.ReadAsync(dtoId)
            ?? throw new InvalidOperationException($"Loan record with {dtoId} was not found");

        return mapper.Map<LoanRecordGetDto>(loanRecord);
    }

    /// <summary>
    /// Retrieves a list of all loan records.
    /// </summary>
    /// <returns>A list of all loan records represented as LoanRecordGetDTOs. Returns empty list if none exist.</returns>
    public async Task<List<LoanRecordGetDto>> GetAllAsync()
    {
        var loanRecords = await loanRecordRepository.ReadAllAsync();
        return mapper.Map<List<LoanRecordGetDto>>(loanRecords);
    }

    /// <summary>
    /// Updates an existing loan record with data from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated loan record data. Must not be null.</param>
    /// <param name="dtoId">The ID of the loan record to update.</param>
    /// <returns>The updated loan record as a LoanRecordGetDTO.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public async Task<LoanRecordGetDto> UpdateAsync(LoanRecordCreateDto dto, int dtoId)
    {
        var toUpdateLoanRecord = await loanRecordRepository.ReadAsync(dtoId)
            ?? throw new InvalidOperationException($"Loan record with ID {dtoId} was not found for updating");

        mapper.Map(dto, toUpdateLoanRecord);
        await loanRecordRepository.UpdateAsync(toUpdateLoanRecord);

        return mapper.Map<LoanRecordGetDto>(toUpdateLoanRecord);
    }

    /// <summary>
    /// Deletes a loan record identified by its unique ID.
    /// </summary>
    /// <param name="dtoId">The ID of the loan record to delete.</param>
    /// <returns>True, if success, False when no book exists with the given ID.</returns>
    public async Task<bool> DeleteAsync(int dtoId)
    {
        return await loanRecordRepository.DeleteAsync(dtoId);
    }

    /// <summary>
    /// Asynchronously processes a batch of loan record contracts.
    /// Validates the existence of associated books and readers, maps each DTO to an entity,
    /// and persists the records in the data store. Throws if any referenced entity is missing.
    /// </summary>
    /// <param name="dtos">List of LoanRecordCreateDto to process. Must not be null.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a referenced book or reader does not exist.</exception>
    public async Task ReceiveContractAsync(IList<LoanRecordCreateDto> dtos)
    {
        foreach (var dto in dtos)
        {
            var book = await bookRepository.ReadAsync(dto.BookId)
                ?? throw new InvalidOperationException($"Книга с ID {dto.BookId} не найдена.");

            var reader = await readerRepository.ReadAsync(dto.ReaderId)
                ?? throw new InvalidOperationException($"Читатель с ID {dto.ReaderId} не найден.");

            var loanRecord = mapper.Map<LoanRecord>(dto);
            loanRecord.BookId = book.Id;
            loanRecord.ReaderId = reader.Id;

            await loanRecordRepository.CreateAsync(loanRecord);
        }
    }
}
