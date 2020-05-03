using AutoMapper;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Dtos;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;

namespace Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Mappers.Default
{
    public class ExampleProfile : Profile //Will be scanned by the Startup,  All Profiles will be in the mapping configuration for automapper. You don't have to register this .
    {
        public ExampleProfile()
        {
            CreateMap(typeof(ExampleEntity), typeof(ExampleDto));
        }
    }
}