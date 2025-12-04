using Library.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

/// <summary>
/// Represents a book in the library system with its metadata, including title, authors, 
/// catalog information, and publisher details.
/// </summary>
[Table("books")]
public class Book
{
    /// <summary>
    /// Unqiue identifier of the book.
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Unique inventory number of the book.
    /// </summary>
    [Column("inventory_number")]
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Catalog book code.
    /// </summary>
    [Column("code")]
    public required string Code { get; set; }

    /// <summary>
    /// Authors of the book.
    /// </summary>
    [Column("authors")]
    public required string Authors { get; set; }

    /// <summary>
    /// Name of the book.
    /// </summary>
    [Column("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Publication type of the book.
    /// </summary>
    [Column("publisher_type")]
    public required PublisherType PublisherType { get; set; }

    /// <summary>
    /// Publisher of the book.
    /// </summary>
    [Column("publisher")]
    public required Publisher Publisher { get; set; }

    /// <summary>
    /// The year of the book's publication.
    /// </summary>
    [Column("year")]
    public required int Year { get; set; }
}
