using Microsoft.AspNetCore.Mvc;
using BikeRental.Infrastructure.Repositories;
using BikeRental.Domain.Models;

namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikesController : ControllerBase
{
    private readonly BikeRepository _repo;

    public BikesController(BikeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repo.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Bike bike)
    {
        await _repo.CreateAsync(bike);
        return Ok(bike);
    }
}
