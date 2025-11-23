namespace BikeRental.Api.DTO;

public class BikeDto
{
    public string Id { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ModelId { get; set; }
}
