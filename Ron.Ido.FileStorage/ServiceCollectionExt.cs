using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common.Interfaces;

namespace Ron.Ido.FileStorage
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddFileStorage<TFileInfo>(this IServiceCollection services) where TFileInfo : class, IFileInfo, new()
        {
            services.Add(new ServiceDescriptor(typeof(IFileStorageService), provider => {
                var config = provider.GetService<IConfiguration>();
                return new FileStorageService<TFileInfo>(config);
            }, ServiceLifetime.Transient));

            return services;
        }
    }
}
