using Library.Domain.Enums;

namespace Library.Domain.Entities;

/// <summary>
/// Represents a book in the library system with its metadata, including title, authors, 
/// catalog information, and publisher details.
/// </summary>
public class Book
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
    /// Name of the book.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Publication type of the book.
    /// </summary>
    public required PublisherType PublisherType { get; set; }

    /// <summary>
    /// Publisher of the book.
    /// </summary>
    public required Publisher Publisher { get; set; }

    /// <summary>
    /// The year of the book's publication.
    /// </summary>
    public required int Year { get; set; }

}
