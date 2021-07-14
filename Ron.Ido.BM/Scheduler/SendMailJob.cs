using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ron.Ido.BM.Scheduler;
using Ron.Ido.Common.Logging;
using Ron.Ido.EM;
using System;
using System.Threading;

namespace Ron.Ido.BM.Scheduler
{
	[SchedulerJobMinutes( 1 )]
	public class SendMailJob : ISchedulerJob
	{
		private AppDbContext _context;
		private ILogger _logger;

		private SmtpSettings _smtpSettings;

		public SendMailJob( AppDbContext context, IConfiguration config, ILoggerFactory factory )
		{
			_context = context;
			_logger = factory?.CreateLogger( "SendMailJob" );

			var section = config.GetSection( SmtpSettings.SectionName );
			_smtpSettings = section.Get<SmtpSettings>();
		}

		public void Execute( CancellationToken cancellationToken )
		{
			if ( _smtpSettings == null )
			{
				_logger.LogWarning( Guid.NewGuid(), new LogItem { Message = "SmtpSettings missing" } );
				return;
			}
			var now = DateTime.Now.AddHours( -_smtpSettings.MailAgeInHours );
			var mailsToSend = _context.Mails
				.Where( m => !m.WasSent && m.CreatedOn >= now && !string.IsNullOrEmpty( m.Email ) )
				.OrderBy( m => m.CreatedOn )
				.Take( 20 )
				.ToList();
			//.Where(m => !string.IsNullOrEmpty(m.Email));

			_logger.LogInformation(
				Guid.NewGuid(),
				new LogItem { Message = $"There are {mailsToSend.Count()} messages to send" }
			);
			ServicePointManager.ServerCertificateValidationCallback =
			delegate (
				object s,
				X509Certificate certificate,
				X509Chain chain,
				SslPolicyErrors sslPolicyErrors
			)
			{
				return true;
			};

			using ( var smtpClient = new SmtpClient() )
			{
				smtpClient.Host = _smtpSettings.Host;
				smtpClient.Port = _smtpSettings.Port;
				smtpClient.EnableSsl = _smtpSettings.SslEnabled;
				smtpClient.Credentials = new NetworkCredential( _smtpSettings.Login, _smtpSettings.Password );
				smtpClient.UseDefaultCredentials = string.IsNullOrEmpty( _smtpSettings.Login );
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

				foreach ( var mail in mailsToSend )
				{
					if ( Send( mail, smtpClient ) )
					{
						mail.WasSent = true;
						mail.WasSentOn = DateTime.Now;
						_context.Mails.Update( mail );
						_context.SaveChanges();
					}
				}
			}
		}

		readonly char[] _spltters = new[] { ';', ' ', ',' };
		private bool Send( Mail mail, SmtpClient smtpClient )
		{
			var currentAddress = "";
			try
			{
				var email = new MailMessage
				{
					From = new MailAddress( _smtpSettings.MailFrom )
				};
				if ( string.IsNullOrEmpty( _smtpSettings.MailTo ) )
					foreach ( var sEmail in mail.Email.Replace( ". ", " " ).Split( _spltters, StringSplitOptions.RemoveEmptyEntries ) )
					{
						if ( string.IsNullOrEmpty( sEmail ) )
							continue;

						currentAddress = sEmail;

						email.To.Add( new MailAddress( sEmail ) );
					}
				else
					email.To.Add( new MailAddress( _smtpSettings.MailTo ) );

				email.Subject = mail.Subject;
				email.Body = mail.Body;
				smtpClient.Send( email );
				return true;
			}
			catch ( Exception ex )
			{
				_logger.LogError( Guid.NewGuid(), new LogItem { Message = $"Exception during sending mail with id {mail.Id} to {currentAddress}, exception message \"{ex.Message}\"" } );
				return false;
			}
		}
	}

	public class SmtpSettings
	{
		public const string SectionName = "SmtpSettings";

		public string MailFrom { get; set; }
		public string MailTo { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public bool SslEnabled { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int MailAgeInHours { get; set; }
	}
}
