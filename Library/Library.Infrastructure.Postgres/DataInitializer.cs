using Library.Domain.Data;

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
}