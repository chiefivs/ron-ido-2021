using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.BM.Scheduler;
using System.Linq;

namespace Ron.Ido.BM.Scheduler
{
	public static class SchedulerServiceCollectionExt
	{
		public static IServiceCollection AddSchedulerJobs( this IServiceCollection collection )
		{
			var baseType = typeof( ISchedulerJob );
			var types = baseType.Assembly.GetTypes().Where( t => t.IsClass && baseType.IsAssignableFrom( t ) );
			foreach ( var type in types )
			{
				collection.AddTransient( type, type );
			}

			return collection;
		}
	}
}
