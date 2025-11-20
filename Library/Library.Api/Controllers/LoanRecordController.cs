using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// REST controller for managing book checkout operations.
/// Handles loan records through CRUD endpoints inherited from CrudControllerBase.
/// </summary>
/// <param name="loanRecordService">Service responsible for loan record operations.</param>
public class LoanRecordController(IApplicationService<LoanRecordGetDto, LoanRecordCreateDto, int> loanRecordService)
    : CrudControllerBase<LoanRecordGetDto, LoanRecordCreateDto, int>(loanRecordService);