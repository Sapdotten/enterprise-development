using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

/// <summary>
/// Service for managing book-related operations, including CRUD actions and DTO mapping.
/// </summary>
public class BookService(
    IRepository<Book, int> bookRepository, IMapper mapper) : IApplicationService<BookGetDto, BookCreateDto, int>
{
    /// <summary>
    /// Creates a new book from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing book data. Must not be null.</param>
    /// <returns>The created book as a BookGetDTO.</returns>
    public BookGetDto Create(BookCreateDto dto)
    {
        var newBook = mapper.Map<Book>(dto);
        bookRepository.Create(newBook);

        return mapper.Map<BookGetDto>(newBook);
    }

    /// <summary>
    /// Retrieves a book by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The ID of the book to retrieve.</param>
    /// <returns>The book as a BookGetDTO if found.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no book exists with the given ID.</exception>
    public BookGetDto Get(int dtoId)
    {
        var book = bookRepository.Read(dtoId) ?? throw new InvalidOperationException($"Book with ID {dtoId} was not found");
        return mapper.Map<BookGetDto>(book);
    }

    /// <summary>
    /// Retrieves a list of all books.
    /// </summary>
    /// <returns>A list of all books represented as BookGetDTOs. Returns empty list if none exist.</returns>
    public List<BookGetDto> GetAll()
    {
        var books = bookRepository.ReadAll();
        return mapper.Map<List<BookGetDto>>(books);
    }

    /// <summary>
    /// Updates an existing book with data from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated book data. Must not be null.</param>
    /// <param name="dtoId">The ID of the book to update.</param>
    /// <returns>The updated book as a BookGetDTO.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no book exists with the given ID.</exception>
    public BookGetDto Update(BookCreateDto dto, int dtoId)
    {
        var toUpdateBook = bookRepository.Read(dtoId) ?? throw new InvalidOperationException($"Book with ID {dtoId} was not found for updating");
        mapper.Map(dto, toUpdateBook);
        bookRepository.Update(toUpdateBook);
        return mapper.Map<BookGetDto>(toUpdateBook);
    }

    /// <summary>
    /// Deletes a book identified by its unique ID.
    /// </summary>
    /// <param name="dtoId">The ID of the book to delete.</param>
    /// <returns>True, if success, False when no book exists with the given ID.</returns>
    public bool Delete(int dtoId)
    {
        return bookRepository.Delete(dtoId);
    }
}