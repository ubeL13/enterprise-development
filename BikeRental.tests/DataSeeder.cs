//// DataSeeder.cs
//using System;
//using System.Collections.Generic;
//using BikeRental.Domain.Enums;
//using BikeRental.Domain.Models;
//using MongoDB.Bson;

//namespace BikeRental.Tests;

//public static class DataSeeder
//{
//    public static List<BikeModel> GetBikeModels() => new()
//    {
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Speedster", Type = BikeType.Road, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 9.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 15 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Mountain Pro", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 120, BikeWeight = 11.2, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 20 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "CityRide", Type = BikeType.City, WheelSize = 27, MaxRiderWeight = 90, BikeWeight = 10.5, BrakeType = "Rim", ModelYear = 2022, HourlyRate = 12 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "EcoBike", Type = BikeType.Electric, WheelSize = 26, MaxRiderWeight = 110, BikeWeight = 18.3, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 25 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "TurboSport", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 95, BikeWeight = 8.9, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 30 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "TrailX", Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 115, BikeWeight = 10.8, BrakeType = "Disc", ModelYear = 2022, HourlyRate = 22 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Urban 2000", Type = BikeType.City, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 11.0, BrakeType = "Rim", ModelYear = 2024, HourlyRate = 14 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "SpeedMax", Type = BikeType.Sport, WheelSize = 28, MaxRiderWeight = 105, BikeWeight = 9.0, BrakeType = "Disc", ModelYear = 2024, HourlyRate = 28 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "VoltBike", Type = BikeType.Electric, WheelSize = 27, MaxRiderWeight = 120, BikeWeight = 19.5, BrakeType = "Disc", ModelYear = 2023, HourlyRate = 26 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "ClassicRide", Type = BikeType.City, WheelSize = 26, MaxRiderWeight = 85, BikeWeight = 12.0, BrakeType = "Rim", ModelYear = 2021, HourlyRate = 10 },
//    };

//    public static List<Renter> GetRenters() => new ()
//    {
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Ivanov Ivan", Phone = "+7 999 111 11 11" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Petrov Petr", Phone = "+7 999 222 22 22" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Sidorov Sidor", Phone = "+7 999 333 33 33" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Kuznetsova Anna", Phone = "+7 999 444 44 44" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Popova Maria", Phone = "+7 999 555 55 55" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Volkov Nikolay", Phone = "+7 999 666 66 66" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Fedorova Darya", Phone = "+7 999 777 77 77" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Egorov Andrey", Phone = "+7 999 888 88 88" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Smirnova Polina", Phone = "+7 999 999 99 99" },
//        new() { Id = ObjectId.GenerateNewId().ToString(), FullName = "Orlova Elena", Phone = "+7 900 123 45 67" },
//    };

//    public static List<Bike> GetBikes(List<BikeModel> models) => new()
//    {
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B001", Color = "Red", Model = models[0], ModelId = models[0].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B002", Color = "Blue", Model = models[1], ModelId = models[1].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B003", Color = "Black", Model = models[2], ModelId = models[2].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B004", Color = "White", Model = models[3], ModelId = models[3].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B005", Color = "Gray", Model = models[4], ModelId = models[4].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B006", Color = "Yellow", Model = models[5], ModelId = models[5].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B007", Color = "Orange", Model = models[6], ModelId = models[6].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B008", Color = "Green", Model = models[7], ModelId = models[7].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B009", Color = "Purple", Model = models[8], ModelId = models[8].Id },
//        new() { Id = ObjectId.GenerateNewId().ToString(), SerialNumber = "B010", Color = "Pink", Model = models[9], ModelId = models[9].Id },
//    };

//    public static List<Rental> GetRentals(List<Bike> bikes, List<Renter> renters) => new()
//    {
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[0], Renter = renters[0], BikeId = bikes[0].Id, RenterId = renters[0].Id, StartTime = DateTime.Now.AddHours(-10), DurationHours = 2 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[1], Renter = renters[1], BikeId = bikes[1].Id, RenterId = renters[1].Id, StartTime = DateTime.Now.AddHours(-9), DurationHours = 5 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[2], Renter = renters[2], BikeId = bikes[2].Id, RenterId = renters[2].Id, StartTime = DateTime.Now.AddHours(-8), DurationHours = 3 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[3], Renter = renters[3], BikeId = bikes[3].Id, RenterId = renters[3].Id, StartTime = DateTime.Now.AddHours(-7), DurationHours = 4 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[4], Renter = renters[4], BikeId = bikes[4].Id, RenterId = renters[4].Id, StartTime = DateTime.Now.AddHours(-6), DurationHours = 6 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[5], Renter = renters[5], BikeId = bikes[5].Id, RenterId = renters[5].Id, StartTime = DateTime.Now.AddHours(-5), DurationHours = 1 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[6], Renter = renters[6], BikeId = bikes[6].Id, RenterId = renters[6].Id, StartTime = DateTime.Now.AddHours(-4), DurationHours = 7 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[7], Renter = renters[7], BikeId = bikes[7].Id, RenterId = renters[7].Id, StartTime = DateTime.Now.AddHours(-3), DurationHours = 3 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[8], Renter = renters[8], BikeId = bikes[8].Id, RenterId = renters[8].Id, StartTime = DateTime.Now.AddHours(-2), DurationHours = 5 },
//        new() { Id = ObjectId.GenerateNewId().ToString(), Bike = bikes[9], Renter = renters[9], BikeId = bikes[9].Id, RenterId = renters[9].Id, StartTime = DateTime.Now.AddHours(-1), DurationHours = 4 },
//    };
//}
