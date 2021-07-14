using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.EM;
using System.Linq;

namespace Ron.Ido.Web.Scheduler
{
	public class SchedulerIdentity : IIdentityService
	{
		public SchedulerIdentity( AppDbContext context )
		{
			var user = context.Users.FirstOrDefault( u => u.Login == "scheduler" ) ??  context.Users.First();
			Identity = new Identity { Login = user.Login, Id = user.Id, Name = user.FullName };
		}
		public Identity Identity { get; }
	}
}
