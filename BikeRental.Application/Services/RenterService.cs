using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

public class RenterService : IRenterService
{
    private readonly IRepository<Renter> _repository;

    public RenterService(IRepository<Renter> repository)
    {
        _repository = repository;
    }

    private static RenterDto ToDto(Renter r) => new()
    {
        Id = r.Id,
        FullName = r.FullName,
        Phone = r.Phone
    };

    public async Task<IEnumerable<RenterDto>> GetAllAsync()
    {
        var renters = await _repository.GetAllAsync();
        return renters.Select(ToDto);
    }

    public async Task<RenterDto?> GetByIdAsync(string id)
    {
        var renter = await _repository.GetByIdAsync(id);
        return renter == null ? null : ToDto(renter);
    }

    public async Task<RenterDto> CreateAsync(RenterCreateDto dto)
    {
        var renter = new Renter
        {
            FullName = dto.FullName,
            Phone = dto.Phone
        };

        await _repository.CreateAsync(renter);
        return ToDto(renter);
    }

    public async Task<RenterDto?> UpdateAsync(RenterUpdateDto dto)
    {
        var renter = await _repository.GetByIdAsync(dto.Id);
        if (renter == null) return null;

        renter.FullName = dto.FullName;
        renter.Phone = dto.Phone;

        await _repository.UpdateAsync(renter.Id, renter);
        return ToDto(renter);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var renter = await _repository.GetByIdAsync(id);
        if (renter == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
