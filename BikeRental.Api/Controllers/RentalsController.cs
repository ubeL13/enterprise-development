using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _service;
    private readonly ILogger<RentalsController> _logger;

    public RentalsController(IRentalService service, ILogger<RentalsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RentalDto>>> GetAll()
    {
        try
        {
            var rentals = await _service.GetAllAsync();
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting rentals");
            return StatusCode(500, "Internal server error");
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
            var rental = await _service.GetByIdAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting rental by id {id}");
            return StatusCode(500);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Create(RentalCreateDto dto)
    {
        try
        {
            var rental = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = rental.Id }, rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating rental");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Update(RentalUpdateDto dto)
    {
        try
        {
            var rental = await _service.UpdateAsync(dto);
            if (rental == null) return NotFound();
            return Ok(rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating rental");
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting rental with id {id}");
            return StatusCode(500);
        }
    }
}
