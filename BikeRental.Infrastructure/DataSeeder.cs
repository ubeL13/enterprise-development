using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;

namespace BikeRental.Infrastructure;

public static class SeedIds
{
    public const string Bike1 = "bike-0001";
    public const string Bike2 = "bike-0002";
    public const string Bike3 = "bike-0003";
    public const string Bike4 = "bike-0004";
    public const string Bike5 = "bike-0005";
    public const string Bike6 = "bike-0006";
    public const string Bike7 = "bike-0007";
    public const string Bike8 = "bike-0008";
    public const string Bike9 = "bike-0009";
    public const string Bike10 = "bike-0010";

    public const string Renter1 = "renter-0001";
    public const string Renter2 = "renter-0002";
    public const string Renter3 = "renter-0003";
    public const string Renter4 = "renter-0004";
    public const string Renter5 = "renter-0005";
    public const string Renter6 = "renter-0006";
    public const string Renter7 = "renter-0007";
    public const string Renter8 = "renter-0008";
    public const string Renter9 = "renter-0009";
    public const string Renter10 = "renter-0010";
}

public static class DataSeeder
{
    public static List<BikeModel> GetBikeModels() =>
    [
        new() { Id = SeedIds.Bike1, Name = "Speedster", Type = BikeType.Road, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 9.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 15 },
        new() { Id = SeedIds.Bike2, Name = "Mountain Pro", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 120, BikeWeight = 11.2, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 20 },
        new() { Id = SeedIds.Bike3, Name = "CityRide", Type = BikeType.City, WheelSize = 27, MaxRiderWeight = 90, BikeWeight = 10.5, BrakeType = "Rim", ModelYear = 2022, HourlyRate = 12 },
        new() { Id = SeedIds.Bike4, Name = "EcoBike", Type = BikeType.Electric, WheelSize = 26, MaxRiderWeight = 110, BikeWeight = 18.3, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 25 },
        new() { Id = SeedIds.Bike5, Name = "TurboSport", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 95, BikeWeight = 8.9, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 30 },
        new() { Id = SeedIds.Bike6, Name = "TrailX", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 115, BikeWeight = 10.8, BrakeType = "Disc", ModelYear = 2022, HourlyRate = 22 },
        new() { Id = SeedIds.Bike7, Name = "Urban 2000", Type = BikeType.City, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 11.0, BrakeType = "Rim", ModelYear = 2024, HourlyRate = 14 },
        new() { Id = SeedIds.Bike8, Name = "SpeedMax", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 105, BikeWeight = 9.0, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 28 },
        new() { Id = SeedIds.Bike9, Name = "VoltBike", Type = BikeType.Electric, WheelSize = 27, MaxRiderWeight = 120, BikeWeight = 19.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 26 },
        new() { Id = SeedIds.Bike10, Name = "ClassicRide", Type = BikeType.City, WheelSize = 26, MaxRiderWeight = 85, BikeWeight = 12.0, BrakeType = "Rim", ModelYear = 2021, HourlyRate = 10 }
    ];

    public static List<Renter> GetRenters() =>
    [
        new() { Id = SeedIds.Renter1, FullName = "Иванов Иван", Phone = "+7 999 111 11 11" },
        new() { Id = SeedIds.Renter2, FullName = "Петров Пётр", Phone = "+7 999 222 22 22" },
        new() { Id = SeedIds.Renter3, FullName = "Сидоров Сидор", Phone = "+7 999 333 33 33" },
        new() { Id = SeedIds.Renter4, FullName = "Кузнецова Анна", Phone = "+7 999 444 44 44" },
        new() { Id = SeedIds.Renter5, FullName = "Попова Мария", Phone = "+7 999 555 55 55" },
        new() { Id = SeedIds.Renter6, FullName = "Волков Николай", Phone = "+7 999 666 66 66" },
        new() { Id = SeedIds.Renter7, FullName = "Фёдорова Дарья", Phone = "+7 999 777 77 77" },
        new() { Id = SeedIds.Renter8, FullName = "Егоров Андрей", Phone = "+7 999 888 88 88" },
        new() { Id = SeedIds.Renter9, FullName = "Смирнова Полина", Phone = "+7 999 999 99 99" },
        new() { Id = SeedIds.Renter10, FullName = "Орлова Елена", Phone = "+7 900 123 45 67" }
    ];

    public static List<Bike> GetBikes(List<BikeModel> models) =>
    [
        new() { Id = SeedIds.Bike1, SerialNumber = "B001", Color = "Red", Model = models[0], ModelId = models[0].Id },
        new() { Id = SeedIds.Bike2, SerialNumber = "B002", Color = "Blue", Model = models[1], ModelId = models[1].Id },
        new() { Id = SeedIds.Bike3, SerialNumber = "B003", Color = "Black", Model = models[2], ModelId = models[2].Id },
        new() { Id = SeedIds.Bike4, SerialNumber = "B004", Color = "White", Model = models[3], ModelId = models[3].Id },
        new() { Id = SeedIds.Bike5, SerialNumber = "B005", Color = "Gray", Model = models[4], ModelId = models[4].Id },
        new() { Id = SeedIds.Bike6, SerialNumber = "B006", Color = "Yellow", Model = models[5], ModelId = models[5].Id },
        new() { Id = SeedIds.Bike7, SerialNumber = "B007", Color = "Orange", Model = models[6], ModelId = models[6].Id },
        new() { Id = SeedIds.Bike8, SerialNumber = "B008", Color = "Green", Model = models[7], ModelId = models[7].Id },
        new() { Id = SeedIds.Bike9, SerialNumber = "B009", Color = "Purple", Model = models[8], ModelId = models[8].Id },
        new() { Id = SeedIds.Bike10, SerialNumber = "B010", Color = "Pink", Model = models[9], ModelId = models[9].Id }
    ];

    public static List<Rental> GetRentals(List<Bike> bikes, List<Renter> renters) =>
    [
        new() { Id = "rental-0001", Bike = bikes[0], BikeId = bikes[0].Id, Renter = renters[0], RenterId = renters[0].Id, StartTime = DateTime.Now.AddHours(-10), DurationHours = 2 },
        new() { Id = "rental-0002", Bike = bikes[1], BikeId = bikes[1].Id, Renter = renters[1], RenterId = renters[1].Id, StartTime = DateTime.Now.AddHours(-9), DurationHours = 5 },
        new() { Id = "rental-0003", Bike = bikes[2], BikeId = bikes[2].Id, Renter = renters[2], RenterId = renters[2].Id, StartTime = DateTime.Now.AddHours(-8), DurationHours = 3 },
        new() { Id = "rental-0004", Bike = bikes[3], BikeId = bikes[3].Id, Renter = renters[3], RenterId = renters[3].Id, StartTime = DateTime.Now.AddHours(-7), DurationHours = 4 },
        new() { Id = "rental-0005", Bike = bikes[4], BikeId = bikes[4].Id, Renter = renters[4], RenterId = renters[4].Id, StartTime = DateTime.Now.AddHours(-6), DurationHours = 6 },
        new() { Id = "rental-0006", Bike = bikes[5], BikeId = bikes[5].Id, Renter = renters[5], RenterId = renters[5].Id, StartTime = DateTime.Now.AddHours(-5), DurationHours = 1 },
        new() { Id = "rental-0007", Bike = bikes[6], BikeId = bikes[6].Id, Renter = renters[6], RenterId = renters[6].Id, StartTime = DateTime.Now.AddHours(-4), DurationHours = 7 },
        new() { Id = "rental-0008", Bike = bikes[7], BikeId = bikes[7].Id, Renter = renters[7], RenterId = renters[7].Id, StartTime = DateTime.Now.AddHours(-3), DurationHours = 3 },
        new() { Id = "rental-0009", Bike = bikes[8], BikeId = bikes[8].Id, Renter = renters[8], RenterId = renters[8].Id, StartTime = DateTime.Now.AddHours(-2), DurationHours = 5 },
        new() { Id = "rental-0010", Bike = bikes[9], BikeId = bikes[9].Id, Renter = renters[9], RenterId = renters[9].Id, StartTime = DateTime.Now.AddHours(-1), DurationHours = 4 }
    ];
}
