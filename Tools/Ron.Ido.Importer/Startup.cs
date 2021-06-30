using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.DbStorage;
using Ron.Ido.FileStorage;
using Ron.Ido.Importer.NDB.Classes;
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
		private string _nostrificationStorage;

		public Startup()
		{
			string basepath = Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath)
				  ?? Environment.GetEnvironmentVariable(Common.Constants.ConfigFolderPath, EnvironmentVariableTarget.Machine);

            Console.WriteLine("Base config path {0}", basepath);
            var builder = new ConfigurationBuilder();


			if (!string.IsNullOrEmpty(basepath))
				builder.SetBasePath(basepath);

			var settings = File.ReadAllLines(Path.Combine(basepath, "nostrification.conf"));
			_nostrificationConn = settings[0];
			_nostrificationStorage = settings[1];
			builder.AddJsonFile("appsettings.json", true);

			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var settings = Configuration.GetSettings<AppDbContextSettings>();

			services.AddSingleton<IConfiguration>(Configuration);
			services.AddAppDbContext(settings);
			services.AddFileStorage<EM.Entities.FileInfo>();
			services.AddDbContext<NostrificationRONContext>(builder => builder.UseSqlServer(_nostrificationConn));
			services.Add(
				new ServiceDescriptor(typeof(NostrificationStorage),
				provider => new NostrificationStorage(_nostrificationStorage),
				ServiceLifetime.Singleton));
		}
	}
}
