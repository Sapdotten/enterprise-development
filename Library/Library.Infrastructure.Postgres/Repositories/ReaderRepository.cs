using Library.Domain.Interfaces;
using Library.Domain.Entities;

namespace Library.Infrastructure.Postgres.Repositories;

/// <summary>
/// In-memory implementation of IRepository for Reader entities.
/// Initializes with seed data and manages unique IDs via an incrementing counter.
/// </summary>
public class ReaderRepository(AppDbContext dbContext) : IRepository<Reader, int>
{
    /// <summary>
    /// Creates a new reader with a unique identifier.
    /// </summary>
    /// <param name="reader">The reader instance to create. Must not be null.</param>
    /// <returns>The unique identifier assigned to the created reader.</returns>
    public int Create(Reader reader)
    {
        var entry = dbContext.Readers.Add(reader);
        dbContext.SaveChanges();
        return entry.Entity.Id;
    }

    /// <summary>
    /// Updates an existing reader with new property values.
    /// </summary>
    /// <param name="reader">The reader instance with updated values. Must not be null.</param>
    /// <returns>The updated reader if found; otherwise, null.</returns>
    public Reader? Update(Reader reader)
    {
        var toUpdateReader = Read(reader.Id);
        if (toUpdateReader == null) return null;

        toUpdateReader.FirstName = reader.FirstName;
        toUpdateReader.LastName = reader.LastName;
        toUpdateReader.PatronymicName = reader.PatronymicName;
        toUpdateReader.Address = reader.Address;
        toUpdateReader.PhoneNumber = reader.PhoneNumber;
        toUpdateReader.RegistrationDate = reader.RegistrationDate;

        dbContext.SaveChanges();

        return toUpdateReader;
    }

    /// <summary>
    /// Deletes a reader by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reader to delete.</param>
    /// <returns>True if the reader was found and deleted; otherwise, false.</returns>
    public bool Delete(int id)
    {
        var toDeleteReader = Read(id);
        if (toDeleteReader == null) return false;
        dbContext.Remove(toDeleteReader);
        dbContext.SaveChanges();
        return true;
    }

    /// <summary>
    /// Retrieves all readers currently stored in the repository.
    /// </summary>
    /// <returns>A list of all readers. Returns a copy of the internal collection.</returns>
    public List<Reader> ReadAll()
    {
        return [.. dbContext.Readers];
    }

    /// <summary>
    /// Retrieves a single reader by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reader to retrieve.</param>
    /// <returns>The reader if found; otherwise, null.</returns>
    public Reader? Read(int id)
    {
        return dbContext.Readers.FirstOrDefault(s => s.Id == id);
    }
}