namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for creating a new bike.
/// </summary>
public class BikeCreateDto
{
    /// <summary>
    /// Serial number of the bike.
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Color of the bike.
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the bike model.
    /// </summary>
    public required string ModelId { get; set; }
}
