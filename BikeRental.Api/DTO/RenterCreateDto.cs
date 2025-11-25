namespace BikeRental.Api.DTO;

/// <summary>
/// Data transfer object for creating a new renter.
/// </summary>
public class RenterCreateDto
{
    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of the renter.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
