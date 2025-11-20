using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing books.
/// Inherits CRUD operations from CrudControllerBase.
/// </summary>
/// <param name="bookService">Service handling book operations.</param>
public class BookController(IApplicationService<BookGetDto, BookCreateDto, int> bookService)
    : CrudControllerBase<BookGetDto, BookCreateDto, int>(bookService);