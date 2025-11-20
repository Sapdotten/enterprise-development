using Library.Application.Contracts.Dtos.AnalyticsDtos;
using Library.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Provides API methods for analytical queries on library data.
/// </summary>
/// <param name="analyticsService">The service handling analytics operations.</param>
/// <param name="logger">Logger instance for request tracing and diagnostics.</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    ILibraryAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{

    /// <summary>
    /// Retrieves a list of all books currently loaned out, ordered alphabetically by title.
    /// </summary>
    /// <returns>
    /// Returns 200 with a list of BookLoanCountDto if successful; 
    /// 204 if no loaned books are found; 
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("loaned-books")]
    [ProducesResponseType(typeof(List<BookLoanCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public ActionResult<List<BookLoanCountDto>> GetBooksOrderedByTitle()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var result = analyticsService.GetLoanedBooksOrderedByTitle(today);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the top readers who have borrowed the highest number of books within a specified date range.
    /// </summary>
    /// <param name="start">Start date of the period (inclusive).</param>
    /// <param name="end">End date of the period (inclusive).</param>
    /// <param name="resultCount">Maximum number of results to return. Defaults to 5.</param>
    /// <returns>
    /// Returns 200 with a list of ReaderLoanCountDto if successful;
    /// 204 if no matching records are found;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("top-readers-by-loaned-books-count")]
    [ProducesResponseType(typeof(List<ReaderLoanCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public ActionResult<List<ReaderLoanCountDto>> GetTopReadersByNumberOfBooks(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int resultCount = 5)
    {
        var result = analyticsService.GetTopReadersByLoanCount(start, end, resultCount);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves readers with the longest single loan duration, ordered by full name.
    /// Results are ranked by name (ascending).
    /// </summary>
    /// <returns>
    /// Returns 200 with a list of ReaderLoanDurationDto if data exists;
    /// 204 if no active loan records are found;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("top-readers-by-longest-loan")]
    [ProducesResponseType(typeof(List<ReaderLoanDurationDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public ActionResult<List<ReaderLoanDurationDto>> GetTopReadersByTotalLoanDays()
    {
        var result = analyticsService.GetTopReadersByLongestLoanTermOrderedByName();
        return result.Count > 0 ? Ok(result) : NoContent();
    }

    /// <summary>
    /// Retrieves the top publishers ranked by the number of book loans within a given period.
    /// </summary>
    /// <param name="start">Start date of the period (inclusive).</param>
    /// <param name="end">End date of the period (inclusive).</param>
    /// <param name="resultCount">Maximum number of results to return. Defaults to 5.</param>
    /// <returns>
    /// Returns 200 with a list of PublisherLoanCountDto if successful;
    /// 204 if no data is available;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("top-publishers-by-loan-count")]
    [ProducesResponseType(typeof(List<PublisherLoanCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public ActionResult<List<PublisherLoanCountDto>> GetTopPopularPublishersLastYear(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int resultCount = 5)
    {
        var result = analyticsService.GetTopPublishersByLoanCount(start, end, resultCount);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the least popular books based on the lowest loan count within a specified time frame.
    /// </summary>
    /// <param name="start">Start date of the period (inclusive).</param>
    /// <param name="end">End date of the period (inclusive).</param>
    /// <param name="resultCount">Maximum number of results to return. Defaults to 5.</param>
    /// <returns>
    /// Returns 200 with a list of BookLoanCountDto if successful;
    /// 204 if no books meet the criteria;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("least-popular-books")]
    [ProducesResponseType(typeof(List<BookLoanCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public ActionResult<List<BookLoanCountDto>> GetTopLeastPopularBooksLastYear(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int resultCount = 5)
    {
        var result = analyticsService.GetBooksByLowestLoanCount(start, end, resultCount);
        return Ok(result);
    }
}