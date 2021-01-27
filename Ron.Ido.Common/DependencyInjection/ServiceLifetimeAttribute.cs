using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ron.Ido.Common.DependencyInjection
{
    public class ServiceLifetimeAttribute : Attribute
    {
        public readonly ServiceLifetime Lifetime;

        public ServiceLifetimeAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
