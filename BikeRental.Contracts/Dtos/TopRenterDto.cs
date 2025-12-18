namespace BikeRental.Contracts.Dtos
{
    /// <summary>
    /// Data transfer object representing a renter with the highest number of rentals.
    /// </summary>
    public class TopRenterDto
    {
        /// <summary>
        /// Name or identifier of the renter.
        /// </summary>
        public string Renter { get; set; } = string.Empty;

        /// <summary>
        /// Total number of rentals made by the renter.
        /// </summary>
        public int Count { get; set; }
    }
}
