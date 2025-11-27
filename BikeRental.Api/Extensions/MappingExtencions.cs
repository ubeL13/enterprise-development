using BikeRental.Api.DTO;
using BikeRental.Domain.Models;

namespace BikeRental.Api.Extensions
{
    public static class MappingExtensions
    {
        public static BikeDto ToDto(this Bike bike) =>
            new BikeDto
            {
                Id = bike.Id,
                SerialNumber = bike.SerialNumber,
                Color = bike.Color,
                ModelId = bike.ModelId
            };

        public static RenterDto ToDto(this Renter renter) =>
            new RenterDto
            {
                Id = renter.Id,
                FullName = renter.FullName,
                Phone = renter.Phone
            };

        public static BikeModelDto ToDto(this BikeModel model) =>
        new BikeModelDto
        {
            Id = model.Id,
            Name = model.Name
        };
    }
}
