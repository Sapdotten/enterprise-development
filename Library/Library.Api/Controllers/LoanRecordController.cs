using Library.Application.Contracts.DTOs;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing book checkout operations.
/// Handles loan records through CRUD endpoints inherited from CrudControllerBase.
/// </summary>
/// <param name="loanRecordService">Service responsible for loan record operations.</param>
/// <param name="logger">Logger instance for diagnostics and monitoring.</param>
public class LoanRecordController(IApplicationService<LoanRecordGetDTO, LoanRecordCreateDTO, int> loanRecordService, ILogger<LoanRecordController> logger)
    : CrudControllerBase<LoanRecordGetDTO, LoanRecordCreateDTO, int>(loanRecordService, logger);