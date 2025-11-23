using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BikeRental.Domain.Models;

/// <summary>
/// Represents a rental record — when a renter takes a bike for a certain period.
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier of the rental.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// The bike that was rented.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string BikeId { get; set; }

    [BsonIgnore]
    public Bike? Bike { get; set; }

    /// <summary>
    /// The renter who took the bike.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string RenterId { get; set; }

    [BsonIgnore]
    public Renter? Renter { get; set; }

    /// <summary>
    /// The start date and time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }
}
