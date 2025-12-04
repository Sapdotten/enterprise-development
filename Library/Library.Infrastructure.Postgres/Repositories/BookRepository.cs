using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Postgres.Repositories;

/// <summary>
/// Asynchronous repository implementation for Book entities using Entity Framework Core.
/// </summary>
public class BookRepository(AppDbContext dbContext) : IRepository<Book, int>
{
    public async Task<int> CreateAsync(Book book)
    {
        var entry = await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();
        return entry.Entity.Id;
    }

    public async Task<Book?> UpdateAsync(Book book)
    {
        var toUpdateBook = await ReadAsync(book.Id);
        if (toUpdateBook == null) return null;

        toUpdateBook.InventoryNumber = book.InventoryNumber;
        toUpdateBook.Code = book.Code;
        toUpdateBook.Authors = book.Authors;
        toUpdateBook.Title = book.Title;
        toUpdateBook.PublisherType = book.PublisherType;
        toUpdateBook.Publisher = book.Publisher;

        await dbContext.SaveChangesAsync();

        return toUpdateBook;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var toDeleteBook = await ReadAsync(id);
        if (toDeleteBook == null) return false;
        dbContext.Remove(toDeleteBook);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Book>> ReadAllAsync()
    {
        return await dbContext.Books.ToListAsync();
    }

    public async Task<Book?> ReadAsync(int id)
    {
        return await dbContext.Books.FirstOrDefaultAsync(a => a.Id == id);
    }
}