using Microsoft.AspNetCore.Mvc;
using BikeRental.Contracts.Interfaces;
using BikeRental.Contracts.Dtos;
using BikeRental.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Provides analytics endpoints for bike rental statistics.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    [HttpGet("sport-models")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetSportModels()
    {
        try
        {
            var models = await service.GetSportModelsAsync();
            return Ok(models);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve sport bike models");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("top-profit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopModelProfitDto>>> GetTop5Profit()
    {
        try
        {
            var topModels = await service.GetTop5ModelsByProfitAsync();
            return Ok(topModels);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve top 5 models by profit");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("top-duration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopModelDurationDto>>> GetTop5Duration()
    {
        try
        {
            var topModels = await service.GetTop5ModelsByDurationAsync();
            return Ok(topModels);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve top 5 models by duration");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("duration-stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetDurationStats()
    {
        try
        {
            var stats = await service.GetRentalDurationStatsAsync();
            return Ok(stats);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve rental duration statistics");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("duration-by-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<double>> GetDurationByType([FromQuery, Required] BikeType? type)
    {
        if (!type.HasValue || !Enum.IsDefined(typeof(BikeType), type.Value))
            return BadRequest("Invalid or missing bike type");

        try
        {
            var duration = await service.GetTotalDurationByBikeTypeAsync(type.Value);
            return Ok(duration);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve total duration by bike type");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("top-renters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopRenterDto>>> GetTopRenters()
    {
        try
        {
            var renters = await service.GetTop3RentersAsync();
            return Ok(renters);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve top renters");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
