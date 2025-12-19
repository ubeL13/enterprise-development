using System.ComponentModel.DataAnnotations;
using BikeRental.Domain.Enums;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for creating a new bike model.
/// </summary>
public class BikeModelCreateDto
{
    /// <summary>
    /// Name of the bike model.
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Type of the bike.
    /// </summary>
    [Required(ErrorMessage = "Type is required")]
    public BikeType Type { get; set; }

    /// <summary>
    /// Size of the bike wheels in inches.
    /// </summary>
    [Range(10, 40, ErrorMessage = "Wheel size must be between 10 and 40 inches")]
    public int WheelSize { get; set; }

    /// <summary>
    /// Maximum rider weight supported by the bike.
    /// </summary>
    [Range(20, 200, ErrorMessage = "Max rider weight must be between 20 and 200 kg")]
    public double MaxRiderWeight { get; set; }

    /// <summary>
    /// Weight of the bike.
    /// </summary>
    [Range(5, 50, ErrorMessage = "Bike weight must be between 5 and 50 kg")]
    public double BikeWeight { get; set; }

    /// <summary>
    /// Type of brakes used on the bike.
    /// </summary>
    [StringLength(50, ErrorMessage = "Brake type cannot exceed 50 characters")]
    public string? BrakeType { get; set; }

    /// <summary>
    /// Year of the bike model.
    /// </summary>
    [Range(1990, 2100, ErrorMessage = "Model year must be between 1990 and 2100")]
    public int ModelYear { get; set; }

    /// <summary>
    /// Hourly rental rate for the bike model.
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be non-negative")]
    public decimal HourlyRate { get; set; }
}
