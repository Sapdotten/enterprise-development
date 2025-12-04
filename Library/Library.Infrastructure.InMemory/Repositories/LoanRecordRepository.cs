using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory implementation of IRepository for LoanRecord entities.
/// Initializes with seed data and manages unique IDs via an incrementing counter.
/// </summary>
public class LoanRecordRepository : IRepository<LoanRecord, int>
{
    private readonly List<LoanRecord> _loanRecords;
    private int _maxId;

    /// <summary>
    /// Initializes a new instance of the LoanRecordRepository class.
    /// Loads initial loan records from SeedData and sets the next available ID.
    /// </summary>
    public LoanRecordRepository()
    {
        _loanRecords = [.. SeedData.LoanRecords];
        _maxId = _loanRecords.Count > 0 ? _loanRecords.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Creates a new loan record with a unique identifier.
    /// </summary>
    /// <param name="loanRecord">The loan record to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created loan record.</returns>
    public Task<int> CreateAsync(LoanRecord loanRecord)
    {
        loanRecord.Id = ++_maxId;
        _loanRecords.Add(loanRecord);
        return Task.FromResult(loanRecord.Id);
    }

    /// <summary>
    /// Updates an existing loan record with new property values.
    /// </summary>
    /// <param name="loanRecord">The loan record with updated values. Must not be null.</param>
    /// <returns>The updated loan record if found; otherwise, null.</returns>
    public Task<LoanRecord?> UpdateAsync(LoanRecord loanRecord)
    {
        var toUpdate = _loanRecords.FirstOrDefault(r => r.Id == loanRecord.Id);
        if (toUpdate == null) return Task.FromResult<LoanRecord?>(null);

        toUpdate.BookId = loanRecord.BookId;
        toUpdate.ReaderId = loanRecord.ReaderId;
        toUpdate.IssueDate = loanRecord.IssueDate;
        toUpdate.LoanTerm = loanRecord.LoanTerm;

        return Task.FromResult<LoanRecord?>(toUpdate);
    }

    /// <summary>
    /// Deletes a loan record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the loan record to delete.</param>
    /// <returns>True if the record was found and removed; otherwise, false.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var toDelete = _loanRecords.FirstOrDefault(r => r.Id == id);
        if (toDelete == null) return Task.FromResult(false);

        var result = _loanRecords.Remove(toDelete);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Retrieves all loan records currently stored in the repository.
    /// </summary>
    /// <returns>A list of all loan records. Returns a copy of the internal collection.</returns>
    public Task<List<LoanRecord>> ReadAllAsync()
    {
        return Task.FromResult<List<LoanRecord>>([.. _loanRecords]);
    }

    /// <summary>
    /// Retrieves a single loan record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the loan record to retrieve.</param>
    /// <returns>The loan record if found; otherwise, null.</returns>
    public Task<LoanRecord?> ReadAsync(int id)
    {
        var record = _loanRecords.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(record);
    }
}
