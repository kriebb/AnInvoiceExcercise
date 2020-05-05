using Autofac;
using Backend.API.CosmosDB.Data.Services.Infrastructure.Impl;
using Backend.API.Infrastructure.DI;
using Microsoft.Extensions.Configuration;

namespace Backend.API.CosmosDB.Data.Services.Infrastructure
{
    internal class ConfigureDependencyContainerForDi : Module, IConfigureDependencyContainer
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType(typeof(DocumentDbRepository<>)).As(typeof(IDocumentDbRepository<>));
            builder.Register<CosmosDbConfig>((context, parameters) =>
            {
                var cosmosDbConfig = new CosmosDbConfig();
                var configuration = context.Resolve<IConfiguration>();
                configuration.GetSection(nameof(CosmosDbConfig)).Bind(cosmosDbConfig);
                return cosmosDbConfig;
            });
        }
    }

}