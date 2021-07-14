using Ron.Ido.BM.Interfaces;
using Ron.Ido.EM;
using System.Linq;

namespace Ron.Ido.Scheduler
{
	public class SchedulerIdentity : IIdentityService
	{
		public SchedulerIdentity( AppDbContext context )
		{
			var user = context.Users.FirstOrDefault( u => u.Login == "scheduler" ) ?? context.Users.FirstOrDefault( u => u.Login == "onec" ) ?? context.Users.First();
			Identity = new Identity { Login = user.Login, Id = user.Id, Name = user.Name };
		}
		public Identity Identity { get; }
	}
}
