using Microsoft.Extensions.Logging;
using Ron.Ido.BM.Scheduler;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.EM;
using System.Threading;

namespace Ron.Ido.BM.Scheduler
{

	//  примеры
	//[SchedulerJobMinutes(2)]
	public class MinuteJob : ISchedulerJob
	{
		private ILogger _logger;
		public void Execute( CancellationToken cancellationToken )
		{
			_logger.LogDebug( "Успешное выполнение MinuteJob" );
			//throw new Exception("test exception");
		}

		public MinuteJob( AppDbContext context, MinuteService service, ILoggerFactory factory )
		{
			_logger = factory.CreateLogger( "test MinuteJob" );
		}
	}

	public class MinuteService : IDependency
	{
		public MinuteService( AppDbContext context )
		{

		}
	}
}
