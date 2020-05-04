using Autofac;
using Backend.API.Infrastructure.DI;

namespace Backend.API.CosmosDB.Data.Services.InvoiceManagement
{
    internal class ConfigureDependencyContainerForDi : Module, IConfigureDependencyContainer
    {
        protected override void Load(ContainerBuilder builder)
        {
            
        }
    }
}