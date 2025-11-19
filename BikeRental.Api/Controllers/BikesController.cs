using BikeRental.Api.DTOs;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikesController : ControllerBase
{
    private readonly IRepository<Bike> _bikes;

    public BikesController(IRepository<Bike> bikes)
    {
        _bikes = bikes;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _bikes.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        return bike is null ? NotFound() : Ok(bike);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BikeDto dto)
    {
        var bike = new Bike
        {
            Color = dto.Color,
            SerialNumber = dto.SerialNumber,
            Model = new BikeModel { Id = dto.ModelId }
        };

        await _bikes.CreateAsync(bike);
        return Ok(bike);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _bikes.DeleteAsync(id);
        return NoContent();
    }
}
