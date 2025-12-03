using Library.Application.Contracts.Dtos;

namespace Library.Application.Contracts.Interfaces;

/// <summary>
/// Service interface for managing loan record operations, including creation and batch processing of contracts.
/// Extends IApplicationService to provide additional methods.
/// </summary>
public interface ILoanRecordService : IApplicationService<LoanRecordGetDto, LoanRecordCreateDto, int>
{
    /// <summary>
    /// Asynchronously processes a batch of loan record contracts.
    /// Maps each DTO to a domain entity and persists them in the data store.
    /// </summary>
    /// <param name="contracts">List of LoanRecordCreateDto representing new loan agreements. Must not be null.</param>
    public Task ReceiveContractAsync(IList<LoanRecordCreateDto> contracts);
}