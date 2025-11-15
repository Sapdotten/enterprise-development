using AutoMapper;
using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

/// <summary>
/// Service for managing book-related operations, including CRUD actions and DTO mapping.
/// </summary>
public class BookService(
    IRepository<Book, int> bookRepository, IMapper mapper) : IApplicationService<BookGetDTO, BookCreateDTO, int>
{
    /// <summary>
    /// Creates a new book from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing book data. Must not be null.</param>
    /// <returns>The created book as a BookGetDTO.</returns>
    public BookGetDTO Create(BookCreateDTO dto)
    {
        var newBook = mapper.Map<Book>(dto);
        bookRepository.Create(newBook);

        return mapper.Map<BookGetDTO>(newBook);
    }

    /// <summary>
    /// Retrieves a book by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The ID of the book to retrieve.</param>
    /// <returns>The book as a BookGetDTO if found.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no book exists with the given ID.</exception>
    public BookGetDTO Get(int dtoId)
    {
        var book = bookRepository.Read(dtoId) ?? throw new InvalidOperationException($"Book with ID {dtoId} was not found");
        return mapper.Map<BookGetDTO>(book);
    }

    /// <summary>
    /// Retrieves a list of all books.
    /// </summary>
    /// <returns>A list of all books represented as BookGetDTOs. Returns empty list if none exist.</returns>
    public List<BookGetDTO> GetAll()
    {
        var books = bookRepository.ReadAll();
        return mapper.Map<List<BookGetDTO>>(books);
    }

    /// <summary>
    /// Updates an existing book with data from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated book data. Must not be null.</param>
    /// <param name="dtoId">The ID of the book to update.</param>
    /// <returns>The updated book as a BookGetDTO.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no book exists with the given ID.</exception>
    public BookGetDTO Update(BookCreateDTO dto, int dtoId)
    {
        var toUpdateBook = bookRepository.Read(dtoId) ?? throw new InvalidOperationException($"Book with ID {dtoId} was not found for updating");
        bookRepository.Update(toUpdateBook);
        return mapper.Map<BookGetDTO>(toUpdateBook);
    }

    /// <summary>
    /// Deletes a book identified by its unique ID.
    /// </summary>
    /// <param name="dtoId">The ID of the book to delete.</param>
    /// <exception cref="InvalidOperationException">Thrown when no book exists with the given ID.</exception>
    public void Delete(int dtoId)
    {
        if (!bookRepository.Delete(dtoId))
            throw new InvalidOperationException($"Book with ID {dtoId} was not found for deleting");
    }
}