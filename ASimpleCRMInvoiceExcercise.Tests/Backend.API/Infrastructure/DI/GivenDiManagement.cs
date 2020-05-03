using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using Autofac.Builder;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using NSubstitute;
using Xunit;

namespace Backend.API.Tests.Backend.API.Infrastructure.DI
{
    public class GivenDiManagement
    {
        [Fact]
        public void WeShouldBeAbleToResolveEverything()
        {
            var containerBuilder = new ContainerBuilder();
            var hostingService = NSubstitute.Substitute.For<IHostingEnvironment>();
            hostingService.ContentRootPath.Returns(Directory.GetCurrentDirectory());

            var startup = new Startup(hostingService);

            startup.ConfigureContainer(containerBuilder);

            var container = containerBuilder.Build(ContainerBuildOptions.None);

            using (new AssertionScope())
            {
                foreach (var componentRegistration in container.ComponentRegistry.Registrations)
                {
                    foreach (var registrationService in componentRegistration.Services)
                    {
                        var registeredTargetType = registrationService.Description;
                        var type = GetType(registeredTargetType);

                        type.Should().NotBe(null, $"Failed to parse type '{registeredTargetType}'");


                        var instance = container.Resolve(type);
                        instance.Should().NotBeNull($"Couldn't resolve type {type.FullName} from the DI Container");
                        instance.Should().BeAssignableTo(componentRegistration.Activator.LimitType, $"Couldn't assign instance of type ${instance?.GetType().FullName} to {componentRegistration.Activator.LimitType.FullName} from the DI Container. Was Assigned to ${type.FullName}");
                    }
                }
            }
        }

        private static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
            {
                return type;
            }
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
