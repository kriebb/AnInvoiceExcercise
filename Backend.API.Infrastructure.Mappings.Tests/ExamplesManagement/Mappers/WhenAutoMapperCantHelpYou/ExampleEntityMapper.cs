using System.Collections.Generic;
using System.Linq;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Dtos;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;

namespace Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Mappers.WhenAutoMapperCantHelpYou
{
    public class ExampleEntityMapper : IMapper<ExampleEntity, ExampleDto>
    {
        public ExampleDto Map(ExampleEntity source)
        {
            var dto = new ExampleDto { Id = source.Id };
            
            return dto;
        }

        public IEnumerable<ExampleDto> MapMany(IEnumerable<ExampleEntity> sources)
        {
            return sources.Select(Map).ToList();
        }
    }
}