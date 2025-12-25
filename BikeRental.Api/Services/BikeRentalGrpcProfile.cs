using AutoMapper;
using BikeRental.Contracts.Dtos;
using BikeRental.Contracts.Grpc;

namespace BikeRental.Api.Services;

/// <summary>
/// AutoMapper profile for mapping gRPC rental contract messages
/// received from the generator service into internal
/// </summary>
public class BikeRentalGrpcProfile : Profile
{
    public BikeRentalGrpcProfile()
    {
        CreateMap<RentalContractMessage, RentalCreateDto>();
    }
}
