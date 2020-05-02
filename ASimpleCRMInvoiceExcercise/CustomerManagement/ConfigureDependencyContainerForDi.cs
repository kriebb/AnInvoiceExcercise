using Autofac;
using Backend.API.ErrorManagement;
using Backend.API.ErrorManagement.Impl;
using Backend.API.Infrastructure.DI;

namespace Backend.API.CustomerManagement
{
    internal class ConfigureDependencyContainerForDi : Module, IConfigureDependencyContainer
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ErrorService>().As<IErrorService>();
        }
    }
}