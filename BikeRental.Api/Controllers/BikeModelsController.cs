using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Manages CRUD operations for bike models.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeModelsController(
    IBikeModelService service,
    ILogger<BikeModelsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all bike models.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BikeModelDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve bike models");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves a bike model by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> GetById(string id)
    {
        try
        {
            var result = await service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new bike model.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> Create([FromBody] BikeModelCreateDto dto)
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
            logger.LogError(ex, "Failed to create bike model");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing bike model.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> Update(string id, [FromBody] BikeModelUpdateDto dto)
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
            logger.LogError(ex, "Failed to update bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a bike model by its ID.
    /// </summary>
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
            logger.LogError(ex, "Failed to delete bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
