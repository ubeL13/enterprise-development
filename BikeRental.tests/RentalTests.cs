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

    /// <summary>
    /// Checks that all models of type Sport are correctly found.
    /// </summary>
    [Fact]
    public void ShouldFindAllSport_Bikes()
    {
        List<string> expected =  ["SpeedMax", "TurboSport" ];

            var actual = _fixture.Models
                .Where(m => m.Type == BikeType.Sport)
                .Select(m => m.Name)
                .OrderBy(n => n)
                .ToList();

            Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Checks the top 5 models by total profit and their order.
    /// </summary>
    [Fact]
    public void ShouldCalculateTop5ModelsByProfit()
    {
        List<string> expected =
        [
            "TurboSport",
            "VoltBike",
            "Mountain Pro",
            "EcoBike",
            "Urban 2000"
        ];

        var actual = _fixture.Rentals
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Name = g.Key.Name,
                Profit = g.Sum(r => r.Bike.Model.HourlyRate * r.DurationHours)
            })
            .OrderByDescending(x => x.Profit)
            .Take(5)
            .Select(x => x.Name)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Checks the top 5 models by total rental duration and their order.
    /// </summary>
    [Fact]
    public void ShouldCalculateTop5ModelsByDuration()
    {
        List<string> expected =
        [
            "Urban 2000",
            "TurboSport",
            "Mountain Pro",
            "VoltBike",
            "EcoBike"
        ];

        var actual = _fixture.Rentals
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Name = g.Key.Name,
                Duration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.Duration)
            .Take(5)
            .Select(x => x.Name)
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Checks the minimum, maximum, and average rental duration.
    /// </summary>
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

    /// <summary>
    /// Checks the total rental time by bike type.
    /// </summary>
    [Theory]
    [InlineData(BikeType.Road, 2)]
    [InlineData(BikeType.Mountain, 6)]
    [InlineData(BikeType.City, 14)]
    [InlineData(BikeType.Electric, 9)]
    [InlineData(BikeType.Sport, 9)]
    public void ShouldSumRentalTimeByBikeType(BikeType bikeType, int expected)
    {
        var actual = _fixture.Rentals
            .Where(r => r.Bike.Model.Type == bikeType)
            .Sum(r => r.DurationHours);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Checks the top 3 renters by rental count.
    /// </summary>
    [Fact]
    public void ShouldFindTopRentersByUsage()
    {
        List<string> expected = [ 
            "Иванов Иван",
            "Петров Пётр", 
            "Сидоров Сидор" 
        ];

        var actual  = _fixture.Rentals
            .GroupBy(r => r.Renter)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key.FullName)
            .ToList();

        Assert.Equal(expected, actual);
    }
}
