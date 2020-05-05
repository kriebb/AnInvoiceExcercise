using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement.Mappers
{
    internal class DataCustomerMapper : Profile
    {
        public DataCustomerMapper()
        {
            CreateMap<Domain.CustomerManagement.Customer, DataModels.CustomerManagement.Customer>()
                .ForMember(x => x.TimeToLive, x => x.Ignore())
                .ForMember(x => x.ResourceId, x => x.Ignore())
                .ForMember(x => x.SelfLink, x => x.Ignore())
                .ForMember(x => x.AltLink, x => x.Ignore())
                .ForMember(x => x.Timestamp, x => x.Ignore())
                .ForMember(x => x.ETag, x => x.Ignore());
        }
    }
}