using BikeRental.Contracts.Dtos;
using BikeRental.Domain.Models;

namespace BikeRental.Api.Extensions;

/// <summary>
/// Provides extension methods for converting domain models into DTOs.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Converts a <see cref="Bike"/> domain entity into a <see cref="BikeDto"/>.
    /// </summary>
    public static BikeDto ToDto(this Bike bike) =>
        new()
        {
            Id = bike.Id,
            SerialNumber = bike.SerialNumber,
            Color = bike.Color,
            ModelId = bike.ModelId,
            Model = bike.Model?.ToDto()
        };

    /// <summary>
    /// Converts a <Renter> domain entity into a <see cref="RenterDto"/>.
    /// </summary>
    public static RenterDto ToDto(this Renter renter) =>
        new()
        {
            Id = renter.Id,
            FullName = renter.FullName,
            Phone = renter.Phone
        };

    /// <summary>
    /// Converts a <BikeModel> domain entity into a <see cref="BikeModelDto"/>.
    /// </summary>
    public static BikeModelDto ToDto(this BikeModel model) =>
    new()
    {
        Id = model.Id,
        Name = model.Name,
        Type = model.Type,
        WheelSize = model.WheelSize,
        MaxRiderWeight = model.MaxRiderWeight,
        BikeWeight = model.BikeWeight,
        BrakeType = model.BrakeType,
        ModelYear = model.ModelYear,
        HourlyRate = model.HourlyRate
    };
}
