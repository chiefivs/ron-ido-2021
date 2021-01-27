using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ron.Ido.Common.DependencyInjection
{
    public static class ServiceCollectionExt
    {
        #region Dependencies
        public static IServiceCollection AddDependencies(this IServiceCollection collection, params Assembly[] assemblies)
        {
            var deptype = typeof(IDependency);
            if (!assemblies.Any())
            {
                assemblies = _GetAssemblies(null).ToArray();
            }

            //  все типы, имеющие интерфейс IDependency
            var alltypes = assemblies
            .SelectMany(a => a.GetTypes()
                .Where(t => t.GetInterfaces().Contains(deptype)));

            var interfaces = alltypes.Where(t => t.IsInterface);
            var classes = alltypes.Where(t => t.IsClass && !t.IsAbstract);

            //  сначала регистрируем реализации интерфейсов, наследующих IDependency
            foreach (var i in interfaces)
            {
                var types = classes.Where(t => t.GetInterfaces().Contains(i));
                foreach (var t in types)
                {
                    var attr = t.GetCustomAttribute<ServiceLifetimeAttribute>();
                    var lifetime = attr?.Lifetime ?? ServiceLifetime.Transient;
                    collection.Add(new ServiceDescriptor(i, t, lifetime));
                }
            }

            //  после регистрируем классы, наследующие IDependency напрямую
            foreach (var t in classes.Where(t => !t.GetInterfaces().Any(i => interfaces.Contains(i))))
            {
                var attr = t.GetCustomAttribute<ServiceLifetimeAttribute>();
                var lifetime = attr?.Lifetime ?? ServiceLifetime.Transient;
                collection.Add(new ServiceDescriptor(t, t, lifetime));
            }

            return collection;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection collection, params Type[] markerTypes)
        {
            var assemblies = markerTypes.Select(t => t.Assembly).ToArray();
            return AddDependencies(collection, assemblies);
        }
        #endregion

        private static IEnumerable<Assembly> _GetAssemblies(Func<AssemblyName, bool> assemblyFilter)
        {
            var entry = Assembly.GetEntryAssembly();
            if (assemblyFilter == null)
            {
                var prefix = entry.FullName.Split('.').First();
                assemblyFilter = name => name.FullName.StartsWith(prefix);
            }

            return new[] { entry }
                .Union(entry.GetReferencedAssemblies()
                    .Where(assemblyFilter)
                    .Select(Assembly.Load));
        }
    }
}
