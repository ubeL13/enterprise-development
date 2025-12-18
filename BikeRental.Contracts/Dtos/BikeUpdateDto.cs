namespace BikeRental.Contracts.Dtos;

public class BikeUpdateDto
{
    public string Id { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
}
