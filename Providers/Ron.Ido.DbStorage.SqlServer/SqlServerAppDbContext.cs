using Microsoft.EntityFrameworkCore;
using Ron.Ido.EM;
using System;
using System.IO;

namespace Ron.Ido.DbStorage.SqlServer
{
    public class SqlServerAppDbContext : AppDbContext
    {
        public SqlServerAppDbContext() : base(_createOptions(_develConn))
        {
        }

        public SqlServerAppDbContext(string conn) : base(_createOptions(conn))
        {
        }

        private static string _develConn
        {
            get
            {
                var path = Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath) ??
                    Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath, EnvironmentVariableTarget.Machine);
                return File.ReadAllText(Path.Combine(path, "sqlserver.conf"));
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
                .UseSqlServer(conn, opt => { /*opt.UseRowNumberForPaging();*/ });

            return builder.Options;
        }
    }
}
