using BikeRental.Domain;
using Xunit;
using System.Linq;
using BikeRental.Domain.Models;

namespace BikeRental.Tests;

public class BikeTests
{
    private readonly List<BikeModel> _models;

    public BikeTests()
    {
        _models = DataSeeder.GetBikeModels();
    }

    [Fact]
    public void All_Bikes_Should_Have_Positive_HourlyRate()
    {
        Assert.All(_models, m => Assert.True(m.HourlyRate > 0));
    }

    [Fact]
    public void All_Bikes_Should_Have_Valid_Year()
    {
        Assert.All(_models, m => Assert.InRange(m.ModelYear, 2000, 2025));
    }
}
