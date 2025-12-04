using Library.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Base asynchronous controller for CRUD operations.
/// Provides common HTTP methods and structured logging for derived controllers.
/// </summary>
/// <typeparam name="TGetDto">DTO type for reading data.</typeparam>
/// <typeparam name="TCreateDto">DTO type for creating or updating data.</typeparam>
/// <typeparam name="TKey">Type of the entity's identifier.</typeparam>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TGetDto, TCreateDto, TKey>(
    IApplicationService<TGetDto, TCreateDto, TKey> appService) : ControllerBase
{
    /// <summary>
    /// Creates a new entity from the provided DTO.
    /// </summary>
    /// <param name="newDto">Data transfer object containing creation data.</param>
    /// <returns>Created entity with 201 status or 500 on error.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<TGetDto>> Create(TCreateDto newDto)
    {
        var result = await appService.CreateAsync(newDto);
        return CreatedAtAction(nameof(Create), result);
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
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<TGetDto>> Edit(TKey id, TCreateDto newDto)
    {
        try
        {
            var result = await appService.UpdateAsync(newDto, id);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity to delete.</param>
    /// <returns>204 on success.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<IActionResult> Delete(TKey id)
    {
        await appService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Retrieves all entities as a list of DTOs.
    /// </summary>
    /// <returns>List of DTOs with 200 status or 500 on error.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<IList<TGetDto>>> GetAll()
    {
        var result = await appService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a single entity by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the entity to retrieve.</param>
    /// <returns>Entity DTO with 200 status or 404 if not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<TGetDto>> Get(TKey id)
    {
        try
        {
            var result = await appService.GetAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}