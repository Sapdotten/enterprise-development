using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Postgres.Repositories;

/// <summary>
/// Asynchronous repository implementation for Reader entities using Entity Framework Core.
/// </summary>
public class ReaderRepository(AppDbContext dbContext) : IRepository<Reader, int>
{
    public async Task<int> CreateAsync(Reader reader)
    {
        var entry = await dbContext.Readers.AddAsync(reader);
        await dbContext.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task<Reader?> UpdateAsync(Reader reader)
    {
        var toUpdateReader = await ReadAsync(reader.Id);
        if (toUpdateReader == null) return null;

        toUpdateReader.FirstName = reader.FirstName;
        toUpdateReader.LastName = reader.LastName;
        toUpdateReader.PatronymicName = reader.PatronymicName;
        toUpdateReader.Address = reader.Address;
        toUpdateReader.PhoneNumber = reader.PhoneNumber;
        toUpdateReader.RegistrationDate = reader.RegistrationDate;

        await dbContext.SaveChangesAsync();

        return toUpdateReader;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var toDeleteReader = await ReadAsync(id);
        if (toDeleteReader == null) return false;
        dbContext.Remove(toDeleteReader);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Reader>> ReadAllAsync()
    {
        return await dbContext.Readers.ToListAsync();
    }

    public async Task<Reader?> ReadAsync(int id)
    {
        return await dbContext.Readers.FirstOrDefaultAsync(s => s.Id == id);
    }
}