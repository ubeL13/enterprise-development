using BikeRental.Domain.Models;
using BikeRental.Infrastructure;
using BikeRental.Infrastructure.Repositories;
using BikeRental.Infrastructure.Services;
using BikeRental.Infrastructure.Settings;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(options =>
{
    var conn = builder.Configuration.GetConnectionString("mongo") ?? throw new InvalidOperationException("Connection string 'mongo' is missing");
    options.ConnectionString = conn;
    options.DatabaseName = "BikeRentalDb";
});

builder.Services.AddDbContext<MongoDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("mongo")
        ?? throw new InvalidOperationException("Connection string 'mongo' is missing");

    options.UseMongoDB(connectionString, "BikeRentalDb");
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<AnalyticsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var modelRepo = services.GetRequiredService<IRepository<BikeModel>>();
    var bikeRepo = services.GetRequiredService<IRepository<Bike>>();
    var renterRepo = services.GetRequiredService<IRepository<Renter>>();
    var rentalRepo = services.GetRequiredService<IRepository<Rental>>();

    if ((await modelRepo.GetAllAsync()).Count == 0)
    {
        var models = DataSeeder.GetBikeModels();
        foreach (var m in models) await modelRepo.CreateAsync(m);

        var bikes = DataSeeder.GetBikes(models);
        foreach (var b in bikes) await bikeRepo.CreateAsync(b);

        var renters = DataSeeder.GetRenters();
        foreach (var r in renters) await renterRepo.CreateAsync(r);

        var rentals = DataSeeder.GetRentals(bikes, renters);
        foreach (var r in rentals) await rentalRepo.CreateAsync(r);
    }
}

app.Run();
