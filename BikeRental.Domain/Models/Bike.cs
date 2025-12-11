using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BikeRental.Domain.Models;

/// <summary>
/// Represents a specific bike instance available for rent.
/// </summary>
public class Bike
{
    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Serial number assigned by the manufacturer.
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Color of the bike.
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Model describing the technical characteristics and pricing of this bike.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public required string ModelId { get; set; }

    [BsonIgnore]
    public BikeModel? Model { get; set; }
}
