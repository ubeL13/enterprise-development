using BikeRental.Domain.Enums;
using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces;

public interface IAnalyticsService
{
    Task<IEnumerable<string>> GetSportModelsAsync();
    Task<IEnumerable<TopModelProfitDto>> GetTop5ModelsByProfitAsync();
    Task<IEnumerable<TopModelDurationDto>> GetTop5ModelsByDurationAsync();
    Task<RentalDurationStatsDto> GetRentalDurationStatsAsync();
    Task<double> GetTotalDurationByBikeTypeAsync(BikeType type);
    Task<IEnumerable<TopRenterDto>> GetTop3RentersAsync();
}
