using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement
{
    internal class DomainAddressMapper : Profile
    {
        public DomainAddressMapper()
        {
            CreateMap<Domain.CommonManagement.Address, DataModels.CommonManagement.Address>();
        }
    }
}