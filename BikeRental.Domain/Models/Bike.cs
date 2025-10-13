namespace BikeRental.Domain.Models
{
    public class Bike
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string SerialNumber { get; set; }
        public required string Color { get; set; }
        public required BikeModel Model { get; set; }
    }

}
