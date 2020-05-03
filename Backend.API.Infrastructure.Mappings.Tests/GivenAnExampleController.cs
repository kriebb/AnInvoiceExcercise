using System;
using Backend.API.Infrastructure.Mappings.BootstrapAutoMapper;
using Backend.API.Infrastructure.Mappings.Impl;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Controllers;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Dtos;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Mappers.WhenAutoMapperCantHelpYou;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Repositories;
using Xunit;

namespace Backend.API.Infrastructure.Mappings.Tests
{
    public class GivenAnExampleController
    {
        [Fact]
        public void WhenWeRequestAnExampleDtoForAnId_ThenWeRetrieveTheIdUsingOwnMapper()
        {
            //DI MAGIC
            var mapper = new ExampleEntityMapper();
            var exampleController = new ExampleController(new FakeRepository(), mapper);
            //END DI Magic

            var id = Guid.NewGuid();
            var dto = exampleController.Get(id);
            Assert.Equal(id, dto.Id);
        }

        [Fact]
        public void WhenWeRequestAnExampleDtoForAnId_ThenWeRetrieveTheIdUsingAutoMapper()
        {
            //DI MAGIC
            var autoMapper = new AutoMapperFactory().CreateMapper();
            IMapper<ExampleEntity, ExampleDto> mapper = new AutoMapperAdapter<ExampleEntity, ExampleDto>(autoMapper);
            var exampleController = new ExampleController(new FakeRepository(), mapper);
            //END DI Magic

            var id = Guid.NewGuid();
            var dto = exampleController.Get(id);
            Assert.Equal(id, dto.Id);
        }
    }
}