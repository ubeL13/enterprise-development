using BikeRental.Api.DTO;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Api.Extensions;


namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing rentals in the bike rental system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRepository<Rental> _rentals;
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<Renter> _renters;
    private readonly IRepository<BikeModel> _models;

    /// <summary>
    /// Initializes a new instance of the RentalsController.
    /// </summary>
    public RentalsController(
        IRepository<Rental> rentals,
        IRepository<Bike> bikes,
        IRepository<Renter> renters,
        IRepository<BikeModel> models)
    {
        _rentals = rentals;
        _bikes = bikes;
        _renters = renters;
        _models = models;
    }

    /// <summary>
    /// Retrieves all rentals.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rentals = await _rentals.GetAllAsync();

        var result = new List<RentalDto>();

        foreach (var r in rentals)
        {
            var bike = await _bikes.GetByIdAsync(r.BikeId);
            var renter = await _renters.GetByIdAsync(r.RenterId);
            bike.Model = await _models.GetByIdAsync(bike.ModelId);

            result.Add(new RentalDto
            {
                Id = r.Id,
                BikeId = r.BikeId,
                Bike = bike?.ToDto(),
                RenterId = r.RenterId,
                Renter = renter?.ToDto(),
                StartTime = r.StartTime,
                DurationHours = r.DurationHours
            });
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new rental.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(RentalCreateDto dto)
    {
        var bike = await _bikes.GetByIdAsync(dto.BikeId);
        if (bike == null)
            return NotFound($"Bike with id {dto.BikeId} not found");

        var renter = await _renters.GetByIdAsync(dto.RenterId);
        if (renter == null)
            return NotFound($"Renter with id {dto.RenterId} not found");

        bike.Model = await _models.GetByIdAsync(bike.ModelId);

        var rental = new Rental
        {
            BikeId = dto.BikeId,
            RenterId = dto.RenterId,
            StartTime = dto.StartTime,
            DurationHours = dto.DurationHours
        };

        await _rentals.CreateAsync(rental);

        var result = new RentalDto
        {
            Id = rental.Id,
            BikeId = rental.BikeId,
            Bike = bike.ToDto(),           
            RenterId = rental.RenterId,
            Renter = renter.ToDto(),       
            StartTime = rental.StartTime,
            DurationHours = rental.DurationHours
        };

        return Ok(result);
    }
}
