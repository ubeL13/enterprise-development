namespace BikeRental.Api.DTO;

/// <summary>
/// Data transfer object representing a bike.
/// </summary>
public class BikeDto
{
    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    public string Id { get; set; }

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
    public string ModelId { get; set; }
    public BikeModelDto? Model { get; set; }

}
