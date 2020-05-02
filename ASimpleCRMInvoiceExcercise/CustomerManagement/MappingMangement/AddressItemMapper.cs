﻿using AutoMapper;
using Backend.API.Domain.CommonManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingManagement
{
    public class AddressItemMapper : Profile
    {
        public AddressItemMapper()
        {
            CreateMap<AddressItem, Address>();
        }
    }
}