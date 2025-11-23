using BikeRental.Api.DTO;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRepository<Rental> _rentals;
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<Renter> _renters;

    public RentalsController(
        IRepository<Rental> rentals,
        IRepository<Bike> bikes,
        IRepository<Renter> renters)
    {
        _rentals = rentals;
        _bikes = bikes;
        _renters = renters;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _rentals.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(RentalDto dto)
    {
        var bike = await _bikes.GetByIdAsync(dto.BikeId);
        if (bike == null)
            return NotFound($"Bike with id {dto.BikeId} not found");

        var renter = await _renters.GetByIdAsync(dto.RenterId);
        if (renter == null)
            return NotFound($"Renter with id {dto.RenterId} not found");

        var rental = new Rental
        {
            Bike = bike,
            Renter = renter,
            StartTime = dto.StartTime,
            DurationHours = dto.DurationHours
        };

        await _rentals.CreateAsync(rental);
        return Ok(rental);
    }
}
