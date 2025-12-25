using BikeRental.Contracts.Grpc;
using BikeRental.Infrastructure;
using Bogus;

namespace BikeRental.Generator.Grpc.Host.Generator;

/// <summary>
/// Static generator for creating fake rental data for testing purposes.
/// Uses Bogus library to produce random but realistic values for rentals.
/// </summary>
public static class RentalGenerator
{
    private static readonly Faker _faker = new();
    private static readonly string[] _bikeIds =
    [
        SeedIds.Bike1, SeedIds.Bike2, SeedIds.Bike3, SeedIds.Bike4, SeedIds.Bike5,
        SeedIds.Bike6, SeedIds.Bike7, SeedIds.Bike8, SeedIds.Bike9, SeedIds.Bike10
    ];

    private static readonly string[] _renterIds =
    [
        SeedIds.Renter1, SeedIds.Renter2, SeedIds.Renter3, SeedIds.Renter4, SeedIds.Renter5,
        SeedIds.Renter6, SeedIds.Renter7, SeedIds.Renter8, SeedIds.Renter9, SeedIds.Renter10
    ];

    /// <summary>
    /// Generates a list of <see cref="RentalContractMessage"/> objects with random data.
    /// </summary>
    public static IList<RentalContractMessage> Generate(int count)
    {
        var list = new List<RentalContractMessage>(count);
        for (var i = 0; i < count; i++)
        {
            list.Add(new RentalContractMessage
            {
                Id = Guid.NewGuid().ToString(),
                BikeId = _faker.PickRandom(_bikeIds),
                RenterId = _faker.PickRandom(_renterIds),
                DurationHours = _faker.Random.Int(1, 8),
                StartTime = _faker.Date.Recent().ToString("o")
            });
        }
        return list;
    }
}

