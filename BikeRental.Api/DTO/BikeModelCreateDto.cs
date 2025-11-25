namespace BikeRental.Api.DTO;

/// <summary>
/// Data transfer object for creating a new bike model.
/// </summary>
public class BikeModelCreateDto
{
    /// <summary>
    /// Name of the bike model.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Size of the bike wheels in inches.
    /// </summary>
    public int WheelSize { get; set; }

    /// <summary>
    /// Maximum rider weight supported by the bike.
    /// </summary>
    public double MaxRiderWeight { get; set; }

    /// <summary>
    /// Weight of the bike.
    /// </summary>
    public double BikeWeight { get; set; }

    /// <summary>
    /// Type of brakes used on the bike.
    /// </summary>
    public string BrakeType { get; set; } = string.Empty;

    /// <summary>
    /// Year of the bike model.
    /// </summary>
    public int ModelYear { get; set; }

    /// <summary>
    /// Hourly rental rate for the bike model.
    /// </summary>
    public decimal HourlyRate { get; set; }
}
