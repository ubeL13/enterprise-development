using BikeRental.Domain.Enums;

namespace BikeRental.Contracts.Dtos
{
    /// <summary>
    /// Data transfer object for updating an existing bike model.
    /// </summary>
    public class BikeModelUpdateDto
    {
        /// <summary>
        /// Unique identifier of the bike model.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Name of the bike model.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Type of the bike (e.g., Sport, City, Mountain).
        /// </summary>
        public required BikeType Type { get; set; }

        /// <summary>
        /// Diameter of the bike wheels in inches.
        /// </summary>
        public int WheelSize { get; set; }

        /// <summary>
        /// Weight of the bike in kilograms.
        /// </summary>
        public double BikeWeight { get; set; }

        /// <summary>
        /// Maximum rider weight supported by the bike in kilograms.
        /// </summary>
        public double MaxRiderWeight { get; set; }

        /// <summary>
        /// Type of brakes installed on the bike.
        /// </summary>
        public string BrakeType { get; set; } = string.Empty;

        /// <summary>
        /// Manufacturing year of the bike model.
        /// </summary>
        public int ModelYear { get; set; }

        /// <summary>
        /// Hourly rental rate for the bike.
        /// </summary>
        public decimal HourlyRate { get; set; }
    }
}
