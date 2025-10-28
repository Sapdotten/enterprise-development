namespace Library.Domain.Entities;

/// <summary>
/// Represents a library reader (patron) with personal information and contact details.
/// </summary>
public class Reader
{
    public int Id { get; set; }
    /// <summary>
    /// First Name of reader
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Second Name of reader
    /// </summary>
    public required string SecondName { get; set; }

    /// <summary>
    /// Last Name of reader
    /// </summary>
    public required string LastName { get; set; }

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
