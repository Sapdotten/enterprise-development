namespace Domain.Entities;

/// <summary>
/// Represents a record of a book loan, capturing the details of when and to whom a book was issued.
/// </summary>
public class LoanRecord
{
    /// <summary>
    /// Unique identifier of loan record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identifier of the borrowed book
    /// </summary>
    public required int BookId { get; set; }

    /// <summary>
    /// Identifier of the reader who borrowed the book
    /// </summary>
    public required int ReaderId { get; set; }

    /// <summary>
    /// Date when the book was issued to the reader
    /// </summary>
    public required DateOnly IssueDate { get; set; }

    /// <summary>
    /// Duration of the loan period in days
    /// </summary>
    public required int LoanTerm { get; set; }
}
