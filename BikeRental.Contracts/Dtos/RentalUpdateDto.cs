namespace BikeRental.Contracts.Dtos;

public class RentalUpdateDto
{
    public string Id { get; set; } = string.Empty; 
    public string BikeId { get; set; } = string.Empty;
    public string RenterId { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int DurationHours { get; set; }
}
