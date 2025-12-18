using Microsoft.AspNetCore.Mvc;
using BikeRental.Contracts.Interfaces;
using BikeRental.Contracts.Dtos;
using BikeRental.Domain.Enums;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Provides analytics endpoints for bike rental statistics.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _service;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all available sport bike models.
    /// </summary>
    [HttpGet("sport-models")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetSportModels()
    {
        try
        {
            return Ok(await _service.GetSportModelsAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve sport bike models");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total profit.
    /// </summary>
    [HttpGet("top-profit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopModelProfitDto>>> GetTop5Profit()
    {
        try
        {
            return Ok(await _service.GetTop5ModelsByProfitAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve top 5 models by profit");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves the top 5 bike models by total rental duration.
    /// </summary>
    [HttpGet("top-duration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopModelDurationDto>>> GetTop5Duration()
    {
        try
        {
            return Ok(await _service.GetTop5ModelsByDurationAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve top 5 models by duration");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves rental duration statistics.
    /// </summary>
    [HttpGet("duration-stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetDurationStats()
    {
        try
        {
            return Ok(await _service.GetRentalDurationStatsAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve rental duration statistics");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves total rental duration for a specific bike type.
    /// </summary>
    [HttpGet("duration-by-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<double>> GetDurationByType(BikeType type)
    {
        if (!Enum.IsDefined(typeof(BikeType), type))
            return BadRequest("Invalid bike type");

        try
        {
            return Ok(await _service.GetTotalDurationByBikeTypeAsync(type));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve total duration by bike type");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves the top 3 renters based on rental activity.
    /// </summary>
    [HttpGet("top-renters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopRenterDto>>> GetTopRenters()
    {
        try
        {
            return Ok(await _service.GetTop3RentersAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve top renters");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
