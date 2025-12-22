var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

builder.AddProject<Projects.BikeRental_Api>("api")
    .WithReference(mongo)
    .WaitFor(mongo);

builder.Build().Run();
