using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.DbStorage;
using System;
using System.IO;

namespace Ron.Ido.Migrator
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup()
		{
			string basepath = Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath)
				  ?? Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath, EnvironmentVariableTarget.Machine);

			var builder = new ConfigurationBuilder();

			if (!string.IsNullOrEmpty(basepath))
				builder.SetBasePath(basepath);

			builder.AddJsonFile("appsettings.json", true);

			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var settings = Configuration.GetSettings<AppDbContextSettings>();
			services
				.AddAppDbContext(settings);
            //.AddDependencies(typeof(BM.IAssemblyMarker).Assembly)
            //.AddFileStorage<Attachment>();
        }
	}
}
