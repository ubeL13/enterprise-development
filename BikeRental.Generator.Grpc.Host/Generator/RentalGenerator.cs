using Bogus;
using BikeRental.Contracts.Grpc;
using BikeRental.Infrastructure;

namespace BikeRental.Generator.Grpc.Host.Generator;

public static class RentalGenerator
{
    private static readonly Faker Faker = new();
    private static readonly string[] BikeIds = new[]
    {
        SeedIds.Bike1, SeedIds.Bike2, SeedIds.Bike3, SeedIds.Bike4, SeedIds.Bike5,
        SeedIds.Bike6, SeedIds.Bike7, SeedIds.Bike8, SeedIds.Bike9, SeedIds.Bike10
    };

    private static readonly string[] RenterIds = new[]
    {
        SeedIds.Renter1, SeedIds.Renter2, SeedIds.Renter3, SeedIds.Renter4, SeedIds.Renter5,
        SeedIds.Renter6, SeedIds.Renter7, SeedIds.Renter8, SeedIds.Renter9, SeedIds.Renter10
    };

    public static IList<RentalContractMessage> Generate(int count)
    {
        var list = new List<RentalContractMessage>(count);
        for (var i = 0; i < count; i++)
        {
            list.Add(new RentalContractMessage
            {
                Id = Guid.NewGuid().ToString(),
                BikeId = Faker.PickRandom(BikeIds),
                RenterId = Faker.PickRandom(RenterIds),
                DurationHours = Faker.Random.Int(1, 8),
                StartTime = Faker.Date.Recent().ToString("o")
            });
        }
        return list;
    }
}

