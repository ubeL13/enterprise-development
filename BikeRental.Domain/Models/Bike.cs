namespace BikeRental.Domain.Models
{
    /// <summary>
    /// Represents a specific bike instance available for rent.
    /// </summary>
    public class Bike
    {
        /// <summary>
        /// Unique identifier of the bike.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
        public required BikeModel Model { get; set; }
    }
}
