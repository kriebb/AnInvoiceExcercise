using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement
{
    internal class DomainCustomerMapper : Profile
    {
        public DomainCustomerMapper()
        {
            CreateMap<Domain.CustomerManagement.Customer, DataModels.CustomerManagement.Customer>();

        }
    }
}