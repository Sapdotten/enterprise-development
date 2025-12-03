using Bogus;
using Library.Application.Contracts.Dtos;

namespace Library.Generator.Kafka;

/// <summary>
/// Generates random test data for LoanRecordCreateDto instances.
/// Uses Bogus library to create realistic fake values.
/// </summary>
public static class LoanRecordGenerator
{
    /// <summary>
    /// Generates a list of random LoanRecordCreateDto objects.
    /// </summary>
    /// <param name="count">Number of DTOs to generate.</param>
    /// <returns>List of generated DTOs.</returns>
    public static List<LoanRecordCreateDto> GenerateLinks(int count) =>
        new Faker<LoanRecordCreateDto>()
            .RuleFor(x => x.BookId, f => f.Random.Int(1, 11))
            .RuleFor(x => x.ReaderId, f => f.Random.Int(1, 10))
            .RuleFor(x => x.IssueDate, f =>
            {
                var dt = f.Date.Past(1);
                return new DateOnly(dt.Year, dt.Month, dt.Day);
            })
            .RuleFor(x => x.LoanTerm, f => f.Random.Int(1, 30))
            .Generate(count);
}