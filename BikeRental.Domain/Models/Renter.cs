namespace BikeRental.Domain.Models;

/// <summary>
/// Represents a person who rents bikes.
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number of the renter.
    /// </summary>
    public required string Phone { get; set; }
}
