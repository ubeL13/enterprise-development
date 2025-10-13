using BikeRental.Domain.Enums;

namespace BikeRental.Domain.Models
{
    public class BikeModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required BikeType Type { get; set; }
        public int WheelSize { get; set; }
        public double MaxRiderWeight { get; set; }
        public double BikeWeight { get; set; }
        public string BrakeType { get; set; } = string.Empty;
        public int ModelYear { get; set; }
        public decimal HourlyRate { get; set; }
    }

}
