using BikeRental.Domain.Models;
using BikeRental.Infrastructure;
using BikeRental.Infrastructure.Repositories;
using BikeRental.Infrastructure.Services;
using BikeRental.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(options =>
{
    var conn = builder.Configuration.GetConnectionString("mongo") ?? throw new InvalidOperationException("Connection string 'mongo' is missing");
    options.ConnectionString = conn;
    options.DatabaseName = "BikeRentalDb";
});

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IRepository<Bike>, MongoRepository<Bike>>(sp =>
    new MongoRepository<Bike>(sp.GetRequiredService<MongoDbContext>(), "Bikes"));

builder.Services.AddScoped<IRepository<BikeModel>, MongoRepository<BikeModel>>(sp =>
    new MongoRepository<BikeModel>(sp.GetRequiredService<MongoDbContext>(), "BikeModels"));

builder.Services.AddScoped<IRepository<Renter>, MongoRepository<Renter>>(sp =>
    new MongoRepository<Renter>(sp.GetRequiredService<MongoDbContext>(), "Renters"));

builder.Services.AddScoped<IRepository<Rental>, MongoRepository<Rental>>(sp =>
    new MongoRepository<Rental>(sp.GetRequiredService<MongoDbContext>(), "Rentals"));

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
        c.RoutePrefix = string.Empty; // Swagger на /
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
