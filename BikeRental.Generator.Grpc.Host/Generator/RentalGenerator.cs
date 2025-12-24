using Bogus;
using BikeRental.Contracts.Grpc;

namespace BikeRental.Generator.Grpc.Host.Generator;

public static class RentalGenerator
{
    private static readonly Faker Faker = new();

    public static IList<RentalContractMessage> Generate(int count)
    {
        var list = new List<RentalContractMessage>(count);

        for (var i = 0; i < count; i++)
        {
            list.Add(new RentalContractMessage
            {
                Id = Guid.NewGuid().ToString(),
                BikeId = Guid.NewGuid().ToString(),
                RenterId = Guid.NewGuid().ToString(),
                DurationHours = Faker.Random.Int(1, 8),
                StartTime = Faker.Date.Recent().ToString("o")
            });
        }

        return list;
    }
}
