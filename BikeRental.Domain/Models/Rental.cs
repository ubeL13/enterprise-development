namespace BikeRental.Domain.Models;

/// <summary>
/// Represents a rental record — when a renter takes a bike for a certain period.
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier of the rental.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The bike that was rented.
    /// </summary>
    public required string BikeId { get; set; }

    public Bike? Bike { get; set; }

    /// <summary>
    /// The renter who took the bike.
    /// </summary>
    public required string RenterId { get; set; }

    public Renter? Renter { get; set; }

    /// <summary>
    /// The start date and time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }
}
