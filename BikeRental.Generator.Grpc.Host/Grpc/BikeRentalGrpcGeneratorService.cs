using AutoMapper;
using BikeRental.Contracts.Grpc;
using Grpc.Core;
using BikeRental.Generator.Grpc.Host.Generator;

namespace BikeRental.Generator.Grpc.Host.Grpc;

public sealed class BikeRentalGrpcGeneratorService(
    IConfiguration configuration,
    IMapper mapper,
    ILogger<BikeRentalGrpcGeneratorService> logger
) : RentalIngestor.RentalIngestorBase
{
    private readonly int _defaultBatchSize = int.Parse(configuration["Generator:BatchSize"] ?? "10");
    private readonly int _waitTimeSeconds = int.Parse(configuration["Generator:WaitTime"] ?? "2");

    public override async Task StreamRentals(
        IAsyncStreamReader<RentalGenerationRequest> requestStream,
        IServerStreamWriter<RentalBatchStreamMessage> responseStream,
        ServerCallContext context)
    {
        await foreach (var req in requestStream.ReadAllAsync(context.CancellationToken))
        {
            var batchSize = req.BatchSize > 0 ? req.BatchSize : _defaultBatchSize;
            var sent = 0;

            while (sent < req.Count && !context.CancellationToken.IsCancellationRequested)
            {
                var take = Math.Min(batchSize, req.Count - sent);
                var rentals = RentalGenerator.Generate(take);

                var payload = new RentalBatchStreamMessage
                {
                    RequestId = req.RequestId,
                    IsFinal = sent + take >= req.Count
                };
                payload.Rentals.AddRange(rentals);

                await responseStream.WriteAsync(payload);
                sent += take;

                if (!payload.IsFinal && _waitTimeSeconds > 0)
                    await Task.Delay(_waitTimeSeconds * 1000, context.CancellationToken);
            }
        }
    }
}
