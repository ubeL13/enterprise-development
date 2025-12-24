using BikeRental.Generator.Grpc.Host.Grpc;
using BikeRental.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new BikeRentalGrpcGeneratorProfile()));
builder.Services.AddGrpc(options => options.EnableDetailedErrors = builder.Environment.IsDevelopment());

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<BikeRentalGrpcGeneratorService>();

app.Run();
