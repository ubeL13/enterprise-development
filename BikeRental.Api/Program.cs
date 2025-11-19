using BikeRental.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Load Mongo settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<MongoDbContext>();

// Register repositories
builder.Services.AddScoped<BikeRepository>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
