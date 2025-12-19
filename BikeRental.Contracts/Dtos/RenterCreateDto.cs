using System.ComponentModel.DataAnnotations;

namespace BikeRental.Contracts.Dtos;

/// <summary>
/// Data transfer object for creating a new renter.
/// </summary>
public class RenterCreateDto
{
    /// <summary>
    /// Full name of the renter.
    /// </summary>
    [Required(ErrorMessage = "FullName is required")]
    [MinLength(3, ErrorMessage = "FullName must be at least 3 characters long")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of the renter.
    /// </summary>
    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string Phone { get; set; } = string.Empty;
}
