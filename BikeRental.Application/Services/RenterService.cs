using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Interfaces;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services
{
    /// <summary>
    /// Service implementation for managing renters.
    /// </summary>
    public class RenterService : IRenterService
    {
        private readonly IRepository<Renter> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterService"/> class.
        /// </summary>
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

        /// <summary>
        /// Retrieves all renters.
        /// </summary>
        public async Task<IEnumerable<RenterDto>> GetAllAsync()
        {
            var renters = await _repository.GetAllAsync();
            return renters.Select(ToDto);
        }

        /// <summary>
        /// Retrieves a renter by its unique identifier.
        /// </summary>
        public async Task<RenterDto?> GetByIdAsync(string id)
        {
            var renter = await _repository.GetByIdAsync(id);
            return renter == null ? null : ToDto(renter);
        }

        /// <summary>
        /// Creates a new renter.
        /// </summary>
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

        /// <summary>
        /// Updates an existing renter.
        /// </summary>
        public async Task<RenterDto?> UpdateAsync(RenterUpdateDto dto)
        {
            var renter = await _repository.GetByIdAsync(dto.Id);
            if (renter == null) return null;

            renter.FullName = dto.FullName;
            renter.Phone = dto.Phone;

            await _repository.UpdateAsync(renter.Id, renter);
            return ToDto(renter);
        }

        /// <summary>
        /// Deletes a renter by its unique identifier.
        /// </summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var renter = await _repository.GetByIdAsync(id);
            if (renter == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
