namespace Library.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a library reader (patron) with personal information and contact details.
/// </summary>
[Table("readers")]
public class Reader
{
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// First Name of reader
    /// </summary>
    [Column("first_name")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Last Name of reader
    /// </summary>
    [Column("last_name")]
    public required string LastName { get; set; }

    /// <summary>
    /// Patronymic Name of reader
    /// </summary>
    [Column("patronymic_name")]
    public required string PatronymicName { get; set; }

    /// <summary>
    /// Address of reader
    /// </summary>
    [Column("address")]
    public required string Address { get; set; }

    /// <summary>
    /// Phone number of reader
    /// </summary>
    [Column("phone_number")]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Date of reader registration
    /// </summary>
    [Column("registration_date")]
    public required DateOnly RegistrationDate { get; set; }
}
