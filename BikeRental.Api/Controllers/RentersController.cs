using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Manages CRUD operations for renters.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(
    IRenterService service,
    ILogger<RentersController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RenterDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve renters");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterDto>> GetById(string id)
    {
        try
        {
            var renter = await service.GetByIdAsync(id);
            return renter is null ? NotFound() : Ok(renter);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve renter with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterDto>> Create([FromBody] RenterCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var renter = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = renter.Id }, renter);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Argument exception: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create renter");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterDto>> Update(string id, [FromBody] RenterUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.Id) return BadRequest("Route id does not match DTO id");

        try
        {
            var renter = await service.UpdateAsync(dto);
            return renter is null ? NotFound() : Ok(renter);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Argument exception: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update renter with id {Id}", id);
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
            logger.LogError(ex, "Failed to delete renter with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
