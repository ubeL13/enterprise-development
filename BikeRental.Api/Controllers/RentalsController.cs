using BikeRental.Api.DTOs;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRepository<Rental> _rentals;

    public RentalsController(IRepository<Rental> rentals)
    {
        _rentals = rentals;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _rentals.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(RentalDto dto)
    {
        var rental = new Rental
        {
            Bike = new Bike { Id = dto.BikeId },
            Renter = new Renter { Id = dto.RenterId },
            StartTime = dto.StartTime,
            DurationHours = dto.DurationHours
        };

        await _rentals.CreateAsync(rental);
        return Ok(rental);
    }
}
