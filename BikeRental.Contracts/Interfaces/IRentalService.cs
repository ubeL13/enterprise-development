using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces;

public interface IRentalService
{
    Task<IEnumerable<RentalDto>> GetAllAsync();
    Task<RentalDto?> GetByIdAsync(string id);
    Task<RentalDto> CreateAsync(RentalCreateDto dto);
    Task<RentalDto?> UpdateAsync(RentalUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}
