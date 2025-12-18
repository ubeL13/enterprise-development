using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces
{
    /// <summary>
    /// Service interface for managing bike rentals.
    /// </summary>
    public interface IRentalService
    {
        /// <summary>
        /// Retrieves all rentals.
        /// </summary>
        Task<IEnumerable<RentalDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a rental by its unique identifier.
        /// </summary>
        Task<RentalDto?> GetByIdAsync(string id);

        /// <summary>
        /// Creates a new rental.
        /// </summary>
        Task<RentalDto> CreateAsync(RentalCreateDto dto);

        /// <summary>
        /// Updates an existing rental.
        /// </summary>
        Task<RentalDto?> UpdateAsync(RentalUpdateDto dto);

        /// <summary>
        /// Deletes a rental by its unique identifier.
        /// </summary>
        Task<bool> DeleteAsync(string id);
    }
}
