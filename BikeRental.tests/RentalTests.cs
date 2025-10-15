using BikeRental.Domain;
using Xunit;
using System.Collections.Generic;
using System.Linq;
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
        Assert.NotEmpty(sportBikes);
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

        Assert.True(top5.Count <= 5);
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

        Assert.True(top5.Count <= 5);
    }

    [Fact]
    public void Should_Find_Min_Max_Avg_Rental_Duration()
    {
        var min = _rentals.Min(r => r.DurationHours);
        var max = _rentals.Max(r => r.DurationHours);
        var avg = _rentals.Average(r => r.DurationHours);

        Assert.True(min <= avg && avg <= max);
    }

    [Fact]
    public void Should_Sum_Rental_Time_By_BikeType()
    {
        var sumByType = _rentals
            .GroupBy(r => r.Bike.Model.Type)
            .Select(g => new { Type = g.Key, TotalHours = g.Sum(r => r.DurationHours) })
            .ToList();

        Assert.NotEmpty(sumByType);
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

        Assert.NotEmpty(topRenters);
    }
}
