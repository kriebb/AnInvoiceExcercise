using Autofac;
using Backend.API.Infrastructure.DI;
using Backend.API.Infrastructure.Mappings.Impl;

namespace Backend.API.Infrastructure.Mappings
{
    public class ConfigureDependencyContainer : Module, IConfigureDependencyContainer
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AutoMapperAdapter<,>)).As(typeof(IMapper<,>)).SingleInstance();
        }
    }
}