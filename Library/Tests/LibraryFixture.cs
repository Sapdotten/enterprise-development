using Library.Infrastructure.InMemory.Repositories;

namespace Library.Tests;

/// <summary>
/// Test fixture providing pre-initialized repository instances for unit and integration testing.
/// </summary>
public class LibraryFixture
{
    /// <summary>
    /// Gets the BookRepository instance initialized with seed data.
    /// </summary>
    public BookRepository Books { get; }

    /// <summary>
    /// Gets the ReaderRepository instance initialized with seed data.
    /// </summary>
    public ReaderRepository Readers { get; }

    /// <summary>
    /// Gets the LoanRecordRepository instance initialized with seed data.
    /// </summary>
    public LoanRecordRepository LoanRecords { get; }

    /// <summary>
    /// Initializes a new instance of the LibraryFixture class.
    /// Creates fresh instances of all repositories, loading them with predefined test data.
    /// </summary>
    public LibraryFixture()
    {
        Books = new BookRepository();
        Readers = new ReaderRepository();
        LoanRecords = new LoanRecordRepository();
    }
}