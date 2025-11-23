using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Repositories;

namespace BikeRental.Infrastructure.Services;

public class AnalyticsService
{
    private readonly IRepository<Rental> _rentals;
    private readonly IRepository<Bike> _bikes;
    private readonly IRepository<BikeModel> _models;
    private readonly IRepository<Renter> _renters;

    public AnalyticsService(
        IRepository<Rental> rentals,
        IRepository<Bike> bikes,
        IRepository<BikeModel> models,
        IRepository<Renter> renters)
    {
        _rentals = rentals;
        _bikes = bikes;
        _models = models;
        _renters = renters;
    }

    // 1. Все спорт модели
    public async Task<IEnumerable<BikeModel>> GetSportModelsAsync()
    {
        var models = await _models.GetAllAsync();
        return models.Where(m => m.Type == BikeType.Sport);
    }

    // 2. Топ-5 моделей по прибыли
    public async Task<IEnumerable<object>> GetTop5ModelsByProfitAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var query =
            from rental in rentals
            join bike in bikes on rental.Bike.Id equals bike.Id
            join model in models on bike.Model.Id equals model.Id
            group rental by model into g
            select new
            {
                Model = g.Key.Name,
                Profit = g.Sum(r => r.DurationHours * g.Key.HourlyRate)
            };

        return query.OrderByDescending(x => x.Profit).Take(5);
    }

    // 3. Топ-5 моделей по общей продолжительности аренды
    public async Task<IEnumerable<object>> GetTop5ModelsByDurationAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var query =
            from rental in rentals
            join bike in bikes on rental.Bike.Id equals bike.Id
            join model in models on bike.Model.Id equals model.Id
            group rental by model into g
            select new
            {
                Model = g.Key.Name,
                TotalHours = g.Sum(r => r.DurationHours)
            };

        return query.OrderByDescending(x => x.TotalHours).Take(5);
    }

    // 4. Статистика длительности
    public async Task<(double min, double max, double avg)> GetRentalDurationStatsAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        if (!rentals.Any()) return (0, 0, 0);

        return (
            rentals.Min(r => r.DurationHours),
            rentals.Max(r => r.DurationHours),
            rentals.Average(r => r.DurationHours)
        );
    }

    // 5. Общая длительность по типу велосипеда
    public async Task<double> GetTotalDurationByBikeTypeAsync(BikeType type)
    {
        var rentals = await _rentals.GetAllAsync();
        var bikes = await _bikes.GetAllAsync();
        var models = await _models.GetAllAsync();

        var query =
            from rental in rentals
            join bike in bikes on rental.Bike.Id equals bike.Id
            join model in models on bike.Model.Id equals model.Id
            where model.Type == type
            select rental.DurationHours;

        return query.Sum();
    }

    // 6. Топ-3 клиентов по количеству аренд
    public async Task<IEnumerable<object>> GetTop3RentersAsync()
    {
        var rentals = await _rentals.GetAllAsync();
        var renters = await _renters.GetAllAsync();

        var query =
            from rental in rentals
            join renter in renters on rental.Renter.Id equals renter.Id
            group renter by renter into g
            select new
            {
                Renter = g.Key.FullName,
                Count = g.Count()
            };

        return query.OrderByDescending(x => x.Count).Take(3);
    }
}
