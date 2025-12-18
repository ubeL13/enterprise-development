using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain.Models;
using BikeRental.Domain;

namespace BikeRental.Application.Services;

public class RentalService : IRentalService
{
    private readonly IRepository<Rental> _rentals;
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<Renter> _renters;
    private readonly IRepository<BikeModel> _models;

    public RentalService(
        IRepository<Rental> rentals,
        IRepository<Bike> bikes,
        IRepository<Renter> renters,
        IRepository<BikeModel> models)
    {
        _rentals = rentals;
        _bikes = bikes;
        _renters = renters;
        _models = models;
    }

    private static BikeModelDto ToDto(BikeModel m) => new()
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

    private static BikeDto ToDto(Bike b)
    {
        return new BikeDto
        {
            Id = b.Id,
            SerialNumber = b.SerialNumber,
            Color = b.Color,
            ModelId = b.ModelId,
            Model = b.Model != null ? ToDto(b.Model) : null
        };
    }

    private static RenterDto ToDto(Renter r) => new()
    {
        Id = r.Id,
        FullName = r.FullName,
        Phone = r.Phone
    };

    private static RentalDto ToDto(Rental r, Bike b, Renter ren) => new()
    {
        Id = r.Id,
        BikeId = r.BikeId,
        Bike = ToDto(b),
        RenterId = r.RenterId,
        Renter = ToDto(ren),
        StartTime = r.StartTime,
        DurationHours = r.DurationHours
    };

    public async Task<IEnumerable<RentalDto>> GetAllAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var result = new List<RentalDto>();

        foreach (var r in rentals)
        {
            var bike = await _bikes.GetByIdAsync(r.BikeId);
            var renter = await _renters.GetByIdAsync(r.RenterId);

            if (bike == null || renter == null) continue;

            bike.Model = await _models.GetByIdAsync(bike.ModelId);

            result.Add(ToDto(r, bike, renter));
        }

        return result;
    }

    public async Task<RentalDto?> GetByIdAsync(string id)
    {
        var r = await _rentals.GetByIdAsync(id);
        if (r == null) return null;

        var bike = await _bikes.GetByIdAsync(r.BikeId);
        var renter = await _renters.GetByIdAsync(r.RenterId);

        if (bike == null || renter == null) return null;

        bike.Model = await _models.GetByIdAsync(bike.ModelId);

        return ToDto(r, bike, renter);
    }

    public async Task<RentalDto> CreateAsync(RentalCreateDto dto)
    {
        var bike = await _bikes.GetByIdAsync(dto.BikeId)
                   ?? throw new KeyNotFoundException($"Bike {dto.BikeId} not found");

        var renter = await _renters.GetByIdAsync(dto.RenterId)
                     ?? throw new KeyNotFoundException($"Renter {dto.RenterId} not found");

        bike.Model = await _models.GetByIdAsync(bike.ModelId);

        var rental = new Rental
        {
            BikeId = dto.BikeId,
            RenterId = dto.RenterId,
            StartTime = dto.StartTime,
            DurationHours = dto.DurationHours
        };

        await _rentals.CreateAsync(rental);

        return ToDto(rental, bike, renter);
    }

    public async Task<RentalDto?> UpdateAsync(RentalUpdateDto dto)
    {
        var rental = await _rentals.GetByIdAsync(dto.Id);
        if (rental == null) return null;

        var bike = await _bikes.GetByIdAsync(dto.BikeId)
                   ?? throw new KeyNotFoundException($"Bike {dto.BikeId} not found");

        var renter = await _renters.GetByIdAsync(dto.RenterId)
                     ?? throw new KeyNotFoundException($"Renter {dto.RenterId} not found");

        rental.BikeId = dto.BikeId;
        rental.RenterId = dto.RenterId;
        rental.StartTime = dto.StartTime;
        rental.DurationHours = dto.DurationHours;

        await _rentals.UpdateAsync(rental.Id, rental);

        bike.Model = await _models.GetByIdAsync(bike.ModelId);

        return ToDto(rental, bike, renter);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var rental = await _rentals.GetByIdAsync(id);
        if (rental == null) return false;

        await _rentals.DeleteAsync(id);
        return true;
    }
}
