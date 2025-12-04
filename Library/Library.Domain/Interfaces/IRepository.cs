namespace Library.Domain.Interfaces;

using System.Threading.Tasks;

/// <summary>
/// Defines a generic repository interface for basic data access operations.
/// </summary>
/// <typeparam name="TEntity">The domain entity type managed by the repository.</typeparam>
/// <typeparam name="TKey">The unique identifier type of the entity.</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Creates a new entity and assigns a unique identifier.
    /// </summary>
    /// <param name="entity">The entity instance to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created entity.</returns>
    Task<TKey> CreateAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity instance with updated values. Must not be null.</param>
    /// <returns>The updated entity if found; otherwise, null.</returns>
    Task<TEntity?> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="key">The unique identifier of the entity to delete.</param>
    /// <returns>True if the entity was found and deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(TKey key);

    /// <summary>
    /// Retrieves all entities of the specified type.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    Task<List<TEntity>> ReadAllAsync();

    /// <summary>
    /// Retrieves a single entity by its unique identifier.
    /// </summary>
    /// <param name="key">The unique identifier of the entity to retrieve.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity?> ReadAsync(TKey key);
}
