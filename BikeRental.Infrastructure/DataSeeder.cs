using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;

namespace BikeRental.Infrastructure;

public static class DataSeeder
{
    private static string NewId() => Guid.NewGuid().ToString();

    public static List<BikeModel> GetBikeModels() =>
    [
        new() { Id = NewId(), Name = "Speedster 3000", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 9.5, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 20 },
        new() { Id = NewId(), Name = "Mountain Fury", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 120, BikeWeight = 12.1, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 18 },
        new() { Id = NewId(), Name = "City Comfort", Type = BikeType.City, WheelSize = 27, MaxRiderWeight = 100, BikeWeight = 11.0, BrakeType = "Rim", ModelYear = 2022, HourlyRate = 10 },
        new() { Id = NewId(), Name = "Urban Spirit", Type = BikeType.City, WheelSize = 28, MaxRiderWeight = 110, BikeWeight = 12.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 12 },
        new() { Id = NewId(), Name = "Volt Rider", Type = BikeType.Electric, WheelSize = 27, MaxRiderWeight = 120, BikeWeight = 19.0, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 25 },
        new() { Id = NewId(), Name = "Trail Crusher", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 115, BikeWeight = 13.0, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 22 },
        new() { Id = NewId(), Name = "Sprint Pro", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 95, BikeWeight = 8.8, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 30 },
        new() { Id = NewId(), Name = "EcoRide E2", Type = BikeType.Electric, WheelSize = 26, MaxRiderWeight = 130, BikeWeight = 20.2, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 27 },
        new() { Id = NewId(), Name = "Classic Roadster", Type = BikeType.City, WheelSize = 26, MaxRiderWeight = 90, BikeWeight = 13.0, BrakeType = "Rim", ModelYear = 2021, HourlyRate = 8 },
        new() { Id = NewId(), Name = "Sport Hawk", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 105, BikeWeight = 9.2, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 22 }
    ];

    public static List<Bike> GetBikes(List<BikeModel> models)
    {
        var bikes = new List<Bike>();
        var rnd = new Random();
        string[] colors = { "Red", "Blue", "Black", "White", "Green", "Yellow", "Gray", "Purple" };

        for (var i = 0; i < 20; i++)
        {
            var model = models[rnd.Next(models.Count)];

            bikes.Add(new Bike
            {
                Id = NewId(),
                SerialNumber = $"B{(i + 1):000}",
                Color = colors[rnd.Next(colors.Length)],
                ModelId = model.Id,
                Model = model
            });
        }

        return bikes;
    }

    public static List<Renter> GetRenters() =>
    [
        new() { Id = NewId(), FullName = "John Smith", Phone = "+1 555 111-11-11" },
        new() { Id = NewId(), FullName = "Emily Johnson", Phone = "+1 555 222-22-22" },
        new() { Id = NewId(), FullName = "Michael Brown", Phone = "+1 555 333-33-33" },
        new() { Id = NewId(), FullName = "Sarah Davis", Phone = "+1 555 444-44-44" },
        new() { Id = NewId(), FullName = "Chris Wilson", Phone = "+1 555 555-55-55" },
        new() { Id = NewId(), FullName = "Emma Miller", Phone = "+1 555 666-66-66" },
        new() { Id = NewId(), FullName = "Daniel Moore", Phone = "+1 555 777-77-77" },
        new() { Id = NewId(), FullName = "Sophia Taylor", Phone = "+1 555 888-88-88" },
        new() { Id = NewId(), FullName = "James Anderson", Phone = "+1 555 999-99-99" },
        new() { Id = NewId(), FullName = "Olivia Thomas", Phone = "+1 555 000-00-00" }
    ];

    public static List<Rental> GetRentals(List<Bike> bikes, List<Renter> renters)
    {
        var rnd = new Random();
        var rentals = new List<Rental>();

        for (var i = 0; i < 50; i++)
        {
            var bike = bikes[rnd.Next(bikes.Count)];
            var renter = renters[rnd.Next(renters.Count)];

            rentals.Add(new Rental
            {
                Id = NewId(),
                BikeId = bike.Id,
                Bike = bike,
                RenterId = renter.Id,
                Renter = renter,
                StartTime = DateTime.Now.AddHours(-rnd.Next(1, 300)),
                DurationHours = rnd.Next(1, 10)
            });
        }

        return rentals;
    }
}