using BikeRental.Application.Services;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain;
using BikeRental.Infrastructure;
using BikeRental.Infrastructure.Repositories;
using BikeRental.Infrastructure.Settings;
using BikeRental.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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
builder.Services.AddScoped<BikeRentalDbSeeder>();


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
app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<BikeRentalDbSeeder>();
    await seeder.SeedAsync();
}


app.Run();