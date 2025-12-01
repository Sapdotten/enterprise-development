using Library.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Postgres;

/// <summary>
/// Provides functionality to seed the database with initial test data.
/// Checks if entities exist and populates them from SeedData if empty.
/// Resets IDs to ensure correct generation by the database.
/// </summary>
public static class DataInitializer
{
    /// <summary>
    /// Seeds the database context with initial data for Books, Readers, and LoanRecords.
    /// Only inserts data if the corresponding table is empty.
    /// Resets entity IDs to 0 before adding to allow database-generated values.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public static void Seed(AppDbContext context)
    {
        if (!context.Books.Any())
        {
            SeedData.Books.ForEach(x => x.Id = 0);
            context.Books.AddRange(SeedData.Books);
            context.SaveChanges();
        }

        if (!context.Readers.Any())
        {
            SeedData.Readers.ForEach(x => x.Id = 0);
            context.Readers.AddRange(SeedData.Readers);
            context.SaveChanges();
        }

        if (!context.LoanRecords.Any())
        {
            SeedData.LoanRecords.ForEach(x => x.Id = 0);
            context.LoanRecords.AddRange(SeedData.LoanRecords);
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Retrieves the maximum ID value from a list of entities.
    /// Uses reflection to access the Id property.
    /// Returns 0 if the list is empty or no valid ID is found.
    /// </summary>
    /// <typeparam name="T">The entity type, must have an Id property.</typeparam>
    /// <param name="list">The list of entities to scan.</param>
    /// <returns>The highest ID value in the list, or 0 if none.</returns>
    private static int GetMaxId<T>(List<T> list) where T : class
    {
        if (list.Count == 0)
            return 0;

        var idProperty = typeof(T).GetProperty("Id") ?? throw new InvalidOperationException("The type does not contain a property Id");

        var maxItem = list.MaxBy(x => idProperty.GetValue(x) as int? ?? 0);
        var maxValue = idProperty.GetValue(maxItem);

        return maxValue is int id ? id : 0;
    }
}