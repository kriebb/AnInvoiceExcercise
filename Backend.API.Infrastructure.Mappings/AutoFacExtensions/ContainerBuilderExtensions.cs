using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;

namespace Backend.API.Infrastructure.Mappings.AutoFacExtensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterAutoMapper(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            var assemblyTypes = assemblies.SelectMany(x => x.GetTypes());

            var assembliesTypes = assemblyTypes.Where(p => typeof(Profile).IsAssignableFrom(p) && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();
            builder.Register(ctx =>
            {
                var mappingConfiguration = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in autoMapperProfiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });
                mappingConfiguration.AssertConfigurationIsValid(); //validation on startup 
                return mappingConfiguration;
            });

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope(); //For injection in the automapper Adapter
        }
    }
}