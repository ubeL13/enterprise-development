using System.ComponentModel.DataAnnotations;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for creating a new bike.
/// </summary>
public class BikeCreateDto
{
    /// <summary>
    /// Serial number of the bike.
    /// </summary>
    [Required(ErrorMessage = "Serial number is required")]
    [StringLength(50, ErrorMessage = "Serial number cannot exceed 50 characters")]
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Color of the bike.
    /// </summary>
    [Required(ErrorMessage = "Color is required")]
    [StringLength(30, ErrorMessage = "Color cannot exceed 30 characters")]
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the bike model.
    /// </summary>
    [Required(ErrorMessage = "ModelId is required")]
    public required string ModelId { get; set; }
}
