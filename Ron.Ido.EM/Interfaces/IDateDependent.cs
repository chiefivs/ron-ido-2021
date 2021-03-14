using System;

namespace Ron.Ido.EM.Interfaces
{
	public interface IDateDependent
	{
		DateTime? BeginDate { get; set; }
		DateTime? EndDate { get; set; }
	}
}
