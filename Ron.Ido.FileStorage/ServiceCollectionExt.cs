using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common.Interfaces;

namespace Ron.Ido.FileStorage
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddFileStorage<TFileInfo>(this IServiceCollection services, FileStorageSettings settings) where TFileInfo : class, IFileInfo, new()
        {
            services.Add(new ServiceDescriptor(typeof(IFileStorageService), provider => {
                return new FileStorageService<TFileInfo>(settings);
            }, ServiceLifetime.Transient));

            return services;
        }
    }
}
