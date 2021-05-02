using Microsoft.EntityFrameworkCore;
using Ron.Ido.EM;
using System;
using System.IO;

namespace Ron.Ido.DbStorage.Npgsql
{
    public class NpgsqlAppDbContext : AppDbContext
    {
        public NpgsqlAppDbContext() : base(_createOptions(_develConn))
        {
        }

        public NpgsqlAppDbContext(string conn) : base(_createOptions(conn))
        {
        }

        private static string _develConn
        {
            get
            {
                var path = Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath) ??
                    Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath, EnvironmentVariableTarget.Machine);
                return File.ReadAllText(Path.Combine(path, "npgsql.conf"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }




        private static DbContextOptions<AppDbContext> _createOptions(string conn)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseLazyLoadingProxies()
                .UseNpgsql(conn);

            return builder.Options;
        }
    }
}
