using System;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Backend.API.Infrastructure.Mappings.Impl;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Dtos;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;
using Xunit;

namespace Backend.API.Infrastructure.Mappings.Tests
{
    public class GivenAnExampleMapperUsingAutoFac
    {
        [Fact]
        public void WhenWeHaveASourceObject_ThenWeCanMapToADestinationObject()
        {
            var autoMapper = new AutoMapperFactory().CreateMapper();
            var entity = new ExampleEntity { Id = Guid.NewGuid() };
            var mapper = new AutoMapperAdapter<ExampleEntity, ExampleDto>(autoMapper);
            var dto = mapper.Map(entity);

            Assert.Equal(entity.Id, dto.Id);
        }
    }
}