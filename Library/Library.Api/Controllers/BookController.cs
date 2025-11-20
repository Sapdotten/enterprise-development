using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing books.
/// Inherits CRUD operations from CrudControllerBase.
/// </summary>
/// <param name="bookService">Service handling book operations.</param>
/// <param name="logger">Logger instance for diagnostics.</param>
public class BookController(IApplicationService<BookGetDto, BookCreateDto, int> bookService, ILogger<BookController> logger)
    : CrudControllerBase<BookGetDto, BookCreateDto, int>(bookService, logger);