using Microsoft.AspNetCore.Mvc;
using BikeRental.Infrastructure.Services;
using BikeRental.Domain.Enums;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Provides analytics data for the bike rental system.
/// </summary>
/// <remarks>
/// Initializes a new instance of the "AnalyticsController" class.
/// </remarks>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(AnalyticsService service) : ControllerBase
{
    private readonly AnalyticsService _service = service;

    /// <summary>
    /// Retrieves all bikes categorized as "sport" models.
    /// </summary>
    [HttpGet("sport-models")]
    public async Task<IActionResult> GetSportModels()
        => Ok(await _service.GetSportModelsAsync());

    /// <summary>
    /// Retrieves the top 5 bike models by total profit.
    /// </summary>
    [HttpGet("top-profit")]
    public async Task<IActionResult> GetTop5Profit()
        => Ok(await _service.GetTop5ModelsByProfitAsync());

    /// <summary>
    /// Retrieves the top 5 bike models by total rental duration.
    /// </summary>
    [HttpGet("top-duration")]
    public async Task<IActionResult> GetTop5Duration()
        => Ok(await _service.GetTop5ModelsByDurationAsync());

    /// <summary>
    /// Retrieves statistics about rental durations, including minimum, maximum, and average.
    /// </summary>
    [HttpGet("duration-stats")]
    public async Task<IActionResult> GetDurationStats()
    {
        var (min, max, avg) = await _service.GetRentalDurationStatsAsync();
        return Ok(new { min, max, avg });
    }

    /// <summary>
    /// Retrieves total rental duration for bikes of a specific type.
    /// </summary>
    [HttpGet("duration-by-type")]
    public async Task<IActionResult> GetDurationByType(BikeType type)
        => Ok(await _service.GetTotalDurationByBikeTypeAsync(type));

    /// <summary>
    /// Retrieves the top 3 renters based on total rentals.
    /// </summary>
    [HttpGet("top-renters")]
    public async Task<IActionResult> GetTopRenters()
        => Ok(await _service.GetTop3RentersAsync());
}
