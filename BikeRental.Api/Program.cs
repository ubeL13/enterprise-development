using BikeRental.Domain.Models;
using BikeRental.Domain;
using BikeRental.Infrastructure;
using BikeRental.Infrastructure.Repositories;
using BikeRental.Application.Services;
using BikeRental.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using BikeRental.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Configures MongoDB settings
/// </summary>
var mongoConnectionString = builder.Configuration.GetConnectionString("mongo")
    ?? throw new InvalidOperationException("Connection string 'mongo' is missing");

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoConnectionString;
    options.DatabaseName = "BikeRentalDb";
});

builder.Services.AddDbContext<MongoDbContext>(options =>
{
    options.UseMongoDB(mongoConnectionString, "BikeRentalDb");
});

/// <summary>
/// Registers repositories and application services
/// </summary>
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IBikeModelService, BikeModelService>();
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IRenterService, RenterService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// <summary>
/// Configures Swagger for API documentation
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeRental API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();

/// <summary>
/// Seeds initial data if the database is empty
/// </summary>
await SeedDataAsync(app.Services);

app.Run();

/// <summary>
/// Seeds the database with initial bike models, bikes, renters, and rentals
/// </summary>
static async Task SeedDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var provider = scope.ServiceProvider;

    var modelRepo = provider.GetRequiredService<IRepository<BikeModel>>();
    var bikeRepo = provider.GetRequiredService<IRepository<Bike>>();
    var renterRepo = provider.GetRequiredService<IRepository<Renter>>();
    var rentalRepo = provider.GetRequiredService<IRepository<Rental>>();

    if ((await modelRepo.GetAllAsync()).Any())
        return;

    var models = DataSeeder.GetBikeModels();
    foreach (var m in models) await modelRepo.CreateAsync(m);

    var bikes = DataSeeder.GetBikes(models);
    foreach (var b in bikes) await bikeRepo.CreateAsync(b);

    var renters = DataSeeder.GetRenters();
    foreach (var r in renters) await renterRepo.CreateAsync(r);

    var rentals = DataSeeder.GetRentals(bikes, renters);
    foreach (var r in rentals) await rentalRepo.CreateAsync(r);
}
