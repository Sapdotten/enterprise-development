namespace Library.Application.Contracts.Dtos.AnalyticsDtos;

/// <summary>
/// Data transfer object representing a reader with their loan count.
/// Contains full reader details and the total number of books they have borrowed.
/// </summary>
public class ReaderLoanCountDto
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
    /// Total number of books this reader has borrowed.
    /// </summary>
    public required int LoanCount { get; set; }
}