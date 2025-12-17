namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object representing a bike.
/// </summary>
public class BikeDto
{
    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Serial number of the bike.
    /// </summary>
    public required string SerialNumber { get; set; }


    /// <summary>
    /// Color of the bike.
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Identifier of the bike model.
    /// </summary>
    public required string ModelId { get; set; }
    public BikeModelDto? Model { get; set; }

}
