namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object representing a bike.
/// </summary>
public class BikeUpdateDto
{
    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    public required string Id { get; set; } = string.Empty;

    /// <summary>
    /// Serial number of the bike.
    /// </summary>
    public required string SerialNumber { get; set; } = string.Empty;


    /// <summary>
    /// Color of the bike.
    /// </summary>
    public required string Color { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the bike model.
    /// </summary>
    public required string ModelId { get; set; } = string.Empty;

    /// <summary>
    /// Model of the bike.
    /// </summary>
    public BikeModelDto? Model { get; set; }

}
