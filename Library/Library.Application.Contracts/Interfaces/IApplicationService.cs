namespace Library.Application.Contracts.Interfaces;

/// <summary>
/// Defines a generic application service interface for CRUD operations on entities.
/// Maps between domain models and data transfer objects (DTOs) and encapsulates business logic.
/// </summary>
/// <typeparam name="TGetDto">The DTO type used for reading data.</typeparam>
/// <typeparam name="TCreateDto">The DTO type used for creating or updating data.</typeparam>
/// <typeparam name="TKey">The type of the entity's unique identifier.</typeparam>
public interface IApplicationService<TGetDto, TCreateDto, TKey>
{
    /// <summary>
    /// Creates a new entity from the provided data transfer object.
    /// </summary>
    /// <param name="dto">The DTO containing the data for the new entity. Must not be null.</param>
    /// <returns>The created entity represented as a TGetDTO.</returns>
    public Task<TGetDto> CreateAsync(TCreateDto dto);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the entity to retrieve.</param>
    /// <returns>The entity as a TGetDTO if found; otherwise, throws KeyNotFoundException.</returns>
    public Task<TGetDto> GetAsync(TKey dtoId);

    /// <summary>
    /// Retrieves a list of all entities.
    /// </summary>
    /// <returns>A list of all entities represented as TGetDTOs. Returns empty list if none exist.</returns>
    public Task<List<TGetDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing entity with data from the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing the updated data. Must not be null.</param>
    /// <param name="dtoid">The unique identifier of the entity to update.</param>
    /// <returns>The updated entity represented as a TGetDTO.</returns>
    public Task<TGetDto> UpdateAsync(TCreateDto dto, TKey dtoid);

    /// <summary>
    /// Deletes an entity identified by its unique identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the entity to delete.</param>
    /// <returns>True, if success, False when no book exists with the given ID.</returns>
    public Task<bool> DeleteAsync(TKey dtoId);
}
