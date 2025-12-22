using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides CRUD operations and business logic for managing bike rentals.
/// Handles mapping between domain models and DTOs.
/// </summary>
public class RentalService(
    IRepository<Rental> _rentals,
    IRepository<Bike> _bikes,
    IRepository<Renter> _renters,
    IRepository<BikeModel> _models
) : IRentalService
{
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

    private static BikeDto ToBikeDto(Bike b, BikeModel? m) => new()
    {
        Id = b.Id,
        SerialNumber = b.SerialNumber,
        Color = b.Color,
        ModelId = b.ModelId,
        Model = m != null ? ToModelDto(m) : null
    };

    private static RenterDto ToRenterDto(Renter r) => new()
    {
        Id = r.Id,
        FullName = r.FullName,
        Phone = r.Phone
    };

    private static RentalDto ToRentalDto(Rental r, BikeDto b, RenterDto ren) => new()
    {
        Id = r.Id,
        BikeId = r.BikeId,
        Bike = b,
        RenterId = r.RenterId,
        Renter = ren,
        StartTime = r.StartTime,
        DurationHours = r.DurationHours
    };

    /// <summary>
    /// Retrieves all rentals with their associated bikes and renters.
    /// </summary>
    public async Task<IEnumerable<RentalDto>> GetAllAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = (await _bikes.GetAllAsync()).ToDictionary(b => b.Id);
        var models = (await _models.GetAllAsync()).ToDictionary(m => m.Id);
        var renters = (await _renters.GetAllAsync()).ToDictionary(r => r.Id);

        var result = new List<RentalDto>();

        foreach (var r in rentals)
        {
            if (!bikes.TryGetValue(r.BikeId, out var bike)) continue;
            if (!renters.TryGetValue(r.RenterId, out var renter)) continue;

            models.TryGetValue(bike.ModelId, out var model);

            var bikeDto = ToBikeDto(bike, model);
            var renterDto = ToRenterDto(renter);

            result.Add(ToRentalDto(r, bikeDto, renterDto));
        }

        return result;
    }

    /// <summary>
    /// Retrieves a single rental by its unique identifier.
    /// </summary>
    public async Task<RentalDto?> GetByIdAsync(string id)
    {
        var rental = await _rentals.GetByIdAsync(id);
        if (rental == null) return null;

        var bike = await _bikes.GetByIdAsync(rental.BikeId);
        var renter = await _renters.GetByIdAsync(rental.RenterId);
        if (bike == null || renter == null) return null;

        var model = await _models.GetByIdAsync(bike.ModelId);

        return ToRentalDto(rental, ToBikeDto(bike, model), ToRenterDto(renter));
    }

    /// <summary>
    /// Creates a new rental in the system.
    /// </summary>
    public async Task<RentalDto> CreateAsync(RentalCreateDto dto)
    {
        var bike = await _bikes.GetByIdAsync(dto.BikeId)
                   ?? throw new KeyNotFoundException($"Bike {dto.BikeId} not found");

        var renter = await _renters.GetByIdAsync(dto.RenterId)
                     ?? throw new KeyNotFoundException($"Renter {dto.RenterId} not found");

        var model = await _models.GetByIdAsync(bike.ModelId);

        var rental = new Rental
        {
            BikeId = dto.BikeId,
            RenterId = dto.RenterId,
            StartTime = dto.StartTime,
            DurationHours = dto.DurationHours
        };

        await _rentals.CreateAsync(rental);

        return ToRentalDto(rental, ToBikeDto(bike, model), ToRenterDto(renter));
    }

    /// <summary>
    /// Updates an existing rental.
    /// </summary>
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

        var model = await _models.GetByIdAsync(bike.ModelId);

        return ToRentalDto(rental, ToBikeDto(bike, model), ToRenterDto(renter));
    }

    /// <summary>
    /// Deletes a rental by its unique identifier.
    /// </summary>
    public async Task<bool> DeleteAsync(string id)
    {
        var rental = await _rentals.GetByIdAsync(id);
        if (rental == null) return false;

        await _rentals.DeleteAsync(id);
        return true;
    }
}
