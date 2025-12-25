using AutoMapper;
using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Grpc;

namespace BikeRental.Api.Services;

public class BikeRentalGrpcProfile : Profile
{
    public BikeRentalGrpcProfile()
    {
        CreateMap<RentalContractMessage, RentalCreateDto>();
    }
}
