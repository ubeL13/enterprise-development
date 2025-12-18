using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentersController : ControllerBase
{
    private readonly IRenterService _service;
    private readonly ILogger<RentersController> _logger;

    public RentersController(IRenterService service, ILogger<RentersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RenterDto>>> GetAll()
    {
        try
        {
            var renters = await _service.GetAllAsync();
            return Ok(renters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting renters");
            return StatusCode(500);
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
            var renter = await _service.GetByIdAsync(id);
            if (renter == null) return NotFound();
            return Ok(renter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting renter by id {id}");
            return StatusCode(500);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterDto>> Create(RenterCreateDto dto)
    {
        try
        {
            var renter = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = renter.Id }, renter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating renter");
            return StatusCode(500);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterDto>> Update(RenterUpdateDto dto)
    {
        try
        {
            var renter = await _service.UpdateAsync(dto);
            if (renter == null) return NotFound();
            return Ok(renter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating renter");
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
            _logger.LogError(ex, $"Error deleting renter with id {id}");
            return StatusCode(500);
        }
    }
}
