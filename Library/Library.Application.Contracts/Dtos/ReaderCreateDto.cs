namespace Library.Application.Contracts.Dtos;

/// <summary>
/// Data Transfer Object for creating a new reader in the library system.
/// </summary>
public class ReaderCreateDto
{
    public required string FirstName { get; set; }

    /// <summary>
    /// Last Name of reader
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Patronymic Name of reader
    /// </summary>
    public required string PatronymicName { get; set; }

    /// <summary>
    /// Address of reader
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Phone number of reader
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Date of reader registration
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }
}
