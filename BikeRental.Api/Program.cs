using BikeRental.Infrastructure;
using BikeRental.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Mongo settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<MongoDbContext>();

// Generic repos
builder.Services.AddScoped<IRepository<Bike>, MongoRepository<Bike>>(sp =>
    new MongoRepository<Bike>(sp.GetRequiredService<MongoDbContext>(), "Bikes"));

builder.Services.AddScoped<IRepository<BikeModel>, MongoRepository<BikeModel>>(sp =>
    new MongoRepository<BikeModel>(sp.GetRequiredService<MongoDbContext>(), "BikeModels"));

builder.Services.AddScoped<IRepository<Renter>, MongoRepository<Renter>>(sp =>
    new MongoRepository<Renter>(sp.GetRequiredService<MongoDbContext>(), "Renters"));

builder.Services.AddScoped<IRepository<Rental>, MongoRepository<Rental>>(sp =>
    new MongoRepository<Rental>(sp.GetRequiredService<MongoDbContext>(), "Rentals"));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
