using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing book reader operations.
/// Exposes CRUD endpoints for readers via inheritance from CrudControllerBase.
/// </summary>
/// <param name="readerService">Service handling reader-related business logic.</param>
/// <param name="logger">Logger instance for diagnostics and request tracing.</param>
public class BookReaderController(IApplicationService<ReaderGetDTO, ReaderCreateDTO, int> readerService, ILogger<BookReaderController> logger)
    : CrudControllerBase<ReaderGetDTO, ReaderCreateDTO, int>(readerService, logger);