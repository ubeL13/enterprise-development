using AutoMapper;
using BikeRental.Contracts.Grpc;

namespace BikeRental.Generator.Grpc.Host.Grpc;

/// <summary>
/// AutoMapper profile for the BikeRental gRPC generator.
/// </summary>
public sealed class BikeRentalGrpcGeneratorProfile : Profile
{
    /// <summary>
    /// Configures mapping rules.
    /// </summary>
    public BikeRentalGrpcGeneratorProfile()
    {
        CreateMap<RentalContractMessage, RentalContractMessage>();
    }
}
