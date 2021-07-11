using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common.Interfaces;

namespace Ron.Ido.FileStorage
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddFileStorage(this IServiceCollection services, FileStorageSettings settings)
        {
            services.Add(new ServiceDescriptor(typeof(IFileStorageService), provider => {
                return new FileStorageService(settings);
            }, ServiceLifetime.Transient));

            return services;
        }
    }
}
