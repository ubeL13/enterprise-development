namespace BikeRental.Contracts.Dtos
{
    /// <summary>
    /// Data transfer object for updating an existing bike rental.
    /// </summary>
    public class RentalUpdateDto
    {
        /// <summary>
        /// Unique identifier of the rental.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the rented bike.
        /// </summary>
        public string BikeId { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the renter.
        /// </summary>
        public string RenterId { get; set; } = string.Empty;

        /// <summary>
        /// Start time of the rental.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Duration of the rental in hours.
        /// </summary>
        public int DurationHours { get; set; }
    }
}
