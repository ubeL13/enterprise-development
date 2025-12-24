using AutoMapper;
using BikeRental.Contracts.Grpc;
using BikeRental.Contracts.Dtos;

namespace BikeRental.Api.Services;

public class BikeRentalGrpcProfile : Profile
{
	public BikeRentalGrpcProfile()
	{
		CreateMap<RentalContractMessage, RentalCreateDto>();
	}
}
