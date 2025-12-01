using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Infrastructure.Postgres.Repositories;

/// <summary>
/// In-memory implementation of IRepository for Book entities.
/// Uses SeedData to initialize with predefined data and manages identity via incrementing ID counter.
/// </summary>
public class BookRepository(AppDbContext dbContext) : IRepository<Book, int>
{
    /// <summary>
    /// Creates a new book with a unique identifier.
    /// </summary>
    /// <param name="book">The book instance to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created book.</returns>
    public int Create(Book book)
    {
        var entry = dbContext.Books.Add(book);
        dbContext.SaveChanges();
        return entry.Entity.Id;
    }

    /// <summary>
    /// Updates an existing book with new property values.
    /// </summary>
    /// <param name="book">The book instance with updated values. Must not be null.</param>
    /// <returns>The updated book if found; otherwise, null.</returns>
    public Book? Update(Book book)
    {
        var toUpdateBook = Read(book.Id);
        if (toUpdateBook == null) return null;

        toUpdateBook.InventoryNumber = book.InventoryNumber;
        toUpdateBook.Code = book.Code;
        toUpdateBook.Authors = book.Authors;
        toUpdateBook.Title = book.Title;
        toUpdateBook.PublisherType = book.PublisherType;
        toUpdateBook.Publisher = book.Publisher;

        dbContext.SaveChanges();

        return toUpdateBook;
    }

    /// <summary>
    /// Deletes a book by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book to delete.</param>
    /// <returns>True if the book was found and deleted; otherwise, false.</returns>
    public bool Delete(int id)
    {
        var toDeleteBook = Read(id);
        if (toDeleteBook == null) return false;
        dbContext.Remove(toDeleteBook);
        dbContext.SaveChanges();
        return true;
    }

    /// <summary>
    /// Retrieves all books currently stored in the repository.
    /// </summary>
    /// <returns>A list of all books. Returns a copy of the internal collection.</returns>
    public List<Book> ReadAll()
    {
        return [.. dbContext.Books];
    }

    /// <summary>
    /// Retrieves a single book by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book to retrieve.</param>
    /// <returns>The book if found; otherwise, null.</returns>
    public Book? Read(int id)
    {
        return dbContext.Books.FirstOrDefault(a => a.Id == id);
    }
}