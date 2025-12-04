using Library.Application.Contracts.Dtos.AnalyticsDtos;
using Library.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Provides asynchronous API methods for analytical queries on library data.
/// </summary>
/// <param name="analyticsService">The service handling analytics operations.</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(ILibraryAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all books currently loaned out, ordered alphabetically by title.
    /// </summary>
    /// <returns>
    /// Returns 200 with a list of BookLoanCountDto if successful;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("loaned-books")]
    [ProducesResponseType(typeof(List<BookLoanCountDto>), 200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<List<BookLoanCountDto>>> GetBooksOrderedByTitle()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var result = await analyticsService.GetLoanedBooksOrderedByTitleAsync(today);
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
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("top-readers-by-loaned-books-count")]
    [ProducesResponseType(typeof(List<ReaderLoanCountDto>), 200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<List<ReaderLoanCountDto>>> GetTopReadersByNumberOfBooks(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int resultCount = 5)
    {
        var result = await analyticsService.GetTopReadersByLoanCountAsync(start, end, resultCount);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves readers with the longest single loan duration, ordered by full name.
    /// Results are ranked by name (ascending).
    /// </summary>
    /// <returns>
    /// Returns 200 with a list of ReaderLoanDurationDto if data exists;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("top-readers-by-longest-loan")]
    [ProducesResponseType(typeof(List<ReaderLoanDurationDto>), 200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<List<ReaderLoanDurationDto>>> GetTopReadersByTotalLoanDays()
    {
        var result = await analyticsService.GetTopReadersByLongestLoanTermOrderedByNameAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the top publishers ranked by the number of book loans within a given period.
    /// </summary>
    /// <param name="start">Start date of the period (inclusive).</param>
    /// <param name="end">End date of the period (inclusive).</param>
    /// <param name="resultCount">Maximum number of results to return. Defaults to 5.</param>
    /// <returns>
    /// Returns 200 with a list of PublisherLoanCountDto if successful;
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("top-publishers-by-loan-count")]
    [ProducesResponseType(typeof(List<PublisherLoanCountDto>), 200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<List<PublisherLoanCountDto>>> GetTopPopularPublishersLastYear(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int resultCount = 5)
    {
        var result = await analyticsService.GetTopPublishersByLoanCountAsync(start, end, resultCount);
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
    /// 500 if an internal error occurs.
    /// </returns>
    [HttpGet("least-popular-books")]
    [ProducesResponseType(typeof(List<BookLoanCountDto>), 200)]
    [ProducesResponseType(500)]
    [ServiceFilter<LoggingActionFilter>]
    public async Task<ActionResult<List<BookLoanCountDto>>> GetTopLeastPopularBooksLastYear(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end,
        [FromQuery] int resultCount = 5)
    {
        var result = await analyticsService.GetBooksByLowestLoanCountAsync(start, end, resultCount);
        return Ok(result);
    }
}