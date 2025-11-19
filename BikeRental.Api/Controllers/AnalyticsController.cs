using Microsoft.AspNetCore.Mvc;
using BikeRental.Infrastructure.Services;
using BikeRental.Domain.Enums;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _service;

    public AnalyticsController(AnalyticsService service)
    {
        _service = service;
    }

    [HttpGet("sport-models")]
    public async Task<IActionResult> GetSportModels()
        => Ok(await _service.GetSportModelsAsync());

    [HttpGet("top-profit")]
    public async Task<IActionResult> GetTop5Profit()
        => Ok(await _service.GetTop5ModelsByProfitAsync());

    [HttpGet("top-duration")]
    public async Task<IActionResult> GetTop5Duration()
        => Ok(await _service.GetTop5ModelsByDurationAsync());

    [HttpGet("duration-stats")]
    public async Task<IActionResult> GetDurationStats()
    {
        var (min, max, avg) = await _service.GetRentalDurationStatsAsync();
        return Ok(new { min, max, avg });
    }

    [HttpGet("duration-by-type")]
    public async Task<IActionResult> GetDurationByType(BikeType type)
        => Ok(await _service.GetTotalDurationByBikeTypeAsync(type));

    [HttpGet("top-renters")]
    public async Task<IActionResult> GetTopRenters()
        => Ok(await _service.GetTop3RentersAsync());
}
