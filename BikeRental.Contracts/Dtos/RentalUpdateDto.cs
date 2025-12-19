using System.ComponentModel.DataAnnotations;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for updating an existing bike rental.
/// </summary>
public class RentalUpdateDto
{
    /// <summary>
    /// Unique identifier of the rental.
    /// </summary>
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the rented bike.
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
