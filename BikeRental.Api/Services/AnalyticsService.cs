using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;

namespace BikeRental.Api.Services;

/// <summary>
/// Service for retrieving analytics data from the bike rental system.
/// </summary>
/// <remarks>
/// Initializes a new instance of the service.
/// </remarks>
public class AnalyticsService(
    IRepository<Rental> rentals,
    IRepository<Bike> bikes,
    IRepository<BikeModel> models,
    IRepository<Renter> renters)
{
    private readonly IRepository<Rental> _rentals = rentals;
    private readonly IRepository<Bike> _bikes = bikes;
    private readonly IRepository<BikeModel> _models = models;
    private readonly IRepository<Renter> _renters = renters;

    /// <summary>
    /// Retrieves all bike models of type "Sport".
    /// </summary>
    public async Task<IEnumerable<BikeModel>> GetSportModelsAsync()
    {
        var models = await _models.GetAllAsync();
        return models.Where(m => m.Type == BikeType.Sport);
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total profit.
    /// </summary>
    public async Task<IEnumerable<object>> GetTop5ModelsByProfitAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var query =
            from rental in rentals
            join bike in bikes on rental.BikeId equals bike.Id
            join model in models on bike.ModelId equals model.Id
            group rental by model into g
            select new
            {
                Model = g.Key.Name,
                Profit = g.Sum(r => r.DurationHours * g.Key.HourlyRate)
            };

        return query.OrderByDescending(x => x.Profit).Take(5);
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total rental duration.
    /// </summary>
    public async Task<IEnumerable<object>> GetTop5ModelsByDurationAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var query =
            from rental in rentals
            join bike in bikes on rental.BikeId equals bike.Id
            join model in models on bike.ModelId equals model.Id
            group rental by model into g
            select new
            {
                Model = g.Key.Name,
                TotalHours = g.Sum(r => r.DurationHours)
            };

        return query.OrderByDescending(x => x.TotalHours).Take(5);
    }

    /// <summary>
    /// Retrieves statistics about rental durations (min, max, average).
    /// </summary>
    public async Task<(double min, double max, double avg)> GetRentalDurationStatsAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        if (rentals.Count == 0) return (0, 0, 0);

        return (
            rentals.Min(r => r.DurationHours),
            rentals.Max(r => r.DurationHours),
            rentals.Average(r => r.DurationHours)
        );
    }

    /// <summary>
    /// Retrieves total rental duration for a specific bike type.
    /// </summary>
    public async Task<double> GetTotalDurationByBikeTypeAsync(BikeType type)
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var query =
            from rental in rentals
            join bike in bikes on rental.BikeId equals bike.Id
            join model in models on bike.ModelId equals model.Id
            where model.Type == type
            select rental.DurationHours;

        return query.Sum();
    }

    /// <summary>
    /// Retrieves the top 3 renters by number of rentals.
    /// </summary>
    public async Task<IEnumerable<object>> GetTop3RentersAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var renters = await _renters.GetAllAsync();

        var query =
            from rental in rentals
            join renter in renters on rental.RenterId equals renter.Id
            group renter by renter into g
            select new
            {
                Renter = g.Key.FullName,
                Count = g.Count()
            };

        return query.OrderByDescending(x => x.Count).Take(3);
    }
}
