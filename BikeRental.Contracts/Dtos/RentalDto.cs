namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object representing a rental.
/// </summary>
public class RentalDto
{
    /// <summary>
    /// Unique identifier of the rental.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Information about bike.
    /// </summary>
    public required string BikeId { get; set; }

    /// <summary>
    /// Identifier of the bike being rented.
    /// </summary>
    public BikeDto? Bike { get; set; }

    /// <summary>
    /// Identifier of the renter.
    /// </summary>
    public required string RenterId { get; set; }

    /// <summary>
    /// Information about renter.
    /// </summary>
    public RenterDto? Renter { get; set; }

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }
}
