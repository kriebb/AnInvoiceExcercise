using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace Backend.API.Infrastructure.DI
{
    public interface IConfigureDependencyContainer:IModule
    {
    }

    public static class Extensions
    {
        public static void RegisterProjectModules(this ContainerBuilder builder, Assembly[] assemblies )
        {

            foreach (var ass in assemblies)
            {
                foreach (System.Reflection.TypeInfo ti in ass.DefinedTypes)
                {
                    if (ti.ImplementedInterfaces.Contains(typeof(IConfigureDependencyContainer)))
                    {
                        var module = (IConfigureDependencyContainer) ass.CreateInstance(ti.FullName);
                        builder.RegisterModule(module);
                    }
                }
            }
        }
    }
}
