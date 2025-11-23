namespace BikeRental.Api.DTO;

public class BikeModelDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int WheelSize { get; set; }
    public double MaxRiderWeight { get; set; }
    public double BikeWeight { get; set; }
    public string BrakeType { get; set; } = string.Empty;
    public int ModelYear { get; set; }
    public decimal HourlyRate { get; set; }
}
