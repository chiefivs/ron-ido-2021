using System;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Ron.Ido.DbStorage;
using Ron.Ido.EM;
using Ron.Ido.Common.DependencyInjection;

namespace Ron.Ido.Scheduler
{
	class Program
	{
		static readonly object _locker = new object();
		public static async Task Main( string[] args )
		{
			string basepath = Environment.GetEnvironmentVariable( "CARGO_CONFIGURATIONS" )
				  ?? Environment.GetEnvironmentVariable( "CARGO_CONFIGURATIONS", EnvironmentVariableTarget.Machine );

			var builder = new HostBuilder()
				.ConfigureAppConfiguration( ( hostingContext, config ) =>
				{
					if ( !string.IsNullOrEmpty( basepath ) )
						config.SetBasePath( Path.Combine( basepath, "Scheduler" ) );

					config.AddJsonFile( "appsettings.json", true );
				} )

				.ConfigureServices( ( hostContext, services ) =>
				{
					var section = hostContext.Configuration.GetSection( AppDbContextSettings.SECTION_NAME );
					var settings = section.Get<AppDbContextSettings>();
					services.Add( new ServiceDescriptor(
						typeof( AppDbContext ),
						provider =>
						{
							 //  контекст создаем один на поток
							 var thread = Thread.CurrentThread;
							AppDbContext context;
							lock ( HostedService.Locker ) //HostedService.ContextsByThread )
							 {
								if ( HostedService.ContextsByThread.ContainsKey( thread.ManagedThreadId ) )
								{
									return HostedService.ContextsByThread[thread.ManagedThreadId];
								}

								context = HostedService.ContextsByThread[thread.ManagedThreadId] = settings.CreateAppDbContext();
							}

							 //  сразу открываем транзакцию
							 //  (коммитим или откатываем в случае ошибок, а также освобождаем контекст после завершения задания (ISchedulerJob))
							 context.BeginTransaction();
							return context;
						},
						ServiceLifetime.Transient ) );

					services
						.AddOptions()
						.AddDependencies( typeof( BM.IAssemblyMarker ), typeof( Program ) )
						.AddFileStorage<Attachment>()
						.AddSchedulerJobs()
						.AddMediatR( typeof( BM.IAssemblyMarker ).Assembly )
						.AddApacheMqBroker( typeof( IOneCImportService ) )
						.AddTransient<HttpClient>()
						.AddSingleton<IHostedService, HostedService>();

					var baseType = typeof( ISchedulerJob );
					var types = baseType.Assembly.GetTypes().Where( t => t.IsClass && baseType.IsAssignableFrom( t ) );
					foreach ( var type in types )
					{
						services.AddTransient( type, type );
					}
				} )
				.ConfigureLogging( ( hostingContext, logging ) =>
				{
					logging
						.AddElastic( hostingContext.Configuration )
						.AddConsole()
						.SetMinimumLevel( LogLevel.Trace );
				} );

			await builder.RunConsoleAsync();
		}
	}
}
}
