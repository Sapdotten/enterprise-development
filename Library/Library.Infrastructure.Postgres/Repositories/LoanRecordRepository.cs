using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Infrastructure.Postgres.Repositories;

/// <summary>
/// In-memory implementation of IRepository for LoanRecord entities.
/// Initializes with seed data and manages unique IDs via an incrementing counter.
/// </summary>
public class LoanRecordRepository(AppDbContext dbContext) : IRepository<LoanRecord, int>
{
    /// <summary>
    /// Creates a new loan record with a unique identifier.
    /// </summary>
    /// <param name="loanRecord">The loan record to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created loan record.</returns>
    public int Create(LoanRecord loanRecord)
    {
        var entry = dbContext.LoanRecords.Add(loanRecord);
        dbContext.SaveChanges();
        return entry.Entity.Id;
    }

    /// <summary>
    /// Updates an existing loan record with new property values.
    /// </summary>
    /// <param name="loanRecord">The loan record with updated values. Must not be null.</param>
    /// <returns>The updated loan record if found; otherwise, null.</returns>
    public LoanRecord? Update(LoanRecord loanRecord)
    {
        var toUpdateLoanRecord = Read(loanRecord.Id);
        if (toUpdateLoanRecord == null) return null;

        toUpdateLoanRecord.BookId = loanRecord.BookId;
        toUpdateLoanRecord.ReaderId = loanRecord.ReaderId;
        toUpdateLoanRecord.IssueDate = loanRecord.IssueDate;
        toUpdateLoanRecord.LoanTerm = loanRecord.LoanTerm;

        dbContext.SaveChanges();

        return toUpdateLoanRecord;
    }

    /// <summary>
    /// Deletes a loan record by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the loan record to delete.</param>
    /// <returns>True if the record was found and removed; otherwise, false.</returns>
    public bool Delete(int Id)
    {
        var toDeleteLoanRecord = Read(Id);
        if (toDeleteLoanRecord == null) return false;

        dbContext.Remove(toDeleteLoanRecord);
        dbContext.SaveChanges();
        return true;
    }

    /// <summary>
    /// Retrieves all loan records currently stored in the repository.
    /// </summary>
    /// <returns>A list of all loan records. Returns a copy of the internal collection.</returns>
    public List<LoanRecord> ReadAll()
    {
        return [.. dbContext.LoanRecords];
    }

    /// <summary>
    /// Retrieves a single loan record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the loan record to retrieve.</param>
    /// <returns>The loan record if found; otherwise, null.</returns>
    public LoanRecord? Read(int id)
    {
        return dbContext.LoanRecords.FirstOrDefault(r => r.Id == id);
    }
}