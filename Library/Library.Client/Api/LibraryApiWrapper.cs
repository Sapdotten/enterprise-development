namespace Library.Client.Api;

public class LibraryApiWrapper(IConfiguration configuration)
{
    private readonly LibraryClient _client = new(configuration["OpenApi:ServerUrl"], new HttpClient());

    public async Task<ReaderGetDto> CreateReader(ReaderCreateDto newReader) => await _client.ReaderPOSTAsync(newReader);
    public async Task<ReaderGetDto> GetReader(int id) => await _client.ReaderGETAsync(id);
    public async Task<IList<ReaderGetDto>> GetAllReaders() => [.. await _client.ReaderAllAsync()];
    public async Task<ReaderGetDto> UpdateReader(int id, ReaderCreateDto updatedReader) => await _client.ReaderPUTAsync(id, updatedReader);
    public async Task DeleteReader(int id) => await _client.ReaderDELETEAsync(id);

    public async Task<BookGetDto> CreateBook(BookCreateDto newBook) => await _client.BookPOSTAsync(newBook);
    public async Task<BookGetDto> GetBook(int id) => await _client.BookGETAsync(id);
    public async Task<IList<BookGetDto>> GetAllBooks() => [.. await _client.BookAllAsync()];
    public async Task<BookGetDto> UpdateBook(int id, BookCreateDto updatedBook) => await _client.BookPUTAsync(id, updatedBook);
    public async Task DeleteBook(int id) => await _client.BookDELETEAsync(id);

    public async Task<LoanRecordGetDto> CreateLoanRecord(LoanRecordCreateDto newLoanRecord) => await _client.LoanRecordPOSTAsync(newLoanRecord);
    public async Task<LoanRecordGetDto> GetLoanRecord(int id) => await _client.LoanRecordGETAsync(id);
    public async Task<IList<LoanRecordGetDto>> GetAllLoanRecords() => [.. await _client.LoanRecordAllAsync()];
    public async Task<LoanRecordGetDto> UpdateLoanRecord(int id, LoanRecordCreateDto updatedLoanRecord) => await _client.LoanRecordPUTAsync(id, updatedLoanRecord);
    public async Task DeleteLoanRecord(int id) => await _client.LoanRecordDELETEAsync(id);


    public async Task<IList<BookLoanCountDto>> GetLoanedBooks() => [.. await _client.LoanedBooksAsync()];


    public async Task<IList<ReaderLoanCountDto>> GetTopReadersByLoanCount(DateOnly start, DateOnly end, int resultCount = 5) =>
    [.. await _client.TopReadersByLoanedBooksCountAsync(
        start: start.ToDateTime(TimeOnly.MinValue),
        end: end.ToDateTime(TimeOnly.MinValue),
        resultCount: resultCount)];


    public async Task<IList<ReaderLoanDurationDto>> GetTopReadersByLongestLoan() =>
        [.. await _client.TopReadersByLongestLoanAsync()];


    public async Task<IList<PublisherLoanCountDto>> GetTopPublishersByLoanCount(DateOnly start, DateOnly end, int resultCount = 5) =>
        [.. await _client.TopPublishersByLoanCountAsync(
            start: start.ToDateTime(TimeOnly.MinValue),
            end: end.ToDateTime(TimeOnly.MinValue),
            resultCount)];


    public async Task<IList<BookLoanCountDto>> GetLeastPopularBooks(DateOnly start, DateOnly end, int resultCount = 5) =>
        [.. await _client.LeastPopularBooksAsync(
            start: start.ToDateTime(TimeOnly.MinValue),
            end: end.ToDateTime(TimeOnly.MinValue),
            resultCount)];
}
