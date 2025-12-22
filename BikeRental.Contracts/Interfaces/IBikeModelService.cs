using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces;

/// <summary>
/// Service interface for managing bike models.
/// </summary>
public interface IBikeModelService
{
    /// <summary>
    /// Retrieves all bike models.
    /// </summary>
    public Task<IEnumerable<BikeModelDto>> GetAllAsync();

    /// <summary>
    /// Retrieves a bike model by its unique identifier.
    /// </summary>
    public Task<BikeModelDto> GetByIdAsync(string id);

    /// <summary>
    /// Creates a new bike model.
    /// </summary>
    public Task<BikeModelDto> CreateAsync(BikeModelCreateDto dto);

    /// <summary>
    /// Updates an existing bike model.
    /// </summary>
    public Task<BikeModelDto?> UpdateAsync(BikeModelUpdateDto dto);

    /// <summary>
    /// Deletes a bike model by its unique identifier.
    /// </summary>
    public Task<bool> DeleteAsync(string id);
}
