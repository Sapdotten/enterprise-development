using Library.Application.Contracts.DTOs.AnalyticsDTOs;
using Library.Application.Contracts.DTOs;

namespace Library.Application.Contracts.Interfaces;

/// <summary>
/// Provides analytical reports for the library system, including book loans, reader activity, and publisher popularity.
/// </summary>
public interface ILibraryAnalyticsService
{
    /// <summary>
    /// Retrieves all books currently on loan as of a specific date, sorted alphabetically by title.
    /// </summary>
    /// <param name="date">The reference date to evaluate active loans.</param>
    /// <returns>List of books on loan, ordered by title. Returns empty list if none found.</returns>
    public List<BookGetDTO> GetLoanedBooksOrderedByTitle(DateOnly date);

    /// <summary>
    /// Retrieves the top readers who borrowed the highest number of books within a given time period.
    /// Results are ranked by loan count in descending order.
    /// </summary>
    /// <param name="periodBegin">Start date of the analysis period (inclusive).</param>
    /// <param name="periodEnd">End date of the analysis period (inclusive).</param>
    /// <param name="resultsCount">Maximum number of top readers to return.</param>
    /// <returns>List of readers with the most loans in the specified period. Limited by resultsCount.</returns>
    public List<ReaderLoanCountDTO> GetTopReadersByLoanCount(DateOnly periodBegin, DateOnly periodEnd, int resultsCount);

    /// <summary>
    /// Retrieves readers with the longest single loan duration across their borrowing history.
    /// Retrieves readers who have the single longest loan duration in the system. 
    /// Only readers matching the global maximum loan term are included.
    /// Readers are ranked by full name (ascending).
    /// </summary>
    /// <returns>List of readers ordered by full name.</returns>
    public List<ReaderLoanDurationDTO> GetTopReadersByLongestLoanTermOrderedByName();

    /// <summary>
    /// Retrieves publishers with the highest number of book loans within a specified period.
    /// Results are ranked by total loan count in descending order.
    /// </summary>
    /// <param name="periodBegin">Start date of the analysis period (inclusive).</param>
    /// <param name="periodEnd">End date of the analysis period (inclusive).</param>
    /// <param name="resultsCount">Maximum number of top publishers to return.</param>
    /// <returns>List of publishers with the most loaned books in the period. Limited by resultsCount.</returns>
    public List<PublisherLoanCountDTO> GetTopPublishersByLoanCount(DateOnly periodBegin, DateOnly periodEnd, int resultsCount);

    /// <summary>
    /// Retrieves books with the lowest loan counts within a specified period.
    /// Results are ranked by loan count in ascending order.
    /// </summary>
    /// <param name="periodBegin">Start date of the analysis period (inclusive).</param>
    /// <param name="periodEnd">End date of the analysis period (inclusive).</param>
    /// <param name="resultsCount">Maximum number of least popular books to return.</param>
    /// <returns>List of books with the fewest loans in the period. Limited by resultsCount.</returns>
    public List<BookLoanCountDTO> GetBooksByLowestLoanCount(DateOnly periodBegin, DateOnly periodEnd, int resultsCount);
}