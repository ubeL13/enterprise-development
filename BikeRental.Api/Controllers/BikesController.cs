using BikeRental.Contracts.Dtos;
using BikeRental.Domain.Models;
using BikeRental.Domain;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Api.Extensions;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikesController(IRepository<Bike> _bikes, IRepository<BikeModel> _models) : ControllerBase
{
    /// <summary>
    /// Returns all bikes with their models loaded.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bikes = await _bikes.GetAllAsync();

        foreach (var bike in bikes)
            bike.Model = await _models.GetByIdAsync(bike.ModelId);

        return Ok(bikes);
    }

    /// <summary>
    /// Creates a new bike entity and returns the created bike with its model information populated.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(BikeCreateDto dto)
    {
        
        var model = await _models.GetByIdAsync(dto.ModelId);
        if (model == null)
            return BadRequest($"BikeModel with id {dto.ModelId} not found");

        var newBike = new Bike
        {
            SerialNumber = dto.SerialNumber,
            Color = dto.Color,
            ModelId = dto.ModelId
        };

        await _bikes.CreateAsync(newBike);

        var result = newBike.ToDto(); 
        result.Model = model.ToDto();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _bikes.DeleteAsync(id);
        return NoContent();
    }
}
