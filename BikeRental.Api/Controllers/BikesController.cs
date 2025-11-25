using BikeRental.Api.DTO;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing bikes in the rental system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikesController : ControllerBase
{
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<BikeModel> _models;

    /// <summary>
    /// Initializes a new instance of the controller.
    /// </summary>
    public BikesController(IRepository<Bike> bikes, IRepository<BikeModel> models)
    {
        _bikes = bikes;
        _models = models;
    }

    /// <summary>
    /// Retrieves all bikes.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _bikes.GetAllAsync());

    /// <summary>
    /// Retrieves a bike by its ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        return bike is null ? NotFound() : Ok(bike);
    }

    /// <summary>
    /// Creates a new bike.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(BikeDto dto)
    {
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

    /// <summary>
    /// Deletes a bike by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _bikes.DeleteAsync(id);
        return NoContent();
    }
}
