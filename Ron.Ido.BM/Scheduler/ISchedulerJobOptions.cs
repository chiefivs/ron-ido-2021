namespace Ron.Ido.BM.Scheduler
{
	public interface ISchedulerJobOptions
	{
		SchedulerIntervalType Type { get; }
		int Interval { get; }
		int StartHour { get; }
		int StartMinute { get; }
		int StartDay { get; }
	}
}
