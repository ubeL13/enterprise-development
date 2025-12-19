using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

/// <summary>
/// Manages CRUD operations for bike rentals.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController(
    IRentalService service,
    ILogger<RentalsController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RentalDto>>> GetAll()
    {
        try
        {
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve rentals");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> GetById(string id)
    {
        try
        {
            var rental = await service.GetByIdAsync(id);
            return rental is null ? NotFound() : Ok(rental);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve rental with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Create([FromBody] RentalCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var rental = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = rental.Id }, rental);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Argument exception: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Key not found: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create rental");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Update(string id, [FromBody] RentalUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != dto.Id) return BadRequest("Route id does not match DTO id");

        try
        {
            var rental = await service.UpdateAsync(dto);
            return rental is null ? NotFound() : Ok(rental);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Argument exception: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Key not found: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update rental with id {Id}", id);
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
            logger.LogError(ex, "Failed to delete rental with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}
