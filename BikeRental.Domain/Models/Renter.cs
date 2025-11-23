namespace BikeRental.Domain.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/// <summary>
/// Represents a person who rents bikes.
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier of the renter.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number of the renter.
    /// </summary>
    public required string Phone { get; set; }
}
