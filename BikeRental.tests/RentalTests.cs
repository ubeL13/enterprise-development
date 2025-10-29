using BikeRental.Domain.Enums;
using BikeRental.tests;

namespace BikeRental.Tests;

public class RentalTests : IClassFixture<RentalFixture>
{
    private readonly RentalFixture _fixture;

    public RentalTests(RentalFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void ShouldFindAllSport_Bikes()
    {
        List<string> expectedBikes =  ["SpeedMax", "TurboSport" ];

            var sportBikes = _fixture.Models
                .Where(m => m.Type == BikeType.Sport)
                .Select(m => m.Name)
                .OrderBy(n => n)
                .ToList();

            expectedBikes.Sort();

            Assert.Equal(expectedBikes, sportBikes);
        }

    [Fact]
    public void ShouldCalculateTop5ModelsByProfit()
    {
        var expected = new[]
        {
            new { Name = "TurboSport", Profit = 180m },   // 30 * 6
            new { Name = "Urban 2000", Profit = 98m },    // 14 * 7
            new { Name = "Mountain Pro", Profit = 100m }, // 20 * 5
            new { Name = "VoltBike", Profit = 130m },     // 26 * 5
            new { Name = "EcoBike", Profit = 100m }       // 25 * 4
        }
        .OrderByDescending(x => x.Profit)
        .Take(5)
        .ToList();

        var actual = _fixture.Rentals
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Name = g.Key.Name,
                Profit = g.Sum(r => r.Bike.Model.HourlyRate * r.DurationHours)
            })
            .OrderByDescending(x => x.Profit)
            .Take(5)
            .ToList();

        Assert.Equal(expected.Select(e => e.Name), actual.Select(a => a.Name));
        Assert.Equal(expected.Select(e => e.Profit), actual.Select(a => a.Profit));
    }

    [Fact]
    public void ShouldCalculateTop5ModelsByDuration()
    {
        var expected = new[]
        {
            new { Name = "Urban 2000", TotalDuration = 7 },
            new { Name = "TurboSport", TotalDuration = 6 },
            new { Name = "Mountain Pro", TotalDuration = 5 },
            new { Name = "VoltBike", TotalDuration = 5 },
            new { Name = "EcoBike", TotalDuration = 4 }
        }.ToList();

        var actual = _fixture.Rentals
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Name = g.Key.Name,
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Take(5)
            .ToList();

        Assert.Equal(expected.Select(e => e.Name), actual.Select(a => a.Name));
        Assert.Equal(expected.Select(e => e.TotalDuration), actual.Select(a => a.TotalDuration));

    }

    [Fact]
    public void ShouldFindMinMaxAvgRentalDuration()
    {
        var durations = _fixture.Rentals.Select(r => r.DurationHours).ToList();

        var min = 1;
        var max = 7; 
        var avg = 4.0; 

        Assert.Equal(min, durations.Min());
        Assert.Equal(max, durations.Max());
        Assert.Equal(avg, durations.Average());
    }

    [Fact]
    public void ShouldSumRentalTimeByBikeType()
    {
        var sumByType = _fixture.Rentals
            .GroupBy(r => r.Bike.Model.Type)
            .Select(g => new { Type = g.Key, TotalHours = g.Sum(r => r.DurationHours) })
            .ToList();

        var expected = new Dictionary<BikeType, int>
        {
            { BikeType.Road, 2 },
            { BikeType.Mountain, 6 },
            { BikeType.City, 14 },
            { BikeType.Electric, 9 },
            { BikeType.Sport, 9 }
        };

        foreach (var kv in expected)
        {
            var actualHours = sumByType.First(x => x.Type == kv.Key).TotalHours;
            Assert.Equal(kv.Value, actualHours);
        }
    }

    [Fact]
    public void ShouldFindTopRentersByUsage()
    {
        var expected = new[] { "Иванов Иван", "Петров Пётр", "Сидоров Сидор" };

        var topRenters = _fixture.Rentals
            .GroupBy(r => r.Renter)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key.FullName)
            .ToList();

        Assert.Equal(expected, topRenters);
    }
}
