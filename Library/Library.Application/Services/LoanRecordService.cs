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
    IRepository<LoanRecord, int> loanRecordRepository, IMapper mapper
    ) : IApplicationService<LoanRecordGetDto, LoanRecordCreateDto, int>
{
    /// <summary>
    /// Creates a new loan record from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing loan record data. Must not be null.</param>
    /// <returns>The created loan record as a LoanRecordGetDTO.</returns>
    public LoanRecordGetDto Create(LoanRecordCreateDto dto)
    {
        var newLoanRecord = mapper.Map<LoanRecord>(dto);
        loanRecordRepository.Create(newLoanRecord);
        return mapper.Map<LoanRecordGetDto>(newLoanRecord);
    }

    /// <summary>
    /// Retrieves a loan record by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The ID of the loan record to retrieve.</param>
    /// <returns>The loan record as a LoanRecordGetDTO if found.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public LoanRecordGetDto Get(int dtoId)
    {
        var loanRecord = loanRecordRepository.Read(dtoId) ?? throw new InvalidOperationException($"Loan record with {dtoId} was not found");
        return mapper.Map<LoanRecordGetDto>(loanRecord);
    }

    /// <summary>
    /// Retrieves a list of all loan records.
    /// </summary>
    /// <returns>A list of all loan records represented as LoanRecordGetDTOs. Returns empty list if none exist.</returns>
    public List<LoanRecordGetDto> GetAll()
    {
        var loanRecords = loanRecordRepository.ReadAll();
        return mapper.Map<List<LoanRecordGetDto>>(loanRecords);
    }

    /// <summary>
    /// Updates an existing loan record with data from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated loan record data. Must not be null.</param>
    /// <param name="dtoId">The ID of the loan record to update.</param>
    /// <returns>The updated loan record as a LoanRecordGetDTO.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public LoanRecordGetDto Update(LoanRecordCreateDto dto, int dtoId)
    {
        var toUpdateLoanRecord = loanRecordRepository.Read(dtoId) ?? throw new InvalidOperationException($"Loan record with ID {dtoId} was not found for updating");
        loanRecordRepository.Update(toUpdateLoanRecord);
        return mapper.Map<LoanRecordGetDto>(toUpdateLoanRecord);
    }

    /// <summary>
    /// Deletes a loan record identified by its unique ID.
    /// </summary>
    /// <param name="dtoId">The ID of the loan record to delete.</param>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public void Delete(int dtoId)
    {
        if (!loanRecordRepository.Delete(dtoId))
            throw new InvalidOperationException($"Loan record with ID {dtoId} was not found for deleting");
    }
}