namespace BikeRental.Api.DTO;

/// <summary>
/// Data transfer object representing a renter.
/// </summary>
public class RenterDto
{
    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of the renter.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
