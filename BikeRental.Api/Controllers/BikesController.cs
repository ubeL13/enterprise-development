using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Manages CRUD operations for bikes.
/// </summary>
[ApiController]
[Route("api/bikes")]
public class BikesController(
    IBikeService service,
    ILogger<BikesController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BikeDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve bikes");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeDto>> GetById(string id)
    {
        try
        {
            var result = await service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeDto>> Create([FromBody] BikeCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var result = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Argument exception: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create bike");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeDto>> Update(string id, [FromBody] BikeUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.Id) return BadRequest("Route id does not match DTO id");

        try
        {
            var result = await service.UpdateAsync(dto);
            return result is null ? NotFound() : Ok(result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Argument exception: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
