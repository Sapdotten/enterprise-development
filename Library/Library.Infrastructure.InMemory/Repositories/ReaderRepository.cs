using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory implementation of IRepository for Reader entities.
/// Initializes with seed data and manages unique IDs via an incrementing counter.
/// </summary>
public class ReaderRepository : IRepository<Reader, int>
{
    private readonly List<Reader> _readers;
    private int _maxId;

    /// <summary>
    /// Initializes a new instance of the ReaderRepository class.
    /// Loads initial reader data from SeedData and sets the next available ID.
    /// </summary>
    public ReaderRepository()
    {
        _readers = [.. SeedData.Readers];
        _maxId = _readers.Count > 0 ? _readers.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Creates a new reader with a unique identifier.
    /// </summary>
    /// <param name="reader">The reader instance to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created reader.</returns>
    public Task<int> CreateAsync(Reader reader)
    {
        reader.Id = ++_maxId;
        _readers.Add(reader);
        return Task.FromResult(reader.Id);
    }

    /// <summary>
    /// Updates an existing reader with new property values.
    /// </summary>
    /// <param name="reader">The reader instance with updated values. Must not be null.</param>
    /// <returns>The updated reader if found; otherwise, null.</returns>
    public Task<Reader?> UpdateAsync(Reader reader)
    {
        var toUpdate = _readers.FirstOrDefault(r => r.Id == reader.Id);
        if (toUpdate == null) return Task.FromResult<Reader?>(null);

        toUpdate.FirstName = reader.FirstName;
        toUpdate.LastName = reader.LastName;
        toUpdate.PatronymicName = reader.PatronymicName;
        toUpdate.Address = reader.Address;
        toUpdate.PhoneNumber = reader.PhoneNumber;
        toUpdate.RegistrationDate = reader.RegistrationDate;

        return Task.FromResult<Reader?>(toUpdate);
    }

    /// <summary>
    /// Deletes a reader by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reader to delete.</param>
    /// <returns>True if the reader was found and deleted; otherwise, false.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var toDelete = _readers.FirstOrDefault(r => r.Id == id);
        if (toDelete == null) return Task.FromResult(false);

        var result = _readers.Remove(toDelete);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Retrieves all readers currently stored in the repository.
    /// </summary>
    /// <returns>A list of all readers. Returns a copy of the internal collection.</returns>
    public Task<List<Reader>> ReadAllAsync()
    {
        return Task.FromResult<List<Reader>>([.. _readers]);
    }

    /// <summary>
    /// Retrieves a single reader by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reader to retrieve.</param>
    /// <returns>The reader if found; otherwise, null.</returns>
    public Task<Reader?> ReadAsync(int id)
    {
        var reader = _readers.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(reader);
    }
}