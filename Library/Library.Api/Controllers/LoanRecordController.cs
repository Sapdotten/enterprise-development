using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing book checkout operations.
/// Handles loan records through CRUD endpoints inherited from CrudControllerBase.
/// </summary>
/// <param name="checkoutService">Service responsible for loan record operations.</param>
/// <param name="logger">Logger instance for diagnostics and monitoring.</param>
public class BookCheckoutController(IApplicationService<LoanRecordGetDTO, LoanRecordCreateDTO, int> checkoutService, ILogger<BookCheckoutController> logger)
    : CrudControllerBase<LoanRecordGetDTO, LoanRecordCreateDTO, int>(checkoutService, logger);