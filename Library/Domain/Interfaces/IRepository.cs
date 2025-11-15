namespace Library.Domain.Interfaces;

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
    public TKey Create(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity instance with updated values. Must not be null.</param>
    /// <returns>The updated entity if found; otherwise, null.</returns>
    public TEntity? Update(TEntity entity);

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="key">The unique identifier of the entity to delete.</param>
    /// <returns>True if the entity was found and deleted; otherwise, false.</returns>
    public bool Delete(TKey key);

    /// <summary>
    /// Retrieves all entities of the specified type.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Retrieves a single entity by its unique identifier.
    /// </summary>
    /// <param name="key">The unique identifier of the entity to retrieve.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public TEntity? Read(TKey key);
}