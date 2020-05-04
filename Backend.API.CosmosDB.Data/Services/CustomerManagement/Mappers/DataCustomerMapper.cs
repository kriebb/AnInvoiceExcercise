using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement
{
    internal class DataCustomerMapper : Profile
    {
        public DataCustomerMapper()
        {
            CreateMap<Domain.CustomerManagement.Customer, DataModels.CustomerManagement.Customer>();
        }
    }
}