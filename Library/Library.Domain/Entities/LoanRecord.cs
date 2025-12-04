namespace Library.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a record of a book loan, capturing the details of when and to whom a book was issued.
/// </summary>
[Table("loan_records")]
public class LoanRecord
{
    /// <summary>
    /// Unique identifier of loan record
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Identifier of the borrowed book
    /// </summary>
    [Column("book_id")]
    public required int BookId { get; set; }

    /// <summary>
    /// Identifier of the reader who borrowed the book
    /// </summary>
    [Column("reader_id")]
    public required int ReaderId { get; set; }

    /// <summary>
    /// Date when the book was issued to the reader
    /// </summary>
    [Column("issue_date")]
    public required DateOnly IssueDate { get; set; }

    /// <summary>
    /// Duration of the loan period in days
    /// </summary>
    [Column("loan_term")]
    public required int LoanTerm { get; set; }
}
