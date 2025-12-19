namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object representing statistics for bike rental durations.
/// </summary>
public class RentalDurationStatsDto
{
    /// <summary>
    /// Minimum rental duration.
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// Maximum rental duration.
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// Average rental duration.
    /// </summary>
    public double Avg { get; set; }
}
