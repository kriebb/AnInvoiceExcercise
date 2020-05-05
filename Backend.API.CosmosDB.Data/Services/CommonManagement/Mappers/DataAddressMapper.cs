using AutoMapper;

namespace Backend.API.CosmosDB.Data.Services.CommonManagement.Mappers
{
    internal class DataAddressMapper : Profile
    {
        public DataAddressMapper()
        {
            CreateMap<DataModels.CommonManagement.Address, Domain.CommonManagement.Address>();
        }
    }
}