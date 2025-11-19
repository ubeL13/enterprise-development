namespace BikeRental.Api.DTO;

public class BikeDto
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public Guid ModelId { get; set; }
}
