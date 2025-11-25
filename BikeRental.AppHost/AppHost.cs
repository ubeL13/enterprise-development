using Aspire.Hosting;
using Aspire.Hosting.MongoDB;

var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var api = builder.AddProject<Projects.BikeRental_Api>("api")
    .WithReference(mongo);

builder.Build().Run();
