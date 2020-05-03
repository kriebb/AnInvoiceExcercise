using System;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Mappers.WhenAutoMapperCantHelpYou;
using Xunit;

namespace Backend.API.Infrastructure.Mappings.Tests
{
    public class GivenAnExampleMapperUsingOwnImplementation
    {
        [Fact]
        public void WhenWeHaveASourceObject_ThenWeCanMapToADestinationObject()
        {
            var entity = new ExampleEntity { Id = Guid.NewGuid() };
            var mapper = new ExampleEntityMapper();
            var dto = mapper.Map(entity);

            Assert.Equal(entity.Id, dto.Id);
        }
    }
}