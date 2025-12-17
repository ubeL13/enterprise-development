using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain.Models;
using BikeRental.Domain;

namespace BikeRental.Application.Services;

public class BikeModelService : IBikeModelService
{
    private readonly IRepository<BikeModel> _repository;

    public BikeModelService(IRepository<BikeModel> repository)
    {
        _repository = repository;
    }

    private static BikeModelDto ToReadDto(BikeModel m) => new()
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

    public async Task<IEnumerable<BikeModelDto>> GetAllAsync()
    {
        var models = await _repository.GetAllAsync();
        return models.Select(ToReadDto);
    }

    public async Task<BikeModelDto> GetByIdAsync(string id)
    {
        var model = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Модель не найдена");

        return ToReadDto(model);
    }

    public async Task<BikeModelDto> CreateAsync(BikeModelCreateDto dto)
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

        await _repository.CreateAsync(model);
        return ToReadDto(model);
    }

    public async Task<BikeModelDto?> UpdateAsync(BikeModelUpdateDto dto)
    {
        var model = await _repository.GetByIdAsync(dto.Id);
        if (model == null) return null;

        model.Name = dto.Name;
        model.Type = dto.Type;
        model.WheelSize = dto.WheelSize;
        model.BikeWeight = dto.BikeWeight;
        model.MaxRiderWeight = dto.MaxRiderWeight;
        model.BrakeType = dto.BrakeType;
        model.ModelYear = dto.ModelYear;
        model.HourlyRate = dto.HourlyRate;

        await _repository.UpdateAsync(model.Id, model);
        return ToReadDto(model);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
