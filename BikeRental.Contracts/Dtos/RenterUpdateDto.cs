namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object representing a renter.
/// </summary>
public class RenterUpdateDto
{
    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    public required string Id { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public required string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of the renter.
    /// </summary>
    public required string Phone{ get; set; } = string.Empty;
}
