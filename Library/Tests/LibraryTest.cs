using Library.Domain.Enums;
using System.Threading.Tasks;

namespace Library.Tests;

public class LibraryTest(LibraryFixture fixture) : IClassFixture<LibraryFixture>
{
    [Fact]
    public async Task GetBorrowedBooks()
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
        var loanRecords = await fixture.LoanRecords.ReadAllAsync();
        var books = await fixture.Books.ReadAllAsync();

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

    [Fact]
    public async Task GetMostActiveReadersDuringPeriod()
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
        var loanRecords = await fixture.LoanRecords.ReadAllAsync();
        var readers = await fixture.Readers.ReadAllAsync();

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

    [Fact]
    public async Task GetReadersWithLongestLoan()
    {
        var expected = new[]
        {
            "Алехин Иван Игоревич",
            "Волков Александр Юрьевич"
        };
        var loanRecords = await fixture.LoanRecords.ReadAllAsync();
        var readers = await fixture.Readers.ReadAllAsync();

        var maxTerm = loanRecords.Max(lr => lr.LoanTerm);

        var actual = loanRecords
            .Where(x => x.LoanTerm == maxTerm)
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

    [Fact]
    public async Task GetMostPopularPublisher()
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
        var loanRecords = await fixture.LoanRecords.ReadAllAsync();
        var books = await fixture.Books.ReadAllAsync();

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

    [Fact]
    public async Task GetLeastPopularBooks()
    {
        var expected = new[]
        {
            "Библия Сатаны",
            "Молот ведьм",
            "1Q84",
            "Ампир V",
            "Архитектура компьютера"
        };
        var startDate = new DateOnly(2024, 10, 5);
        var endDate = new DateOnly(2025, 10, 5);
        var loanRecords = await fixture.LoanRecords.ReadAllAsync();
        var books = await fixture.Books.ReadAllAsync();

        var loanCounts = loanRecords
            .GroupBy(lr => lr.BookId)
            .ToDictionary(g => g.Key, g => g.Count());

        var actual = books
            .Select(b => new
            {
                b.Title,
                Count = loanCounts.TryGetValue(b.Id, out var count) ? count : 0
            })
            .OrderBy(x => x.Count)
            .ThenBy(x => x.Title)
            .Take(5)
            .Select(x => x.Title)
            .ToList();

        Assert.Equal(expected, actual);
    }
}