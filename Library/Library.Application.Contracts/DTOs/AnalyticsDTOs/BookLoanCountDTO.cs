namespace Library.Application.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// Data transfer object representing a book with its loan count.
/// Contains full book metadata and the total number of times the book has been loaned.
/// </summary>
public class BookLoanCountDto
{
    /// <summary>
    /// Unqiue identifier of the book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Unique inventory number of the book.
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Catalog book code.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Authors of the book.
    /// </summary>
    public required string Authors { get; set; }

    /// <summary>
    /// Name of book.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Publication type of book.
    /// </summary>
    public required string PublisherType { get; set; }

    /// <summary>
    /// Publisher of book.
    /// </summary>
    public required string Publisher { get; set; }

    /// <summary>
    /// The year of the book's publication.
    /// </summary>
    public required string Year { get; set; }

    /// <summary>
    /// Total number of times this book has been loaned out.
    /// </summary>
    public required int LoanCount { get; set; }
}
