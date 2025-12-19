using System.ComponentModel.DataAnnotations;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for creating a new rental.
/// </summary>
public class RentalCreateDto
{
    /// <summary>
    /// Identifier of the bike being rented.
    /// </summary>
    [Required(ErrorMessage = "BikeId is required")]
    public string BikeId { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the renter.
    /// </summary>
    [Required(ErrorMessage = "RenterId is required")]
    public string RenterId { get; set; } = string.Empty;

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    [Required(ErrorMessage = "StartTime is required")]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    [Range(1, 168, ErrorMessage = "Duration must be between 1 and 168 hours")]
    public int DurationHours { get; set; }
}
