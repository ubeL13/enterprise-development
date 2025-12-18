using BikeRental.Domain.Enums;
using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces
{
    /// <summary>
    /// Service interface for retrieving analytics and statistics related to bike rentals.
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Retrieves all bike models of type Sport.
        /// </summary>
        Task<IEnumerable<string>> GetSportModelsAsync();

        /// <summary>
        /// Retrieves the top 5 bike models ranked by total profit.
        /// </summary>
        Task<IEnumerable<TopModelProfitDto>> GetTop5ModelsByProfitAsync();

        /// <summary>
        /// Retrieves the top 5 bike models ranked by total rental duration.
        /// </summary>
        Task<IEnumerable<TopModelDurationDto>> GetTop5ModelsByDurationAsync();

        /// <summary>
        /// Retrieves statistics for rental durations, including minimum, maximum, and average durations.
        /// </summary>
        Task<RentalDurationStatsDto> GetRentalDurationStatsAsync();

        /// <summary>
        /// Retrieves the total rental duration for bikes of a specific type.
        /// </summary>
        Task<double> GetTotalDurationByBikeTypeAsync(BikeType type);

        /// <summary>
        /// Retrieves the top 3 renters by number of rentals.
        /// </summary>
        Task<IEnumerable<TopRenterDto>> GetTop3RentersAsync();
    }
}
