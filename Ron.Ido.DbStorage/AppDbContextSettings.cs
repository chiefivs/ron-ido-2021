using Ron.Ido.Common.Extensions;
using Ron.Ido.DbStorage.Npgsql;
using Ron.Ido.DbStorage.SqlServer;
using Ron.Ido.EM;

namespace Ron.Ido.DbStorage
{
    [SectionName("DbStorage")]
    public class AppDbContextSettings
    {
        public string ConnectionString { get; set; }

        public ProviderType Provider { get; set; }

        public AppDbContext CreateAppDbContext()
        {
            switch (Provider)
            {
                case ProviderType.PostgreSQL:
                    return new NpgsqlAppDbContext(ConnectionString);
                case ProviderType.SqlServer:
                    return new SqlServerAppDbContext(ConnectionString);
                default:
                    return null;
            }
        }
    }

    public enum ProviderType
    {
        SqlServer,
        PostgreSQL
    }
}
