using System.ComponentModel.DataAnnotations;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for updating an existing bike.
/// </summary>
public class BikeUpdateDto
{
    /// <summary>
    /// Unique identifier of the bike.
    /// </summary>
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; } = string.Empty;

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
    public string ModelId { get; set; } = string.Empty;
}
