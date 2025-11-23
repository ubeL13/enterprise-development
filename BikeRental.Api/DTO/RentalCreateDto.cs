namespace BikeRental.Api.DTO;

public class RentalCreateDto
{
    public string BikeId { get; set; }
    public string RenterId { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationHours { get; set; }
}
