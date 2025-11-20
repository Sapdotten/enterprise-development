namespace Library.Application.Contracts.Dtos;

/// <summary>
/// Data Transfer Object for creating a new loan record in the library system.
/// </summary>
public class LoanRecordCreateDto
{
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
