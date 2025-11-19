using BikeRental.Domain.Models;
using BikeRental.Domain.Enums;
using BikeRental.Infrastructure.Repositories;

namespace BikeRental.Infrastructure.Services;

public class AnalyticsService
{
	private readonly IRepository<Rental> _rentalRepo;
	private readonly IRepository<BikeModel> _modelRepo;

	public AnalyticsService(
		IRepository<Rental> rentalRepo,
		IRepository<BikeModel> modelRepo)
	{
		_rentalRepo = rentalRepo;
		_modelRepo = modelRepo;
	}

	/// <summary>
	/// Найти все модели типа Sport.
	/// </summary>
	public async Task<List<string>> GetSportBikeModels()
	{
		var models = await _modelRepo.GetAllAsync();

		return models
			.Where(m => m.Type == BikeType.Sport)
			.Select(m => m.Name)
			.OrderBy(n => n)
			.ToList();
	}

	/// <summary>
	/// Топ-5 моделей по прибыли.
	/// </summary>
	public async Task<List<string>> GetTop5ModelsByProfit()
	{
		var rentals = await _rentalRepo.GetAllAsync();

		return rentals
			.GroupBy(r => r.Bike.Model)
			.Select(g => new
			{
				g.Key.Name,
				Profit = g.Sum(r => r.Bike.Model.HourlyRate * r.DurationHours)
			})
			.OrderByDescending(x => x.Profit)
			.Take(5)
			.Select(x => x.Name)
			.ToList();
	}

	/// <summary>
	/// Топ-5 моделей по длительности аренды.
	/// </summary>
	public async Task<List<string>> GetTop5ModelsByDuration()
	{
		var rentals = await _rentalRepo.GetAllAsync();

		return rentals
			.GroupBy(r => r.Bike.Model)
			.Select(g => new
			{
				g.Key.Name,
				Duration = g.Sum(r => r.DurationHours)
			})
			.OrderByDescending(x => x.Duration)
			.Take(5)
			.Select(x => x.Name)
			.ToList();
	}

	/// <summary>
	/// Min/Max/Avg длительности аренды.
	/// </summary>
	public async Task<(int Min, int Max, double Avg)> GetRentalDurationStats()
	{
		var rentals = await _rentalRepo.GetAllAsync();
		var durations = rentals.Select(r => r.DurationHours).ToList();

		return (
			durations.Min(),
			durations.Max(),
			durations.Average()
		);
	}

	/// <summary>
	/// Сумма аренды по типу велосипеда.
	/// </summary>
	public async Task<int> GetTotalTimeByBikeType(BikeType type)
	{
		var rentals = await _rentalRepo.GetAllAsync();

		return rentals
			.Where(r => r.Bike.Model.Type == type)
			.Sum(r => r.DurationHours);
	}

	/// <summary>
	/// Топ-3 клиентов по количеству аренд.
	/// </summary>
	public async Task<List<string>> GetTop3Renters()
	{
		var rentals = await _rentalRepo.GetAllAsync();

		return rentals
			.GroupBy(r => r.Renter)
			.OrderByDescending(g => g.Count())
			.Take(3)
			.Select(g => g.Key.FullName)
			.ToList();
	}
}
