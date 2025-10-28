using BikeRental.Domain.Models;
using BikeRental.Domain.Enums;

namespace BikeRental.Tests;

public class RentalTests
{
    private readonly List<BikeModel> _models;
    private readonly List<Bike> _bikes;
    private readonly List<Renter> _renters;
    private readonly List<Rental> _rentals;

    public RentalTests()
    {
        _models = DataSeeder.GetBikeModels();
        _renters = DataSeeder.GetRenters();
        _bikes = DataSeeder.GetBikes(_models);
        _rentals = DataSeeder.GetRentals(_bikes, _renters);
    }

    [Fact]
    public void Should_Find_All_Sport_Bikes()
    {
        var sportBikes = _models.Where(m => m.Type == BikeType.Sport).ToList();
        Assert.Equal(2, sportBikes.Count);
    }

    [Fact]
    public void Should_Calculate_Top5_Models_By_Profit()
    {
        var top5 = _rentals
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Model = g.Key,
                Profit = g.Sum(r => r.Bike.Model.HourlyRate * r.DurationHours)
            })
            .OrderByDescending(x => x.Profit)
            .Take(5)
            .ToList();

        var topModel = top5.First();
        Assert.Equal("TurboSport", topModel.Model.Name);
        Assert.Equal(180, topModel.Profit);
    }

    [Fact]
    public void Should_Calculate_Top5_Models_By_Duration()
    {
        var top5 = _rentals
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Model = g.Key,
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Take(5)
            .ToList();

        var top1 = top5.First();
        var top2 = top5.Skip(1).First();

        Assert.Equal("Urban 2000", top1.Model.Name);
        Assert.Equal(7, top1.TotalDuration);
        Assert.Equal("TurboSport", top2.Model.Name);
        Assert.Equal(6, top2.TotalDuration);
    }

    [Fact]
    public void Should_Find_Min_Max_Avg_Rental_Duration()
    {
        var durations = _rentals.Select(r => r.DurationHours).ToList();
        var min = durations.Min(); 
        var max = durations.Max(); 
        var avg = durations.Average(); 

        Assert.Equal(1, min);
        Assert.Equal(7, max);
        Assert.Equal(4.0, avg);
    }

    [Fact]
    public void Should_Sum_Rental_Time_By_BikeType()
    {
        var sumByType = _rentals
            .GroupBy(r => r.Bike.Model.Type)
            .Select(g => new { Type = g.Key, TotalHours = g.Sum(r => r.DurationHours) })
            .ToList();

        var sportHours = sumByType.First(x => x.Type == BikeType.Sport).TotalHours;
        Assert.Equal(9, sportHours);
    }

    [Fact]
    public void Should_Find_Top_Renters_By_Usage()
    {
        var topRenters = _rentals
            .GroupBy(r => r.Renter)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key.FullName)
            .ToList();

        Assert.Equal(new[] { "Иванов Иван", "Петров Пётр", "Сидоров Сидор" }, topRenters);
    }
}
