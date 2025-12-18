using BikeRental.Contracts.Dtos;

namespace BikeRental.Contracts.Interfaces
{
    /// <summary>
    /// Service interface for managing renters.
    /// </summary>
    public interface IRenterService
    {
        /// <summary>
        /// Retrieves all renters.
        /// </summary>
        Task<IEnumerable<RenterDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a renter by its unique identifier.
        /// </summary>
        Task<RenterDto?> GetByIdAsync(string id);

        /// <summary>
        /// Creates a new renter.
        /// </summary>
        Task<RenterDto> CreateAsync(RenterCreateDto dto);

        /// <summary>
        /// Updates an existing renter.
        /// </summary>
        Task<RenterDto?> UpdateAsync(RenterUpdateDto dto);

        /// <summary>
        /// Deletes a renter by its unique identifier.
        /// </summary>
        Task<bool> DeleteAsync(string id);
    }
}

