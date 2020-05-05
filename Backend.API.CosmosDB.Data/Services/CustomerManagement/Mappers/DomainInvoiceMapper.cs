using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement.Mappers
{
    internal class DomainCustomerMapper : Profile
    {
        public DomainCustomerMapper()
        {
            CreateMap<DataModels.CustomerManagement.Customer, Domain.CustomerManagement.Customer>();

        }
    }
}