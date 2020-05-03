using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class ContactInfoItemMapper : Profile
    {
        public ContactInfoItemMapper()
        {
            CreateMap<ContactInfoItem, ContactInfo>();
        }
    }
}