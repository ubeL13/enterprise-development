var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var batchSize = builder.AddParameter("GeneratorBatchSize");
var waitTime = builder.AddParameter("GeneratorWaitTime");

var generator = builder.AddProject<Projects.BikeRental_Generator_Grpc_Host>("generator")
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:WaitTime", waitTime);

builder.AddProject<Projects.BikeRental_Api>("api")
    .WithReference(mongo)
    .WithReference(generator)
    .WithEnvironment("RentalIngestor:GrpcAddress", generator.GetEndpoint("https"))
    .WaitFor(mongo)
    .WaitFor(generator);

builder.Build().Run();
