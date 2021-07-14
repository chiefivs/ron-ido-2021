using Ron.Ido.Common.DependencyInjection;
using System.Collections.Generic;
using System.Net.Mail;

namespace Ron.Ido.BM.Interfaces
{
	public interface IMailService : IDependency
	{
		MailMessage Send( MailMessage message );

		IEnumerable<MailMessage> Receive();
	}
}
