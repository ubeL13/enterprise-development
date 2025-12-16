using BikeRental.Domain.Enums;

namespace BikeRental.Domain.Models;

/// <summary>
/// Represents a bike model with its technical specifications and rental price.
/// </summary>
public class BikeModel
{
    /// <summary>
    /// Unique identifier of the bike model.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();


    /// <summary>
    /// Name of the model
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Type of the bike 
    /// </summary>
    public required BikeType Type { get; set; }

    /// <summary>
    /// Diameter of the wheels in inches
    /// </summary>
    public int WheelSize { get; set; }

    /// <summary>
    /// Maximum rider weight allowed 
    /// </summary>
    public double MaxRiderWeight { get; set; }

    /// <summary>
    /// Weight of the bike
    /// </summary>
    public double BikeWeight { get; set; }

    /// <summary>
    /// Type of brakes 
    /// </summary>
    public string? BrakeType { get; set; }

    /// <summary>
    /// Model release year
    /// </summary>
    public int ModelYear { get; set; }

    /// <summary>
    /// Rental price per hour
    /// </summary>
    public decimal HourlyRate { get; set; }
}
