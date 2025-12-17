using Microsoft.AspNetCore.Mvc;
using BikeRental.Contracts.Interfaces;
using BikeRental.Contracts.Dtos;
using BikeRental.Domain.Enums;

namespace BikeRental.Api.Controllers;

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

    [HttpGet("sport-models")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> GetSportModels()
    {
        try
        {
            var result = await _service.GetSportModelsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении спортивных моделей");
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpGet("top-profit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopModelProfitDto>>> GetTop5Profit()
    {
        try
        {
            var result = await _service.GetTop5ModelsByProfitAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении топ-5 моделей по прибыли");
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpGet("top-duration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopModelDurationDto>>> GetTop5Duration()
    {
        try
        {
            var result = await _service.GetTop5ModelsByDurationAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении топ-5 моделей по продолжительности аренды");
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpGet("duration-stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetDurationStats()
    {
        try
        {
            var result = await _service.GetRentalDurationStatsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении статистики по продолжительности аренды");
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpGet("duration-by-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<double>> GetDurationByType(BikeType type)
    {
        try
        {
            var result = await _service.GetTotalDurationByBikeTypeAsync(type);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении суммарной продолжительности аренды по типу велосипеда");
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }

    [HttpGet("top-renters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopRenterDto>>> GetTopRenters()
    {
        try
        {
            var result = await _service.GetTop3RentersAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении топ-3 арендаторов");
            return StatusCode(500, "Произошла ошибка при обработке запроса");
        }
    }
}
