using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using Backend.API.Infrastructure.Mappings.Impl;

namespace Backend.API.Infrastructure.Mappings.BootstrapAutoMapper
{
    public class AutoMapperFactory
    {
        public IMapper CreateMapper()
        {
            var mce = new MapperConfigurationExpression();

            var mc = GetMapperConfiguration(mce);
            var m = new Mapper(mc);

            return m;
        }

        private Assembly[] GetAllAssemblies()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
        }

        private MapperConfiguration GetMapperConfiguration(MapperConfigurationExpression mce)
        {
            var assemblies = GetAllAssemblies();
            var assemblyTypes = assemblies.SelectMany(x => x.GetTypes());

            var assembliesTypes = assemblyTypes.Where(p => typeof(Profile).IsAssignableFrom(p) && !p.IsAbstract)
                .Distinct();

            var profiles = assembliesTypes.Where(x => x.GetConstructors().Any(constructorInfo => !constructorInfo.GetParameters().Any()))
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            mce.AddProfiles(profiles);

            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();
            return mc;
        }

        public IMapper<TSource, TDestination> CreateMapper<TSource, TDestination>()
        {
            var autoMapper = CreateMapper();
            var mapper = new AutoMapperAdapter<TSource, TDestination>(autoMapper);

            return mapper;
        }
    }
}
