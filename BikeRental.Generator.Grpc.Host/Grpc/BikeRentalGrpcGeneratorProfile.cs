using AutoMapper;
using BikeRental.Contracts.Grpc;

namespace BikeRental.Generator.Grpc.Host.Grpc;

public sealed class BikeRentalGrpcGeneratorProfile : Profile
{
    public BikeRentalGrpcGeneratorProfile()
    {
        CreateMap<RentalContractMessage, RentalContractMessage>();
    }
}
