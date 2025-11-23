using BikeRental.Api.DTO;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikesController : ControllerBase
{
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<BikeModel> _models;

    public BikesController(IRepository<Bike> bikes, IRepository<BikeModel> models)
    {
        _bikes = bikes;
        _models = models;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _bikes.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        return bike is null ? NotFound() : Ok(bike);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BikeDto dto)
    {
        // Проверяем, есть ли такой BikeModel
        var model = await _models.GetByIdAsync(dto.ModelId);
        if (model == null)
            return BadRequest($"BikeModel with id {dto.ModelId} not found");

        var bike = new Bike
        {
            Color = dto.Color,
            SerialNumber = dto.SerialNumber,
            ModelId = dto.ModelId
        };

        await _bikes.CreateAsync(bike);
        return Ok(bike);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _bikes.DeleteAsync(id);
        return NoContent();
    }
}