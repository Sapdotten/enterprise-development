using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a book in the library system with its metadata, including title, authors, 
/// catalog information, and publisher details.
/// </summary>
public class Book
{
    /// <summary>
    /// Unqiue identifier of book
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Unique inventory number of book
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Catalog book code
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Authors of book
    /// </summary>
    public required string Authors { get; set; }

    /// <summary>
    /// Name of book
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Publication type of book
    /// </summary>
    public required PublisherType PublisherType { get; set; }

    /// <summary>
    /// Publisher of book
    /// </summary>
    public required Publisher Publisher { get; set; }

}
