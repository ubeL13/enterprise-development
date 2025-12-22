using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Infrastructure;

public class BikeRentalDbSeeder(
    IRepository<BikeModel> modelRepo,
    IRepository<Bike> bikeRepo,
    IRepository<Renter> renterRepo,
    IRepository<Rental> rentalRepo)
{
    public async Task SeedAsync()
    {
        if ((await modelRepo.GetAllAsync()).Count != 0)
            return;

        var models = DataSeeder.GetBikeModels();
        foreach (var m in models)
            await modelRepo.CreateAsync(m);

        var bikes = DataSeeder.GetBikes(models);
        foreach (var b in bikes)
            await bikeRepo.CreateAsync(b);

        var renters = DataSeeder.GetRenters();
        foreach (var r in renters)
            await renterRepo.CreateAsync(r);

        var rentals = DataSeeder.GetRentals(bikes, renters);
        foreach (var r in rentals)
            await rentalRepo.CreateAsync(r);
    }
}
