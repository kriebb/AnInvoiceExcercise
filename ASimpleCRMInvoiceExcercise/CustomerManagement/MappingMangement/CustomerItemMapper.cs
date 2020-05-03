﻿using AutoMapper;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Dtos.CustomerManagement;

namespace Backend.API.CustomerManagement.MappingMangement
{
    public class CustomerItemMapper : Profile
    {
        public CustomerItemMapper()
        {
            CreateMap<Customer, CustomerItem>();
        }
    }
}