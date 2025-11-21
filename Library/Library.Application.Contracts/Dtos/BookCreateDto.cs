namespace Library.Application.Contracts.Dtos;

/// <summary>
/// Data Transfer Object for creating a new book.
/// Contains all necessary metadata required to register a book, excluding auto-generated fields such as ID.
/// Designed for use in API requests and service layer operations.
/// </summary>
public class BookCreateDto
{
    /// <summary>
    /// Unique inventory number of the book assigned by the library.
    /// Must be non-null and unique across all books.
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Catalog code identifying the book within the library's classification system.
    /// Must be non-null.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Full names of the book's authors, formatted as a comma-separated list if multiple.
    /// Must be not empty.
    /// </summary>
    public required string Authors { get; set; }

    /// <summary>
    /// Title of the book.
    /// Must be not null.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Type of publisher (e.g., Commercial, Academic, Educational).
    /// Used to categorize the source and nature of the publication.
    /// Must match one of the supported publisher types.
    /// </summary>
    public required string PublisherType { get; set; }

    /// <summary>
    /// Name of the publishing house responsible for the book's release.
    /// Should correspond to an existing publisher in the library's records.
    /// </summary>
    public required string Publisher { get; set; }

    /// <summary>
    /// Year when the book was published.
    /// </summary>
    public required int Year { get; set; }
}