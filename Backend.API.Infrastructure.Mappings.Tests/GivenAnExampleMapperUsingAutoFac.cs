using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Backend.API.Infrastructure.DI;
using Backend.API.Infrastructure.Mappings.AutoFacExtensions;
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
        public Assembly[] GetAllAssemblies()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
        }
        [Fact]
        public void WhenWeCallRegisterMapper_WeSHouldBeAbleToRetrieveUsingIMapper()
        {
            var assemblies = GetAllAssemblies();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAutoMapper(assemblies);
            containerBuilder.RegisterProjectModules(assemblies);

            var container = containerBuilder.Build();

            var entity = new ExampleEntity(){Id = Guid.NewGuid()};
            var mapper = container.Resolve<IMapper<ExampleEntity, ExampleDto>>();

            var dto = mapper.Map(entity);

            Assert.Equal(entity.Id, dto.Id);
        }
    }
}