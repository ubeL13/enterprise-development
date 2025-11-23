using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;
using MongoDB.Bson;

namespace BikeRental.Infrastructure;

public static class DataSeeder
{
    public static List<BikeModel> GetBikeModels() => new()
    {
        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Speedster", Type = BikeType.Road, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 9.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 15 },
        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Mountain Pro", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 120, BikeWeight = 11.2, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 20 },
        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "CityRide", Type = BikeType.City, WheelSize = 27, MaxRiderWeight = 90, BikeWeight = 10.5, BrakeType = "Rim", ModelYear = 2022, HourlyRate = 12 },
        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "EcoBike", Type = BikeType.Electric, WheelSize = 26, MaxRiderWeight = 110, BikeWeight = 18.3, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 25 },
        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "TurboSport", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 95, BikeWeight = 8.9, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 30 }
    };

    public static List<Bike> GetBikes(List<BikeModel> models) => new()
    {
        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B001", Color = "Red", ModelId = models[0].Id, Model = models[0] },
        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B002", Color = "Blue", ModelId = models[1].Id, Model = models[1] },
        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B003", Color = "Black", ModelId = models[2].Id, Model = models[2] },
        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B004", Color = "White", ModelId = models[3].Id, Model = models[3] },
        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B005", Color = "Gray", ModelId = models[4].Id, Model = models[4] }
    };

    public static List<Renter> GetRenters() => new()
    {
        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "John Smith", Phone = "+1 111 111 1111" },
        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Peter Johnson", Phone = "+1 222 222 2222" },
        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Michael Brown", Phone = "+1 333 333 3333" },
        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Anna Wilson", Phone = "+1 444 444 4444" },
        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Maria Davis", Phone = "+1 555 555 5555" }
    };

    public static List<Rental> GetRentals(List<Bike> bikes, List<Renter> renters)
    {
        var rentals = new List<Rental>();

        for (int i = 0; i < Math.Min(bikes.Count, renters.Count); i++)
        {
            rentals.Add(new Rental
            {
                Id = ObjectId.GenerateNewId().ToString(),
                BikeId = bikes[i].Id,
                Bike = bikes[i],
                RenterId = renters[i].Id,
                Renter = renters[i],
                StartTime = DateTime.Now.AddHours(-(i + 1) * 2),
                DurationHours = 2 + i
            });
        }

        return rentals;
    }
}
