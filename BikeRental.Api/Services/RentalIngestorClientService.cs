using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using BikeRental.Contracts.Grpc;
using BikeRental.Contracts;
using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;


namespace BikeRental.Api.Services;

/// <summary>
/// Фоновый gRPC клиент для получения батчей RentalCreateUpdateDto
/// </summary>
public class BikeRentalGrpcClient(
    RentalIngestor.RentalIngestorClient client,
    IServiceScopeFactory scopeFactory,
    IMapper mapper,
    ILogger<BikeRentalGrpcClient> logger,
    IConfiguration cfg
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Rental gRPC client started");

        using var call = client.StreamRentals(cancellationToken: stoppingToken);

        var requestId = Guid.NewGuid().ToString("N");

        await call.RequestStream.WriteAsync(new RentalGenerationRequest
        {
            RequestId = requestId,
            Count = cfg.GetValue("RentalGenerator:Count", 100),
            BatchSize = cfg.GetValue("RentalGenerator:BatchSize", 10)
        });

        await call.RequestStream.CompleteAsync();

        await foreach (var batch in call.ResponseStream.ReadAllAsync(stoppingToken))
        {
            if (batch.RequestId != requestId)
                continue;

            using var scope = scopeFactory.CreateScope();
            var rentalService = scope.ServiceProvider.GetRequiredService<IRentalService>();

            foreach (var rentalMsg in batch.Rentals)
            {
                var dto = mapper.Map<RentalCreateDto>(rentalMsg);
                await rentalService.CreateAsync(dto);
            }

            logger.LogInformation(
                "Received batch: {count}, isFinal={final}",
                batch.Rentals.Count,
                batch.IsFinal
            );

            if (batch.IsFinal)
                break;
        }
    }
}
