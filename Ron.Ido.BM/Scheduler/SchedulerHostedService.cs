using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ron.Ido.Common.Extensions;
using Ron.Ido.Common.Logging;
using Ron.Ido.EM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Scheduler
{

	public sealed class SchedulerHostedService : IHostedService, IDisposable
	{
		private readonly IServiceProvider _provider;
		private readonly ILogger<SchedulerHostedService> _logger;
		private Timer _timer;
		private readonly Dictionary<Type, JobDescriptor> _descriptors;
		private readonly Dictionary<Type, Task> _tasks = new Dictionary<Type, Task>();
		private CancellationTokenSource _cancel = new CancellationTokenSource();

		internal static Dictionary<int, AppDbContext> ContextsByThread = new Dictionary<int, AppDbContext>();

		public SchedulerHostedService( IServiceProvider provider )
		{
			_provider = provider;
			_descriptors = _getTaskTypes().ToDictionary( t => t, t => new JobDescriptor( t ) );
			var loggerFactory = provider.GetService<ILoggerFactory>();
			_logger = loggerFactory?.CreateLogger<SchedulerHostedService>();


			_logger.LogDebug( Guid.NewGuid(), new JobLogItem
			{
				Message = "Scheduler service: jobs created",
				JobType = _descriptors.Select( d => d.Key.FullName ).Join( "; " )
			} );

		}

		internal static readonly object Locker = new object();
		public Task StartAsync( CancellationToken cancellationToken )
		{
			_logger.LogDebug( Guid.NewGuid(), new LogItem { Message = "Scheduler service: start" } );
			_timer = new Timer( obj =>
			 {
				 if ( _cancel.Token.IsCancellationRequested )
				 {
					 return;
				 }

				 foreach ( var desc in _descriptors )
				 {
					 var nextstart = desc.Value.NextStart;
					 if ( nextstart > DateTime.Now )
						 continue;

					 if ( _tasks.ContainsKey( desc.Key ) && !_tasks[desc.Key].IsCompleted )
						 continue;

					 try
					 {
						 _tasks[desc.Key] = Task.Run( () =>
						 {
							 ISchedulerJob job = null;
							 try
							 {
								 job = (ISchedulerJob)_provider.GetService( desc.Key );
								 job.Execute( _cancel.Token );


								 //  после завершения задания необходимо закоммитить и освободить AppDbContext,
								 //  если он создавался для потока
								 if ( ContextsByThread.ContainsKey( Thread.CurrentThread.ManagedThreadId ) )
								 {
									 lock ( Locker ) //ContextsByThread)
									 {
										 if ( ContextsByThread.ContainsKey( Thread.CurrentThread.ManagedThreadId ) )
										 {
											 var context = ContextsByThread[Thread.CurrentThread.ManagedThreadId];
											 context.Commit();
											 context.Database.CloseConnection();

											 ///context.Dispose();
											 ///ContextsByThread.Remove(Thread.CurrentThread.ManagedThreadId);
										 }
									 }
								 }
								 //_logger.LogDebug(
								 //    Guid.NewGuid(),
								 //    new JobLogItem
								 //    {
								 //        Message = "Scheduler service: job successfully done",
								 //        JobType = desc.Key.FullName
								 //    });
							 }
							 catch ( Exception ex )
							 {
								 //  в случае ошибки необходимо откатить и освободить AppDbContext,
								 //  если он создавался для потока
								 if ( ContextsByThread.ContainsKey( Thread.CurrentThread.ManagedThreadId ) )
								 {
									 lock ( Locker )//ContextsByThread)
									 {
										 if ( ContextsByThread.ContainsKey( Thread.CurrentThread.ManagedThreadId ) )
										 {
											 var context = ContextsByThread[Thread.CurrentThread.ManagedThreadId];
											 context.Rollback();
											 context.Database.CloseConnection();
											 ///context.Dispose();
											 ///ContextsByThread.Remove(Thread.CurrentThread.ManagedThreadId);
										 }
									 }
								 }

								 _logger.LogError(
									 Guid.NewGuid(),
									 new JobLogItem
									 {
										 Message = "Scheduler service: job executing error",
										 JobType = desc.Key.FullName
									 },
									 ex );
							 }
							 finally
							 {
								 (job as IDisposable)?.Dispose();
							 }
						 } );

						 desc.Value.LastStart = nextstart;
						 //_logger?.LogDebug(
						 //    Guid.NewGuid(),
						 //    new JobLogItem
						 //    {
						 //        Message = "Scheduler service: job started",
						 //        JobType = desc.Key.FullName
						 //    });
					 }
					 catch ( Exception ex )
					 {
						 _logger.LogError(
							 Guid.NewGuid(),
							 new JobLogItem
							 {
								 Message = "Scheduler service: job executing error",
								 JobType = desc.Key.FullName
							 },
							 ex );
						 continue;
					 }
				 }
			 }, null, 0, 1000 );
			return Task.CompletedTask;
		}

		public Task StopAsync( CancellationToken cancellationToken )
		{
			_logger.LogDebug( Guid.NewGuid(), new LogItem { Message = "Scheduler service: stop" } );
			_timer.Dispose();
			_timer = null;
			try
			{
				_cancel.Cancel();
			}
			catch { }
			Task.WaitAll( _tasks.Select( i => i.Value ).ToArray() );

			return Task.CompletedTask;
		}

		public void Dispose()
		{
		}

		private IEnumerable<Type> _getTaskTypes()
		{
			var baseType = typeof( ISchedulerJob );
			return baseType.Assembly.GetTypes()
				.Where( t => t.IsClass
				 && baseType.IsAssignableFrom( t )
				 && t.GetCustomAttributes( typeof( SchedulerJobOptionsBaseAttribute ), true ).Any() );
		}
	}

	internal class JobDescriptor
	{
		public Type Type { get; set; }
		public DateTime? LastStart { get; set; }

		private ISchedulerJobOptions _options { get; set; }

		public JobDescriptor( Type type, ISchedulerJobOptions options )
		{
			Type = type;
			_options = options;
		}

		public JobDescriptor( Type type )
		{
			Type = type;
			_options = (ISchedulerJobOptions)type.GetCustomAttributes( typeof( SchedulerJobOptionsBaseAttribute ), true ).First();
			_setFirstStartTime( DateTime.Now );
		}

		public DateTime NextStart
		{
			get
			{
				switch ( _options.Type )
				{
					case SchedulerIntervalType.SECOND:
						return LastStart.Value.AddSeconds( _options.Interval );
					case SchedulerIntervalType.MUNUTE:
						return LastStart.Value.AddMinutes( _options.Interval );
					case SchedulerIntervalType.HOUR:
						return LastStart.Value.AddHours( _options.Interval );
					case SchedulerIntervalType.DAY:
						return LastStart.Value.AddDays( _options.Interval );
					case SchedulerIntervalType.WEEK:
						return LastStart.Value.AddDays( _options.Interval * 7 );
					case SchedulerIntervalType.MONTH:
						return LastStart.Value.AddMonths( _options.Interval );
					default:
						return DateTime.MinValue;
				}
			}
		}

		private void _setFirstStartTime( DateTime now )
		{
			if ( LastStart.HasValue )
				return;

			switch ( _options.Type )
			{
				default:
				case SchedulerIntervalType.SECOND:
					LastStart = new DateTime( now.Year, now.Month, now.Day );
					while ( LastStart.Value.AddSeconds( _options.Interval ) < now )
						LastStart = LastStart.Value.AddSeconds( _options.Interval );
					break;
				case SchedulerIntervalType.MUNUTE:
					LastStart = new DateTime( now.Year, now.Month, now.Day );
					while ( LastStart.Value.AddMinutes( _options.Interval ) < now )
						LastStart = LastStart.Value.AddMinutes( _options.Interval );
					break;
				case SchedulerIntervalType.HOUR:
					LastStart = new DateTime( now.Year, now.Month, now.Day );
					while ( LastStart.Value.AddHours( _options.Interval ) < now )
						LastStart = LastStart.Value.AddHours( _options.Interval );
					break;
				case SchedulerIntervalType.DAY:
					LastStart = new DateTime( now.Year, now.Month, now.Day, _options.StartHour, _options.StartMinute, 0 );
					if ( LastStart > now )
						LastStart = LastStart.Value.AddDays( -1 );
					break;
				case SchedulerIntervalType.WEEK:
					LastStart = new DateTime( now.Year, now.Month, now.Day, _options.StartHour, _options.StartMinute, 0 );
					while ( (int)LastStart.Value.DayOfWeek != _options.StartDay )
						LastStart = LastStart.Value.AddDays( -1 );
					if ( LastStart > now )
						LastStart = LastStart.Value.AddDays( -7 );
					break;
				case SchedulerIntervalType.MONTH:
					LastStart = new DateTime( now.Year, now.Month, _options.StartDay, _options.StartHour, _options.StartMinute, 0 );
					if ( LastStart > now )
						LastStart = LastStart.Value.AddMonths( -1 );
					break;
			}
		}
	}
}
