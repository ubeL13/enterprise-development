using BikeRental.Api.DTOs;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikeModelsController : ControllerBase
{
    private readonly IRepository<BikeModel> _models;

    public BikeModelsController(IRepository<BikeModel> models)
    {
        _models = models;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _models.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(BikeModelDto dto)
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
