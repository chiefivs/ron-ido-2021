using System.Threading;

namespace Ron.Ido.BM.Scheduler
{
	public interface ISchedulerJob
	{
		void Execute( CancellationToken cancellationToken );
	}
}
