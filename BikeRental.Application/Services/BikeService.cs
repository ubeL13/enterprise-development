using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Service implementation for managing bikes.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BikeService"/> class.
/// </remarks>
public class BikeService(
    IRepository<Bike> bikes,
    IRepository<BikeModel> models) : IBikeService
{
    private readonly IRepository<Bike> _bikes = bikes;
    private readonly IRepository<BikeModel> _models = models;

    private static BikeModelDto ToModelDto(BikeModel m) => new()
    {
        Id = m.Id,
        Name = m.Name,
        Type = m.Type,
        WheelSize = m.WheelSize,
        BikeWeight = m.BikeWeight,
        MaxRiderWeight = m.MaxRiderWeight,
        BrakeType = m.BrakeType,
        ModelYear = m.ModelYear,
        HourlyRate = m.HourlyRate
    };

    private static BikeDto ToDto(Bike b, BikeModel m) => new()
    {
        Id = b.Id,
        SerialNumber = b.SerialNumber,
        Color = b.Color,
        ModelId = b.ModelId,
        Model = ToModelDto(m)
    };

    /// <summary>
    /// Retrieves all bikes with their associated models.
    /// </summary>
    public async Task<IEnumerable<BikeDto>> GetAllAsync()
    {
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var modelMap = models.ToDictionary(m => m.Id);

        var result = new List<BikeDto>();

        foreach (var bike in bikes)
        {
            if (!modelMap.TryGetValue(bike.ModelId, out var model))
                continue;

            result.Add(ToDto(bike, model));
        }

        return result;
    }

    /// <summary>
    /// Retrieves a bike by its unique identifier.
    /// </summary>
    public async Task<BikeDto?> GetByIdAsync(string id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        if (bike == null) return null;

        var model = await _models.GetByIdAsync(bike.ModelId);
        if (model == null) return null;

        return ToDto(bike, model);
    }

    /// <summary>
    /// Creates a new bike.
    /// </summary>
    public async Task<BikeDto> CreateAsync(BikeCreateDto dto)
    {
        var model = await _models.GetByIdAsync(dto.ModelId)
            ?? throw new KeyNotFoundException($"Bike model {dto.ModelId} not found");

        var bike = new Bike
        {
            SerialNumber = dto.SerialNumber,
            Color = dto.Color,
            ModelId = dto.ModelId
        };

        await _bikes.CreateAsync(bike);

        return ToDto(bike, model);
    }

    /// <summary>
    /// Updates an existing bike.
    /// </summary>
    public async Task<BikeDto?> UpdateAsync(BikeUpdateDto dto)
    {
        var bike = await _bikes.GetByIdAsync(dto.Id);
        if (bike == null) return null;

        var model = await _models.GetByIdAsync(dto.ModelId)
            ?? throw new KeyNotFoundException($"Bike model {dto.ModelId} not found");

        bike.SerialNumber = dto.SerialNumber;
        bike.Color = dto.Color;
        bike.ModelId = dto.ModelId;

        await _bikes.UpdateAsync(bike.Id, bike);

        return ToDto(bike, model);
    }

    /// <summary>
    /// Deletes a bike by its unique identifier.
    /// </summary>
    public async Task<bool> DeleteAsync(string id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        if (bike == null) return false;

        await _bikes.DeleteAsync(id);
        return true;
    }
}
