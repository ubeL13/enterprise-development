using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces
{
    /// <summary>
    /// Service interface for managing bikes.
    /// </summary>
    public interface IBikeService
    {
        /// <summary>
        /// Retrieves all bikes.
        /// </summary>
        Task<IEnumerable<BikeDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a bike by its unique identifier.
        /// </summary>
        Task<BikeDto?> GetByIdAsync(string id);

        /// <summary>
        /// Creates a new bike.
        /// </summary>
        Task<BikeDto> CreateAsync(BikeCreateDto dto);

        /// <summary>
        /// Updates an existing bike.
        /// </summary>
        Task<BikeDto?> UpdateAsync(BikeUpdateDto dto);

        /// <summary>
        /// Deletes a bike by its unique identifier.
        /// </summary>
        Task<bool> DeleteAsync(string id);
    }
}
