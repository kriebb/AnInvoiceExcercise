using Autofac;
using Backend.API.Domain.Services.CustomerManagement;
using Backend.API.Infrastructure.DI;

namespace Backend.API.CosmosDB.Data.Services.CustomerManagement
{
    internal class ConfigureDependencyContainerForDi : Module, IConfigureDependencyContainer
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

        }
    }

}