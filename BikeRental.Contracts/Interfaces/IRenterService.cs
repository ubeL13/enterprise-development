using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces;

public interface IRenterService
{
    Task<IEnumerable<RenterDto>> GetAllAsync();
    Task<RenterDto?> GetByIdAsync(string id);
    Task<RenterDto> CreateAsync(RenterCreateDto dto);
    Task<RenterDto?> UpdateAsync(RenterUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}
