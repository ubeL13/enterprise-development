using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain;
using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides analytics data and statistics for the bike rental system.
/// </summary>
public class AnalyticsService(
    IRepository<Rental> rentals,
    IRepository<Bike> bikes,
    IRepository<BikeModel> models,
    IRepository<Renter> renters)
    : IAnalyticsService
{
    /// <summary>
    /// Returns the names of all bike models of type Sport.
    /// </summary>
    public async Task<IEnumerable<string>> GetSportModelsAsync()
    {
        var allModels = await models.GetAllAsync();

        return allModels
            .Where(m => m.Type == BikeType.Sport)
            .Select(m => m.Name);
    }

    /// <summary>
    /// Returns the top 5 bike models ranked by total rental profit.
    /// </summary>
    public async Task<IEnumerable<TopModelProfitDto>> GetTop5ModelsByProfitAsync()
    {
        var allRentals = await rentals.GetAllAsync();
        var allBikes = await bikes.GetAllAsync();
        var allModels = await models.GetAllAsync();

        return
            (from rental in allRentals
             join bike in allBikes on rental.BikeId equals bike.Id
             join model in allModels on bike.ModelId equals model.Id
             group rental by model into g
             select new TopModelProfitDto
             {
                 Model = g.Key.Name,
                 Profit = g.Sum(r => r.DurationHours * g.Key.HourlyRate)
             })
            .OrderByDescending(x => x.Profit)
            .Take(5);
    }

    /// <summary>
    /// Returns the top 5 bike models ranked by total rental duration.
    /// </summary>
    public async Task<IEnumerable<TopModelDurationDto>> GetTop5ModelsByDurationAsync()
    {
        var allRentals = await rentals.GetAllAsync();
        var allBikes = await bikes.GetAllAsync();
        var allModels = await models.GetAllAsync();

        return
            (from rental in allRentals
             join bike in allBikes on rental.BikeId equals bike.Id
             join model in allModels on bike.ModelId equals model.Id
             group rental by model into g
             select new TopModelDurationDto
             {
                 Model = g.Key.Name,
                 TotalHours = g.Sum(r => r.DurationHours)
             })
            .OrderByDescending(x => x.TotalHours)
            .Take(5);
    }

    /// <summary>
    /// Returns minimum, maximum and average rental duration values.
    /// </summary>
    public async Task<RentalDurationStatsDto> GetRentalDurationStatsAsync()
    {
        var allRentals = await rentals.GetAllAsync();

        if (allRentals.Count == 0)
        {
            return new RentalDurationStatsDto();
        }

        return new RentalDurationStatsDto
        {
            Min = allRentals.Min(r => r.DurationHours),
            Max = allRentals.Max(r => r.DurationHours),
            Avg = allRentals.Average(r => r.DurationHours)
        };
    }

    /// <summary>
    /// Returns the total rental duration for a specified bike type.
    /// </summary>
    public async Task<double> GetTotalDurationByBikeTypeAsync(BikeType type)
    {
        var allRentals = await rentals.GetAllAsync();
        var allBikes = await bikes.GetAllAsync();
        var allModels = await models.GetAllAsync();

        return
            (from rental in allRentals
             join bike in allBikes on rental.BikeId equals bike.Id
             join model in allModels on bike.ModelId equals model.Id
             where model.Type == type
             select rental.DurationHours)
            .Sum();
    }

    /// <summary>
    /// Returns the top 3 renters ranked by number of rentals.
    /// </summary>
    public async Task<IEnumerable<TopRenterDto>> GetTop3RentersAsync()
    {
        var allRentals = await rentals.GetAllAsync();
        var allRenters = await renters.GetAllAsync();

        return
            (from rental in allRentals
             join renter in allRenters on rental.RenterId equals renter.Id
             group renter by renter into g
             select new TopRenterDto
             {
                 Renter = g.Key.FullName,
                 Count = g.Count()
             })
            .OrderByDescending(x => x.Count)
            .Take(3);
    }
}
