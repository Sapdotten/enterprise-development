using AutoMapper;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Application.Contracts.Dtos.AnalyticsDtos;
using Library.Application.Contracts.Dtos;

namespace Library.Application.Services;

/// <summary>
/// Implementation of ILibraryAnalyticsService that provides analytical reports
/// using data from repositories and mapping via AutoMapper.
/// </summary>
public class LibraryAnalyticsService(
    IRepository<LoanRecord, int> loanRecordRepository,
    IRepository<Book, int> bookRepository,
    IRepository<Reader, int> readerRepository,
    IMapper mapper) : ILibraryAnalyticsService
{
    /// <summary>
    /// Retrieves all books currently on loan as of the specified date, ordered alphabetically by title.
    /// A book is considered on loan if the given date falls within its issue and return period.
    /// </summary>
    /// <param name="date">The reference date to evaluate active loans.</param>
    /// <returns>List of books on loan, mapped to BookGetDTO and sorted by title.</returns>
    public async Task<List<BookLoanCountDto>> GetLoanedBooksOrderedByTitleAsync(DateOnly date)
    {
        var loanRecords = await loanRecordRepository.ReadAllAsync();
        var books = await bookRepository.ReadAllAsync();

        var result =
            loanRecords
                .Where(lr =>
                    lr.IssueDate <= date &&
                    lr.IssueDate.AddDays(lr.LoanTerm) >= date
                )
                .GroupBy(lr => lr.BookId)
                .Join(
                    books,
                    g => g.Key,
                    b => b.Id,
                    (g, b) => new BookLoanCountDto
                    {
                        Id = b.Id,
                        InventoryNumber = b.InventoryNumber,
                        Code = b.Code,
                        Authors = b.Authors,
                        Title = b.Title,
                        PublisherType = b.PublisherType.ToString(),
                        Publisher = b.Publisher.ToString(),
                        Year = b.Year,
                        LoanCount = g.Count()
                    }
                )
                .OrderBy(b => b.Title)
                .ToList();

        return result;
    }

    /// <summary>
    /// Retrieves the top readers who borrowed the most books within a specified date range.
    /// Results are ranked by loan count in descending order, then by last name.
    /// </summary>
    /// <param name="periodBegin">Start date of the analysis period (inclusive).</param>
    /// <param name="periodEnd">End date of the analysis period (inclusive).</param>
    /// <param name="resultsCount">Maximum number of results to return.</param>
    /// <returns>List of readers with highest loan counts, limited to resultsCount.</returns>
    public async Task<List<ReaderLoanCountDto>> GetTopReadersByLoanCountAsync(
        DateOnly periodBegin,
        DateOnly periodEnd,
        int resultsCount)
    {
        var loanRecords = await loanRecordRepository.ReadAllAsync();
        var readers = await readerRepository.ReadAllAsync();

        var topReaders = loanRecords
            .Where(lr => lr.IssueDate >= periodBegin && lr.IssueDate <= periodEnd)
            .GroupBy(lr => lr.ReaderId)
            .Join(
                readers,
                lr => lr.Key,
                r => r.Id,
                (lr, r) =>
                {
                    var reader = mapper.Map<ReaderLoanCountDto>(r);
                    reader.LoanCount = lr.Count();
                    return reader;
                }
            )
            .OrderByDescending(r => r.LoanCount)
            .ThenBy(r => r.LastName)
            .Take(resultsCount)
            .ToList();

        return topReaders;
    }

    /// <summary>
    /// Retrieves readers with the longest single loan duration across their borrowing history.
    /// Only readers with the maximum observed loan term are included.
    /// Results are ordered by full name (last, first, patronymic).
    /// </summary>
    /// <returns>List of readers with the longest loan term, ordered by name.</returns>
    public async Task<List<ReaderLoanDurationDto>> GetTopReadersByLongestLoanTermOrderedByNameAsync()
    {
        var loanRecords = await loanRecordRepository.ReadAllAsync();
        var readers = await readerRepository.ReadAllAsync();

        var maxTerm = loanRecords.Max(lr => lr.LoanTerm);

        var topReaders = loanRecords
            .Where(x => x.LoanTerm == maxTerm)
            .GroupBy(lr => lr.ReaderId)
            .Join(
                readers,
                lr => lr.Key,
                r => r.Id,
                (lr, r) =>
                {
                    var reader = mapper.Map<ReaderLoanDurationDto>(r);
                    reader.Duration = lr.Max(x => x.LoanTerm);
                    return reader;
                }
            )
            .OrderBy(r => r.LastName).ThenBy(r => r.FirstName).ThenBy(r => r.PatronymicName)
            .ToList();

        return topReaders;
    }

    /// <summary>
    /// Retrieves publishers with the highest number of book loans within a specified period.
    /// Results are ranked by total loan count in descending order, then by publisher name.
    /// </summary>
    /// <param name="periodBegin">Start date of the analysis period (inclusive).</param>
    /// <param name="periodEnd">End date of the analysis period (inclusive).</param>
    /// <param name="resultsCount">Maximum number of results to return.</param>
    /// <returns>List of publishers with highest loan counts, limited to resultsCount.</returns>
    public async Task<List<PublisherLoanCountDto>> GetTopPublishersByLoanCountAsync(
        DateOnly periodBegin,
        DateOnly periodEnd,
        int resultsCount)
    {
        var loanRecords = await loanRecordRepository.ReadAllAsync();
        var books = await bookRepository.ReadAllAsync();

        var topPublishers = loanRecords
            .Where(lr => lr.IssueDate >= periodBegin && lr.IssueDate <= periodEnd)
            .Join(
                books,
                lr => lr.BookId,
                b => b.Id,
                (lr, b) => b.Publisher.ToString()
            )
            .GroupBy(p => p)
            .Select(g => new PublisherLoanCountDto
            {
                PublisherName = g.Key,
                LoanCount = g.Count()
            })
            .OrderByDescending(dto => dto.LoanCount)
            .ThenBy(dto => dto.PublisherName)
            .Take(resultsCount)
            .ToList();

        return topPublishers;
    }

    /// <summary>
    /// Retrieves books with the lowest loan counts within a specified period.
    /// Results are ranked by loan count in ascending order, then by title.
    /// </summary>
    /// <param name="periodBegin">Start date of the analysis period (inclusive).</param>
    /// <param name="periodEnd">End date of the analysis period (inclusive).</param>
    /// <param name="resultsCount">Maximum number of results to return.</param>
    /// <returns>List of least popular books, limited to resultsCount.</returns>
    public async Task<List<BookLoanCountDto>> GetBooksByLowestLoanCountAsync(
        DateOnly periodBegin,
        DateOnly periodEnd,
        int resultsCount)
    {
        var loanRecords = await loanRecordRepository.ReadAllAsync();
        var books = await bookRepository.ReadAllAsync();

        var topBooks = loanRecords
            .Where(lr => lr.IssueDate >= periodBegin && lr.IssueDate <= periodEnd)
            .GroupBy(lr => lr.BookId)
            .Join(
                books,
                lr => lr.Key,
                r => r.Id,
                (lr, r) =>
                {
                    var book = mapper.Map<BookLoanCountDto>(r);
                    book.LoanCount = lr.Count();
                    return book;
                }
            )
            .OrderBy(b => b.LoanCount)
            .ThenBy(b => b.Title)
            .Take(resultsCount)
            .ToList();

        return topBooks;
    }
}
