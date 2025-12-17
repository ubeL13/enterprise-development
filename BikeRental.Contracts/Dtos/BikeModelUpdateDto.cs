using BikeRental.Domain.Enums;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for updating an existing bike model.
/// </summary>
public class BikeModelUpdateDto
{
    /// <summary>
    /// Unique identifier of the bike model.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Name of the bike model.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Type of the bike (e.g., Sport, City, Mountain).
    /// </summary>
    public required BikeType Type { get; set; }

    public int WheelSize { get; set; }
    public double BikeWeight { get; set; }
    public double MaxRiderWeight { get; set; }
    public string BrakeType { get; set; } = string.Empty;
    public int ModelYear { get; set; }
    public decimal HourlyRate { get; set; }
}
