using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing books.
/// Inherits CRUD operations from CrudControllerBase.
/// </summary>
/// <param name="bookService">Service handling book operations.</param>
/// <param name="logger">Logger instance for diagnostics.</param>
public class BookController(IApplicationService<BookGetDTO, BookCreateDTO, int> bookService, ILogger<BookController> logger)
    : CrudControllerBase<BookGetDTO, BookCreateDTO, int>(bookService, logger);