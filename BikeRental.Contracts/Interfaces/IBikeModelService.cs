using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces;

public interface IBikeModelService
{
    Task<IEnumerable<BikeModelDto>> GetAllAsync();
    Task<BikeModelDto> GetByIdAsync(string id);
    Task<BikeModelDto> CreateAsync(BikeModelCreateDto dto);
    Task<BikeModelDto?> UpdateAsync(BikeModelUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}
