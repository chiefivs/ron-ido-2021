using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.EM;
using System;

namespace Ron.Ido.DbStorage
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, AppDbContextSettings settings, Action<IServiceProvider, AppDbContext> oncreate = null)
        {
            services.AddScoped(provider =>
            {
                AppDbContext context = settings.CreateAppDbContext();
                oncreate?.Invoke(provider, context);

                return context;
            });

            return services;
        }
    }
}
