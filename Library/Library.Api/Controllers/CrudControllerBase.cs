using Library.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Base controller for CRUD operations.
/// Provides common HTTP methods and structured logging for derived controllers.
/// </summary>
/// <typeparam name="TGetDto">DTO type for reading data.</typeparam>
/// <typeparam name="TCreateDto">DTO type for creating or updating data.</typeparam>
/// <typeparam name="TKey">Type of the entity's identifier.</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TGetDto, TCreateDto, TKey>(
    IApplicationService<TGetDto, TCreateDto, TKey> appService,
    ILogger<CrudControllerBase<TGetDto, TCreateDto, TKey>> logger) : ControllerBase
{
    /// <summary>
    /// Executes an operation with logging and error handling.
    /// Logs initiation, success with item count, or failure with exception details.
    /// </summary>
    /// <param name="operation">Name of the operation being executed.</param>
    /// <param name="action">Delegate representing the operation to execute.</param>
    /// <returns>Action result wrapped in logging and exception handling.</returns>
    protected ActionResult Logging(string operation, Func<ActionResult> action)
    {
        logger.LogInformation("Initiating {Operation}", operation);
        try
        {
            var result = action();
            var count = 0;
            if (result is OkObjectResult okResult && okResult.Value != null)
            {
                if (okResult.Value is System.Collections.IEnumerable collection)
                {
                    count = collection.Cast<object>().Count();
                }
                else count = 1;
            }
            logger.LogInformation("Completed {Operation}. Retrieved {Count} items.", operation, count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to execute {Operation}", operation);
            return StatusCode(500, $"Internal error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new entity from the provided DTO.
    /// </summary>
    /// <param name="newDto">Data transfer object containing creation data.</param>
    /// <returns>Created entity with 201 status or 500 on error.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public ActionResult<TGetDto> Create(TCreateDto newDto)
    {
        return Logging(nameof(Create), () =>
        {
            var result = appService.Create(newDto);
            return CreatedAtAction(nameof(Create), result);
        });
    }

    /// <summary>
    /// Updates an existing entity identified by key.
    /// </summary>
    /// <param name="id">Identifier of the entity to update.</param>
    /// <param name="newDto">Data transfer object containing updated data.</param>
    /// <returns>Updated entity with 200 status, 404 if not found, or 500 on error.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TGetDto> Edit(TKey id, TCreateDto newDto)
    {
        return Logging(nameof(Edit), () =>
        {
            try
            {
                var result = appService.Update(newDto, id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity to delete.</param>
    /// <returns>200 on success, 404 if not found, or 500 on error.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        return Logging(nameof(Delete), () =>
        {
            try
            {
                appService.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }

    /// <summary>
    /// Retrieves all entities as a list of DTOs.
    /// </summary>
    /// <returns>List of DTOs with 200 status or 500 on error.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IList<TGetDto>> GetAll()
    {
        return Logging(nameof(GetAll), () =>
        {
            var result = appService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// Retrieves a single entity by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity to retrieve.</param>
    /// <returns>Entity DTO with 200 status, 204 if not found, or 500 on error.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<TGetDto> Get(TKey id)
    {
        return Logging(nameof(Get), () =>
        {
            try
            {
                var result = appService.Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }
}