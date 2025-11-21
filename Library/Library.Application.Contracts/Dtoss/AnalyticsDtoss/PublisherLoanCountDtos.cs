namespace Library.Application.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// Data transfer object representing a publisher's loan statistics.
/// </summary>
public class PublisherLoanCountDto
{
    /// <summary>
    /// Name of the publisher.
    /// </summary>
    public required string PublisherName { get; set; }

    /// <summary>
    /// Total number of times books from this publisher have been loaned out.
    /// </summary>
    public required int LoanCount { get; set; }
}