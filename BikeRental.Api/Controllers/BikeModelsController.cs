using BikeRental.Contracts.Dtos;
using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;
using BikeRental.Domain;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Controller for managing bike models in the rental system.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BikeModelsController"/> class.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class BikeModelsController(IRepository<BikeModel> _models) : ControllerBase
{
    /// <summary>
    /// Retrieves all bike models.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _models.GetAllAsync());

    /// <summary>
    /// Creates a new bike model.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(BikeModelCreateDto dto)
    {
        var model = new BikeModel
        {
            Name = dto.Name,
            Type = dto.Type,
            WheelSize = dto.WheelSize,
            BikeWeight = dto.BikeWeight,
            MaxRiderWeight = dto.MaxRiderWeight,
            BrakeType = dto.BrakeType,
            ModelYear = dto.ModelYear,
            HourlyRate = dto.HourlyRate
        };

        await _models.CreateAsync(model);
        return Ok(model);
    }
}
