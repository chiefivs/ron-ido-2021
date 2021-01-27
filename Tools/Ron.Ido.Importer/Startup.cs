using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.DbStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.Importer
{
	public class Startup
	{
		private IConfiguration Configuration { get; }
		private string _nostrificationConn;

		public Startup()
		{
			string basepath = Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath)
				  ?? Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath, EnvironmentVariableTarget.Machine);

			var builder = new ConfigurationBuilder();

			if (!string.IsNullOrEmpty(basepath))
				builder.SetBasePath(basepath);

			_nostrificationConn = File.ReadAllText(Path.Combine(basepath, "nostrification.conf"));
			builder.AddJsonFile("appsettings.json", true);

			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var settings = Configuration.GetSettings<AppDbContextSettings>();

			services.AddSingleton<IConfiguration>(Configuration);
			services.AddAppDbContext(settings);
			services.AddDbContext<NostrificationRONContext>(builder => builder.UseSqlServer(_nostrificationConn));
		}
	}
}
