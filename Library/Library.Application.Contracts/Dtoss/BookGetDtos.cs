namespace Library.Application.Contracts.Dtos;

/// <summary>
/// Data Transfer Object representing a book retrieved from the library system.
/// Contains full book metadata including identifier, catalog information, authorship, 
/// publication details, and is used in read operations across services and API endpoints.
/// </summary>
public class BookGetDto
{
    /// <summary>
    /// Unique identifier of the book.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Unique inventory number of the book.
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Catalog code identifying the book within the library's classification system.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Full names of the book's authors.
    /// </summary>
    public required string Authors { get; set; }

    /// <summary>
    /// Title of the book as published.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Type of publisher (e.g., Commercial, Academic, Educational).
    /// </summary>
    public required string PublisherType { get; set; }

    /// <summary>
    /// Name of the publishing house responsible for the book's release.
    /// </summary>
    public required string Publisher { get; set; }

    /// <summary>
    /// Year when the book was published.
    /// </summary>
    public required int Year { get; set; }
}