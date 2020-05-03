using AutoMapper;
using Backend.API.Domain.CommonManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class AddressMapper:Profile
    {
        public AddressMapper()
        {
            CreateMap<Address, AddressItem>();
        }
    }
}