using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces;

public interface IBikeService
{
    Task<IEnumerable<BikeDto>> GetAllAsync();
    Task<BikeDto?> GetByIdAsync(string id);
    Task<BikeDto> CreateAsync(BikeCreateDto dto);
    Task<BikeDto?> UpdateAsync(BikeUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}
