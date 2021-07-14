using System;

namespace Ron.Ido.BM.Scheduler
{
	public abstract class SchedulerJobOptionsBaseAttribute : Attribute, ISchedulerJobOptions
	{
		/// <summary>
		/// Устанавливает параметры запуска задачи
		/// </summary>
		/// <param name="type">Единица измерения интервала времени</param>
		/// <param name="interval">Количество единиц времени между запусками</param>
		/// <param name="starthour">Время запуска, часы. Учитывается начиная с DAY</param>
		/// <param name="startmin">Время запуска, минуты. Учитывается начиная с DAY</param>
		/// <param name="startday">День запуска. Для WEEK - день недели, для MONTH - день месяца</param>
		public SchedulerJobOptionsBaseAttribute( SchedulerIntervalType type, int interval = 1, int starthour = 0, int startmin = 0, int startday = 0 )
		{
			Type = type;
			Interval = interval;
			StartHour = starthour;
			StartMinute = startmin;
			StartDay = startday;
		}

		public SchedulerIntervalType Type { get; private set; }
		public int Interval { get; private set; }
		public int StartHour { get; private set; }
		public int StartMinute { get; private set; }
		public int StartDay { get; private set; }
	}
	public enum SchedulerIntervalType
	{
		/// <summary>
		/// секунда
		/// </summary>
		SECOND,
		/// <summary>
		/// минута
		/// </summary>
		MUNUTE,
		/// <summary>
		/// час
		/// </summary>
		HOUR,
		/// <summary>
		/// день
		/// </summary>
		DAY,
		/// <summary>
		/// неделя
		/// </summary>
		WEEK,
		/// <summary>
		/// месяц
		/// </summary>
		MONTH
	}

	public class SchedulerJobSecondsAttribute : SchedulerJobOptionsBaseAttribute
	{
		public SchedulerJobSecondsAttribute( int interval = 1 ) : base( SchedulerIntervalType.SECOND, interval )
		{
		}
	}

	public class SchedulerJobMinutesAttribute : SchedulerJobOptionsBaseAttribute
	{
		public SchedulerJobMinutesAttribute( int interval = 1 ) : base( SchedulerIntervalType.MUNUTE, interval )
		{
		}
	}

	public class SchedulerJobHoursAttribute : SchedulerJobOptionsBaseAttribute
	{
		public SchedulerJobHoursAttribute( int interval = 1 ) : base( SchedulerIntervalType.HOUR, interval )
		{
		}
	}

	public class SchedulerJobDayAttribute : SchedulerJobOptionsBaseAttribute
	{
		public SchedulerJobDayAttribute( int starthour = 0, int startmin = 0 ) : base( SchedulerIntervalType.DAY, 1, starthour, startmin, 0 )
		{
		}
	}

	public class SchedulerJobWeekAttribute : SchedulerJobOptionsBaseAttribute
	{
		public SchedulerJobWeekAttribute( DayOfWeek startday = DayOfWeek.Monday, int starthour = 0, int startmin = 0 ) : base( SchedulerIntervalType.WEEK, 1, starthour, startmin, (int)startday )
		{
		}
	}

	public class SchedulerJobMonthAttribute : SchedulerJobOptionsBaseAttribute
	{
		public SchedulerJobMonthAttribute( int startday = 0, int starthour = 0, int startmin = 0 ) : base( SchedulerIntervalType.MONTH, 1, starthour, startmin, startday )
		{
		}
	}
}

