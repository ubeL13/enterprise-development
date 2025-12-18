using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain.Models;
using BikeRental.Domain;

namespace BikeRental.Application.Services;

public class BikeService : IBikeService
{
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<BikeModel> _models;

    public BikeService(
        IRepository<Bike> bikes,
        IRepository<BikeModel> models)
    {
        _bikes = bikes;
        _models = models;
    }

    public async Task<IEnumerable<BikeDto>> GetAllAsync()
    {
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        return bikes.Select(b =>
        {
            var model = models.First(m => m.Id == b.ModelId);

            return new BikeDto
            {
                Id = b.Id,
                SerialNumber = b.SerialNumber,
                Color = b.Color,
                ModelId = b.ModelId,
                Model = new BikeModelDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    Type = model.Type,
                    WheelSize = model.WheelSize,
                    BikeWeight = model.BikeWeight,
                    MaxRiderWeight = model.MaxRiderWeight,
                    BrakeType = model.BrakeType,
                    ModelYear = model.ModelYear,
                    HourlyRate = model.HourlyRate
                }
            };
        });
    }

    public async Task<BikeDto?> GetByIdAsync(string id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        if (bike == null) return null;

        var model = await _models.GetByIdAsync(bike.ModelId);
        if (model == null) return null;

        return new BikeDto
        {
            Id = bike.Id,
            SerialNumber = bike.SerialNumber,
            Color = bike.Color,
            ModelId = bike.ModelId,
            Model = new BikeModelDto
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                WheelSize = model.WheelSize,
                BikeWeight = model.BikeWeight,
                MaxRiderWeight = model.MaxRiderWeight,
                BrakeType = model.BrakeType,
                ModelYear = model.ModelYear,
                HourlyRate = model.HourlyRate
            }
        };
    }

    public async Task<BikeDto> CreateAsync(BikeCreateDto dto)
    {
        var model = await _models.GetByIdAsync(dto.ModelId)
            ?? throw new ArgumentException("Bike model not found");

        var bike = new Bike
        {
            SerialNumber = dto.SerialNumber,
            Color = dto.Color,
            ModelId = dto.ModelId
        };

        await _bikes.CreateAsync(bike);

        return (await GetByIdAsync(bike.Id))!;
    }

    public async Task<BikeDto?> UpdateAsync(BikeUpdateDto dto)
    {
        var bike = await _bikes.GetByIdAsync(dto.Id);
        if (bike == null) return null;

        bike.SerialNumber = dto.SerialNumber;
        bike.Color = dto.Color;
        bike.ModelId = dto.ModelId;

        await _bikes.UpdateAsync(bike.Id, bike);

        return await GetByIdAsync(bike.Id);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var bike = await _bikes.GetByIdAsync(id);
        if (bike == null) return false;

        await _bikes.DeleteAsync(id);
        return true;
    }
}
