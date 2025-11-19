namespace BikeRental.Api.DTO;

public class RentalDto
{
    public Guid Id { get; set; }
    public Guid BikeId { get; set; }
    public Guid RenterId { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationHours { get; set; }
}
