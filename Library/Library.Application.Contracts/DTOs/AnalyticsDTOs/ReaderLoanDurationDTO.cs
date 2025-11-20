namespace Library.Application.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// Data transfer object representing a reader with their maximum loan duration.
/// Contains full reader details and the longest single loan period they have taken.
/// Used for ranking readers by book holding time in library analytics.
/// </summary>
public class ReaderLoanDurationDto
{
    /// <summary>
    /// Unique identifier of the reader.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// First name of the reader.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Last name of the reader.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Patronymic (middle) name of the reader.
    /// </summary>
    public required string PatronymicName { get; set; }

    /// <summary>
    /// Residential address of the reader.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Contact phone number of the reader.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Date when the reader registered in the library system.
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }

    /// <summary>
    /// Maximum loan term (in days) this reader has taken for a single book.
    /// Used to rank readers by the length of time they hold books.
    /// </summary>
    public required int Duration { get; set; }
}