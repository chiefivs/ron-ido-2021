using Ron.Ido.Common.Logging;
using System;

namespace Ron.Ido.Web.Scheduler
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
