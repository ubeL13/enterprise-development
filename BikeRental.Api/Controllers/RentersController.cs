using BikeRental.Api.DTOs;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentersController : ControllerBase
{
    private readonly IRepository<Renter> _renters;

    public RentersController(IRepository<Renter> renters)
    {
        _renters = renters;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _renters.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(RenterDto dto)
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
