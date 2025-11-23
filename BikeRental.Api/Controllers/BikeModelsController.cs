using BikeRental.Api.DTO;
using BikeRental.Domain.Enums;
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
        if (!Enum.TryParse<BikeType>(dto.Type, true, out var bikeType))
            return BadRequest($"Unknown bike type: {dto.Type}");

        var model = new BikeModel
        {
            Name = dto.Name,
            Type = bikeType,
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
