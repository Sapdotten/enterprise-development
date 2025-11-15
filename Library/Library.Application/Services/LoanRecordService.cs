using AutoMapper;
using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

/// <summary>
/// Application service for managing loan record operations.
/// </summary>
public class LoanRecordService(
    IRepository<LoanRecord, int> loanRecordRepository, IMapper mapper
    ) : IApplicationService<LoanRecordGetDTO, LoanRecordCreateDTO, int>
{
    /// <summary>
    /// Creates a new loan record from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing loan record data. Must not be null.</param>
    /// <returns>The created loan record as a LoanRecordGetDTO.</returns>
    public LoanRecordGetDTO Create(LoanRecordCreateDTO dto)
    {
        var newLoanRecord = mapper.Map<LoanRecord>(dto);
        loanRecordRepository.Create(newLoanRecord);
        return mapper.Map<LoanRecordGetDTO>(newLoanRecord);
    }

    /// <summary>
    /// Retrieves a loan record by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The ID of the loan record to retrieve.</param>
    /// <returns>The loan record as a LoanRecordGetDTO if found.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public LoanRecordGetDTO Get(int dtoId)
    {
        var loanRecord = loanRecordRepository.Read(dtoId) ?? throw new InvalidOperationException($"Loan record with {dtoId} was not found");
        return mapper.Map<LoanRecordGetDTO>(loanRecord);
    }

    /// <summary>
    /// Retrieves a list of all loan records.
    /// </summary>
    /// <returns>A list of all loan records represented as LoanRecordGetDTOs. Returns empty list if none exist.</returns>
    public List<LoanRecordGetDTO> GetAll()
    {
        var loanRecords = loanRecordRepository.ReadAll();
        return mapper.Map<List<LoanRecordGetDTO>>(loanRecords);
    }

    /// <summary>
    /// Updates an existing loan record with data from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated loan record data. Must not be null.</param>
    /// <param name="dtoId">The ID of the loan record to update.</param>
    /// <returns>The updated loan record as a LoanRecordGetDTO.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loan record exists with the given ID.</exception>
    public LoanRecordGetDTO Update(LoanRecordCreateDTO dto, int dtoId)
    {
        var toUpdateLoanRecord = loanRecordRepository.Read(dtoId) ?? throw new InvalidOperationException($"Loan record with ID {dtoId} was not found for updating");
        loanRecordRepository.Update(toUpdateLoanRecord);
        return mapper.Map<LoanRecordGetDTO>(toUpdateLoanRecord);
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