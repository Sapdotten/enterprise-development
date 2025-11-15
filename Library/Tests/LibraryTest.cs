using Library.Domain.Enums;

namespace Library.Tests;

public class LibraryTest(LibraryFixture fixture) : IClassFixture<LibraryFixture>
{
    /// <summary>
    /// Gets the list of borrowed books, ordered alphabetically by title.
    /// </summary>
    [Fact]
    public void GetBorrowedBooks()
    {
        var expected = new[]
        {
            "1Q84",
            "Ампир V",
            "Архитектура компьютера",
            "Задачник по физике",
            "Игра престолов",
            "Пикник на обочине",
            "Практическая информационная безопасность",
            "Трансгуманизм Inc.",
            "Чапаев и Пустота"
        };
        var loanRecords = fixture.LoanRecords.ReadAll();
        var books = fixture.Books.ReadAll();

        var actual = loanRecords
            .Join(
                books,
                lr => lr.BookId,
                b => b.Id,
                (lr, b) => b.Title
            )
            .Distinct()
            .Order()
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Gets the top 5 most active readers who borrowed the highest number of books during a certain period 
    /// </summary>
    [Fact]
    public void GetMostActiveReadersDuringPeriod()
    {
        var expected = new[]
        {
            "Волков Александр Юрьевич",
            "Алехин Иван Игоревич",
            "Домнин Никита Михайлович",
            "Иванов Даниил Александрович",
            "Панявкин Андрей Сергеевич"
        };
        var startDate = new DateOnly(2025, 1, 1);
        var endDate = new DateOnly(2025, 3, 25);
        var loanRecords = fixture.LoanRecords.ReadAll();
        var readers = fixture.Readers.ReadAll();

        var actual = loanRecords
            .Where(lr => lr.IssueDate >= startDate && lr.IssueDate <= endDate)
            .GroupBy(lr => lr.ReaderId)
            .Select(g => new
            {
                ReaderId = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Join(
                readers,
                result => result.ReaderId,
                reader => reader.Id,
                (result, reader) => $"{reader.LastName} {reader.FirstName} {reader.PatronymicName}"
            )
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Gets readers who borrowed books for the longest loan period, ordered by full name (second name, first name, last name).
    /// </summary>
    [Fact]
    public void GetReadersWithLongestLoan()
    {
        var expected = new[]
        {
            "Алехин Иван Игоревич",
            "Волков Александр Юрьевич"
        };
        var loanRecords = fixture.LoanRecords.ReadAll();
        var readers = fixture.Readers.ReadAll();

        var actual = loanRecords
            .Where(x => x.LoanTerm == loanRecords.Max(lr => lr.LoanTerm))
            .Join(
                readers,
                lr => lr.ReaderId,
                r => r.Id,
                (lr, r) => new
                {
                    r.LastName,
                    r.FirstName,
                    r.PatronymicName
                }
            )
            .Distinct()
            .Select(r => $"{r.LastName} {r.FirstName} {r.PatronymicName}")
            .Order()
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Gets the top 5 most popular publishers over the last year, based on the number of book loans.
    /// </summary>
    [Fact]
    public void GetMostPopularPublisher()
    {
        var expected = new[]
        {
            Publisher.AST,
            Publisher.Binom,
            Publisher.Eksmo,
            Publisher.Mir,
            Publisher.AdMarginem
        };
        var startDate = new DateOnly(2024, 10, 5);
        var endDate = new DateOnly(2025, 10, 5);
        var loanRecords = fixture.LoanRecords.ReadAll();
        var books = fixture.Books.ReadAll();

        var actual = loanRecords
            .Where(lr => lr.IssueDate >= startDate && lr.IssueDate <= endDate)
            .Join(
                books,
                lr => lr.BookId,
                b => b.Id,
                (lr, b) => b.Publisher
            )
            .GroupBy(p => p)
            .Select(g => new
            {
                Publisher = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Publisher)
            .Select(x => x.Publisher)
            .Take(5)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Gets the top 5 least popular books over the last year, including those with zero loans, ordered by loan count and then by title.
    /// </summary>
    [Fact]
    public void GetLeastPopularBooks()
    {
        var expected = new[]
        {
            "1Q84",
            "Ампир V",
            "Архитектура компьютера",
            "Игра престолов",
            "Пикник на обочине"
        };
        var startDate = new DateOnly(2024, 10, 5);
        var endDate = new DateOnly(2025, 10, 5);
        var loanRecords = fixture.LoanRecords.ReadAll();
        var books = fixture.Books.ReadAll();

        var actual = loanRecords
            .GroupBy(lr => lr.BookId)
            .Select(g => new
            {
                book_id = g.Key,
                count = g.Count()

            })
            .Join(
            books,
            g => g.book_id,
            b => b.Id,
            (g, b) => new
            {
                b.Title,
                g.count
            })
            .OrderBy(x => x.count)
            .ThenBy(x => x.Title)
            .Take(5)
            .Select(x => x.Title)
            .ToList();

        Assert.Equal(expected, actual);
    }
}