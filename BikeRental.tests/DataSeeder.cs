﻿using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;

namespace BikeRental.Tests;

public static class DataSeeder
{
    public static List<BikeModel> GetBikeModels() =>
    [
        new() { Id = Guid.NewGuid(), Name = "Speedster", Type = BikeType.Road, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 9.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 15 },
        new() { Id = Guid.NewGuid(), Name = "Mountain Pro", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 120, BikeWeight = 11.2, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 20 },
        new() { Id = Guid.NewGuid(), Name = "CityRide", Type = BikeType.City, WheelSize = 27, MaxRiderWeight = 90, BikeWeight = 10.5, BrakeType = "Rim", ModelYear = 2022, HourlyRate = 12 },
        new() { Id = Guid.NewGuid(), Name = "EcoBike", Type = BikeType.Electric, WheelSize = 26, MaxRiderWeight = 110, BikeWeight = 18.3, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 25 },
        new() { Id = Guid.NewGuid(), Name = "TurboSport", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 95, BikeWeight = 8.9, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 30 },
        new() { Id = Guid.NewGuid(), Name = "TrailX", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 115, BikeWeight = 10.8, BrakeType = "Disc", ModelYear = 2022, HourlyRate = 22 },
        new() { Id = Guid.NewGuid(), Name = "Urban 2000", Type = BikeType.City, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 11.0, BrakeType = "Rim", ModelYear = 2024, HourlyRate = 14 },
        new() { Id = Guid.NewGuid(), Name = "SpeedMax", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 105, BikeWeight = 9.0, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 28 },
        new() { Id = Guid.NewGuid(), Name = "VoltBike", Type = BikeType.Electric, WheelSize = 27, MaxRiderWeight = 120, BikeWeight = 19.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 26 },
        new() { Id = Guid.NewGuid(), Name = "ClassicRide", Type = BikeType.City, WheelSize = 26, MaxRiderWeight = 85, BikeWeight = 12.0, BrakeType = "Rim", ModelYear = 2021, HourlyRate = 10 },
    ];

    public static List<Renter> GetRenters() =>
    [
        new() { Id = Guid.NewGuid(), FullName = "Иванов Иван", Phone = "+7 999 111 11 11" },
        new() { Id = Guid.NewGuid(), FullName = "Петров Пётр", Phone = "+7 999 222 22 22" },
        new() { Id = Guid.NewGuid(), FullName = "Сидоров Сидор", Phone = "+7 999 333 33 33" },
        new() { Id = Guid.NewGuid(), FullName = "Кузнецова Анна", Phone = "+7 999 444 44 44" },
        new() { Id = Guid.NewGuid(), FullName = "Попова Мария", Phone = "+7 999 555 55 55" },
        new() { Id = Guid.NewGuid(), FullName = "Волков Николай", Phone = "+7 999 666 66 66" },
        new() { Id = Guid.NewGuid(), FullName = "Фёдорова Дарья", Phone = "+7 999 777 77 77" },
        new() { Id = Guid.NewGuid(), FullName = "Егоров Андрей", Phone = "+7 999 888 88 88" },
        new() { Id = Guid.NewGuid(), FullName = "Смирнова Полина", Phone = "+7 999 999 99 99" },
        new() { Id = Guid.NewGuid(), FullName = "Орлова Елена", Phone = "+7 900 123 45 67" },
    ];

    public static List<Bike> GetBikes(List<BikeModel> models) => 
    [
        new() { Id = Guid.NewGuid(), SerialNumber = "B001", Color = "Red", Model = models[0] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B002", Color = "Blue", Model = models[1] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B003", Color = "Black", Model = models[2] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B004", Color = "White", Model = models[3] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B005", Color = "Gray", Model = models[4] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B006", Color = "Yellow", Model = models[5] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B007", Color = "Orange", Model = models[6] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B008", Color = "Green", Model = models[7] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B009", Color = "Purple", Model = models[8] },
        new() { Id = Guid.NewGuid(), SerialNumber = "B010", Color = "Pink", Model = models[9] },
    ];

    public static List<Rental> GetRentals(List<Bike> bikes, List<Renter> renters) => 
    [
        new() { Id = Guid.NewGuid(), Bike = bikes[0], Renter = renters[0], StartTime = DateTime.Now.AddHours(-10), DurationHours = 2 },
        new() { Id = Guid.NewGuid(), Bike = bikes[1], Renter = renters[1], StartTime = DateTime.Now.AddHours(-9), DurationHours = 5 },
        new() { Id = Guid.NewGuid(), Bike = bikes[2], Renter = renters[2], StartTime = DateTime.Now.AddHours(-8), DurationHours = 3 },
        new() { Id = Guid.NewGuid(), Bike = bikes[3], Renter = renters[3], StartTime = DateTime.Now.AddHours(-7), DurationHours = 4 },
        new() { Id = Guid.NewGuid(), Bike = bikes[4], Renter = renters[4], StartTime = DateTime.Now.AddHours(-6), DurationHours = 6 },
        new() { Id = Guid.NewGuid(), Bike = bikes[5], Renter = renters[5], StartTime = DateTime.Now.AddHours(-5), DurationHours = 1 },
        new() { Id = Guid.NewGuid(), Bike = bikes[6], Renter = renters[6], StartTime = DateTime.Now.AddHours(-4), DurationHours = 7 },
        new() { Id = Guid.NewGuid(), Bike = bikes[7], Renter = renters[7], StartTime = DateTime.Now.AddHours(-3), DurationHours = 3 },
        new() { Id = Guid.NewGuid(), Bike = bikes[8], Renter = renters[8], StartTime = DateTime.Now.AddHours(-2), DurationHours = 5 },
        new() { Id = Guid.NewGuid(), Bike = bikes[9], Renter = renters[9], StartTime = DateTime.Now.AddHours(-1), DurationHours = 4 },
    ];
}
