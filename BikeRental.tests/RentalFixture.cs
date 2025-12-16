using BikeRental.Domain.Models;
using BikeRental.Infrastructure;

namespace BikeRental.Tests;
public class RentalFixture
{
    public List<BikeModel> Models { get; }
    public List<Bike> Bikes { get; }
    public List<Renter> Renters { get; }
    public List<Rental> Rentals { get; }

    public RentalFixture()
    {
        Models = DataSeeder.GetBikeModels();
        Renters = DataSeeder.GetRenters();
        Bikes = DataSeeder.GetBikes(Models);
        Rentals = DataSeeder.GetRentals(Bikes, Renters);
    }
}