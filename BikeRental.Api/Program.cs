using BikeRental.Domain.Models;
using BikeRental.Infrastructure;
using BikeRental.Infrastructure.Repositories;
using BikeRental.Infrastructure.Services;
using BikeRental.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// MongoDb settings
builder.Services.Configure<MongoDbSettings>(options =>
{
    var conn = builder.Configuration.GetConnectionString("mongo");

    options.ConnectionString = conn;
    options.DatabaseName = "BikeRentalDb";
});

builder.Services.AddSingleton<MongoDbContext>();

// Generic repositories
builder.Services.AddScoped<IRepository<Bike>, MongoRepository<Bike>>(sp =>
    new MongoRepository<Bike>(sp.GetRequiredService<MongoDbContext>(), "Bikes"));

builder.Services.AddScoped<IRepository<BikeModel>, MongoRepository<BikeModel>>(sp =>
    new MongoRepository<BikeModel>(sp.GetRequiredService<MongoDbContext>(), "BikeModels"));

builder.Services.AddScoped<IRepository<Renter>, MongoRepository<Renter>>(sp =>
    new MongoRepository<Renter>(sp.GetRequiredService<MongoDbContext>(), "Renters"));

builder.Services.AddScoped<IRepository<Rental>, MongoRepository<Rental>>(sp =>
    new MongoRepository<Rental>(sp.GetRequiredService<MongoDbContext>(), "Rentals"));

// Analytics service
builder.Services.AddScoped<AnalyticsService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeRental API v1");
        c.RoutePrefix = string.Empty; // Swagger на /
    });
}

app.UseHttpsRedirection();
app.MapControllers();

// --- Seed data ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var modelRepo = services.GetRequiredService<IRepository<BikeModel>>();
    var bikeRepo = services.GetRequiredService<IRepository<Bike>>();
    var renterRepo = services.GetRequiredService<IRepository<Renter>>();
    var rentalRepo = services.GetRequiredService<IRepository<Rental>>();

    if (!(await modelRepo.GetAllAsync()).Any())
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
