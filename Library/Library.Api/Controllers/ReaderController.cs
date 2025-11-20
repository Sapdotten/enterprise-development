using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing book reader operations.
/// Exposes CRUD endpoints for readers via inheritance from CrudControllerBase.
/// </summary>
/// <param name="readerService">Service handling reader-related business logic.</param>
public class ReaderController(IApplicationService<ReaderGetDto, ReaderCreateDto, int> readerService)
    : CrudControllerBase<ReaderGetDto, ReaderCreateDto, int>(readerService);