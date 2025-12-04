using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Postgres.Repositories;

/// <summary>
/// Asynchronous repository implementation for LoanRecord entities using Entity Framework Core.
/// </summary>
public class LoanRecordRepository(AppDbContext dbContext) : IRepository<LoanRecord, int>
{
    public async Task<int> CreateAsync(LoanRecord loanRecord)
    {
        var entry = await dbContext.LoanRecords.AddAsync(loanRecord);
        await dbContext.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task<LoanRecord?> UpdateAsync(LoanRecord loanRecord)
    {
        var toUpdateLoanRecord = await ReadAsync(loanRecord.Id);
        if (toUpdateLoanRecord == null) return null;

        toUpdateLoanRecord.BookId = loanRecord.BookId;
        toUpdateLoanRecord.ReaderId = loanRecord.ReaderId;
        toUpdateLoanRecord.IssueDate = loanRecord.IssueDate;
        toUpdateLoanRecord.LoanTerm = loanRecord.LoanTerm;

        await dbContext.SaveChangesAsync();

        return toUpdateLoanRecord;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var toDeleteLoanRecord = await ReadAsync(id);
        if (toDeleteLoanRecord == null) return false;
        dbContext.Remove(toDeleteLoanRecord);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<LoanRecord>> ReadAllAsync()
    {
        return await dbContext.LoanRecords.ToListAsync();
    }

    public async Task<LoanRecord?> ReadAsync(int id)
    {
        return await dbContext.LoanRecords.FirstOrDefaultAsync(r => r.Id == id);
    }
}