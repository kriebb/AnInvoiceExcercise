using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CommonManagement.Mappers
{
    internal class DomainAddressMapper : Profile
    {
        public DomainAddressMapper()
        {
            CreateMap<Domain.CommonManagement.Address, DataModels.CommonManagement.Address>();
        }
    }
}