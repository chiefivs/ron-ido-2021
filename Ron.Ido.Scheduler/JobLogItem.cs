using Cargo.Logging.Common;
using System;

namespace Ron.Ido.Scheduler
{
	public class JobLogItem : LogItem
	{
		public string JobType { get; set; }
		public DateTime LocalTime { get; set; }

		public JobLogItem()
		{
			LocalTime = DateTime.Now;
		}
	}
}
