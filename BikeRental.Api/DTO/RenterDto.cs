namespace BikeRental.Api.DTO;

/// <summary>
/// Data transfer object representing a renter.
/// </summary>
public class RenterDto
{
    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public required string FullName
    {
        get; set;
    }

    /// <summary>
    /// Phone number of the renter.
    /// </summary>
    public required string Phone
    {
        get; set;
    }
}
