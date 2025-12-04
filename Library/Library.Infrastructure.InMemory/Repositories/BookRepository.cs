using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory implementation of IRepository for Book entities.
/// Uses SeedData to initialize with predefined data and manages identity via incrementing ID counter.
/// </summary>
public class BookRepository : IRepository<Book, int>
{
    private readonly List<Book> _books;
    private int _maxId;

    /// <summary>
    /// Initializes a new instance of the BookRepository class.
    /// Loads initial data from SeedData and sets the next available ID.
    /// </summary>
    public BookRepository()
    {
        _books = [.. SeedData.Books];
        _maxId = _books.Count > 0 ? _books.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Creates a new book with a unique identifier.
    /// </summary>
    /// <param name="book">The book instance to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created book.</returns>
    public Task<int> CreateAsync(Book book)
    {
        book.Id = ++_maxId;
        _books.Add(book);
        return Task.FromResult(book.Id);
    }

    /// <summary>
    /// Updates an existing book with new property values.
    /// </summary>
    /// <param name="book">The book instance with updated values. Must not be null.</param>
    /// <returns>The updated book if found; otherwise, null.</returns>
    public Task<Book?> UpdateAsync(Book book)
    {
        var toUpdateBook = _books.FirstOrDefault(a => a.Id == book.Id);
        if (toUpdateBook == null) return Task.FromResult<Book?>(null);

        toUpdateBook.InventoryNumber = book.InventoryNumber;
        toUpdateBook.Code = book.Code;
        toUpdateBook.Authors = book.Authors;
        toUpdateBook.Title = book.Title;
        toUpdateBook.PublisherType = book.PublisherType;
        toUpdateBook.Publisher = book.Publisher;
        toUpdateBook.Year = book.Year;

        return Task.FromResult<Book?>(toUpdateBook);
    }

    /// <summary>
    /// Deletes a book by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book to delete.</param>
    /// <returns>True if the book was found and deleted; otherwise, false.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var toDeleteBook = _books.FirstOrDefault(a => a.Id == id);
        if (toDeleteBook == null) return Task.FromResult(false);

        var result = _books.Remove(toDeleteBook);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Retrieves all books currently stored in the repository.
    /// </summary>
    /// <returns>A list of all books. Returns a copy of the internal collection.</returns>
    public Task<List<Book>> ReadAllAsync()
    {
        return Task.FromResult<List<Book>>([.. _books]);
    }

    /// <summary>
    /// Retrieves a single book by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book to retrieve.</param>
    /// <returns>The book if found; otherwise, null.</returns>
    public Task<Book?> ReadAsync(int id)
    {
        var book = _books.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(book);
    }
}
