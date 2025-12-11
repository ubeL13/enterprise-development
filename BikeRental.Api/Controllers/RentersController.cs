using BikeRental.Api.DTO;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing renters in the bike rental system.
/// </summary>
/// <remarks>
/// Initializes a new instance of the controller.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class RentersController(IRepository<Renter> renters) : ControllerBase
{
    private readonly IRepository<Renter> _renters = renters;

    /// <summary>
    /// Retrieves all renters.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _renters.GetAllAsync());

    /// <summary>
    /// Creates a new renter.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(RenterCreateDto dto)
    {
        var renter = new Renter
        {
            FullName = dto.FullName,
            Phone = dto.Phone
        };

        await _renters.CreateAsync(renter);
        return Ok(renter);
    }
}
