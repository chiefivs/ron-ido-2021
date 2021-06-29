using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.EM;

namespace Ron.Ido.Tests.Mocks
{
    public class MockAppDbContext : AppDbContext
    {
        public MockAppDbContext() : base(_createOptions()) { }

        private static DbContextOptions<AppDbContext> _createOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkProxies()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
