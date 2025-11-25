namespace BikeRental.Api.DTO;

/// <summary>
/// Data transfer object representing a rental.
/// </summary>
public class RentalDto
{
    /// <summary>
    /// Unique identifier of the rental.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Identifier of the bike being rented.
    /// </summary>
    public string BikeId { get; set; }

    /// <summary>
    /// Identifier of the renter.
    /// </summary>
    public string RenterId { get; set; }

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }
}
