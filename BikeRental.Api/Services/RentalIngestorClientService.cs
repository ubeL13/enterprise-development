using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

using BikeRental.Contracts.Grpc;
using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;

namespace BikeRental.Api.Services;

/// <summary>
/// Фоновый gRPC клиент для получения батчей RentalCreateDto
/// и создания аренд в системе
/// </summary>
public class BikeRentalGrpcClient(
    RentalIngestor.RentalIngestorClient client,
    IServiceScopeFactory scopeFactory,
    IMapper mapper,
    ILogger<BikeRentalGrpcClient> logger,
    IConfiguration cfg,
    IMemoryCache cache
) : BackgroundService
{
    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(10);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var count = cfg.GetValue("RentalGenerator:Count", 100);
                var batchSize = cfg.GetValue("RentalGenerator:BatchSize", 10);

                logger.LogInformation("Connecting to RentalGenerator gRPC stream...");

                using var call = client.StreamRentals(cancellationToken: stoppingToken);

                var requestId = Guid.NewGuid().ToString("N");

                // отправляем запрос генератору
                var writerTask = Task.Run(async () =>
                {
                    await call.RequestStream.WriteAsync(new RentalGenerationRequest
                    {
                        RequestId = requestId,
                        Count = count,
                        BatchSize = batchSize
                    });

                    await call.RequestStream.CompleteAsync();
                }, stoppingToken);

                await foreach (var batch in call.ResponseStream.ReadAllAsync(stoppingToken))
                {
                    if (batch.RequestId != requestId)
                        continue;

                    var dtos = batch.Rentals
                        .Select(mapper.Map<RentalCreateDto>)
                        .ToList();

                    using var scope = scopeFactory.CreateScope();

                    var rentalService = scope.ServiceProvider.GetRequiredService<IRentalService>();
                    var bikeService = scope.ServiceProvider.GetRequiredService<IBikeService>();
                    var renterService = scope.ServiceProvider.GetRequiredService<IRenterService>();

                    var valid = new List<RentalCreateDto>();

                    foreach (var dto in dtos)
                    {
                        if (!await ExistsAsync(dto.BikeId, "Bike", dto, bikeService.GetByIdAsync, stoppingToken))
                            continue;

                        if (!await ExistsAsync(dto.RenterId, "Renter", dto, renterService.GetByIdAsync, stoppingToken))
                            continue;

                        valid.Add(dto);
                    }

                    var created = 0;

                    foreach (var dto in valid)
                    {
                        await rentalService.CreateAsync(dto);
                        created++;
                    }

                    logger.LogInformation(
                        "Received batch: total={total}, valid={valid}, created={created}, isFinal={isFinal}",
                        dtos.Count,
                        valid.Count,
                        created,
                        batch.IsFinal
                    );

                    if (batch.IsFinal)
                        break;
                }

                await writerTask;

                logger.LogInformation("Finished receiving rentals for requestId={requestId}", requestId);
                break;
            }
            catch (RpcException ex) when (!stoppingToken.IsCancellationRequested)
            {
                logger.LogError(ex, "gRPC stream error: {code} - {status}", ex.StatusCode, ex.Status);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                logger.LogError(ex, "Unexpected error in BikeRentalGrpcClient");
                break;
            }
        }
    }

    /// <summary>
    /// Проверка существования сущности по id с кешированием
    /// </summary>
    private async Task<bool> ExistsAsync<TEntity>(
        string id,
        string entityName,
        RentalCreateDto dto,
        Func<string, Task<TEntity?>> readFunc,
        CancellationToken ct)
        where TEntity : class
    {
        var cacheKey = $"{entityName}:exists:{id}";

        if (cache.TryGetValue(cacheKey, out bool cached))
            return cached;

        ct.ThrowIfCancellationRequested();

        bool exists;
        try
        {
            var entity = await readFunc(id);
            exists = entity is not null;
        }
        catch (KeyNotFoundException)
        {
            exists = false;

            logger.LogWarning(
                "Skipping rental because {entity} with id {id} was not found (bikeId={bikeId}, renterId={renterId})",
                entityName,
                id,
                dto.BikeId,
                dto.RenterId
            );
        }

        cache.Set(
            cacheKey,
            exists,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = CacheTtl
            }
        );

        return exists;
    }
}
