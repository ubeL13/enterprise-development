namespace BikeRental.Api.DTO;

public class RentalDto
{
    public string Id { get; set; }
    public string BikeId { get; set; }
    public string RenterId { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationHours { get; set; }
}
