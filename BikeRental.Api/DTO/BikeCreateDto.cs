namespace BikeRental.Api.DTO;

public class BikeCreateDto
{
    public string SerialNumber { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public Guid ModelId { get; set; }
}
