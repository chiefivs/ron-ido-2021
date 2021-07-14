using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using FiocoServiceDesk.Data.Extentions;
using OpenPop.Pop3;
using OpenPop.Pop3.Exceptions;
using MailMessage = FiocoServiceDesk.Web.Models.Mail.MailMessage;

namespace Ron.Ido.BM.Scheduler
{
	public class MailService : IMailService
	{
		private readonly SettingsService _settings;

		public MailService( SettingsService settings )
		{
			_settings = settings;
		}

		public IEnumerable<MailMessage> Receive()
		{
			using ( var pop = new Pop3Client() )
			{
				try
				{
					pop.Connect( _settings.Pop3.Host, _settings.Pop3.Port, false );
					pop.Authenticate( _settings.Pop3.Login, _settings.Pop3.Password );
				}
				catch //(PopServerNotFoundException)
				{
					return new MailMessage[] { };
				}

				int cnt = pop.GetMessageCount();
				var result = new List<MailMessage>();
				for ( int i = cnt; i > 0; i-- )
				{
					var msg = pop.GetMessage( i );
					var attachs = msg.FindAllAttachments();
					var bodypart = msg.FindAllTextVersions().FirstOrDefault();

					var message = new MailMessage
					{
						From = msg.Headers.From.Address,
						To = msg.Headers.To.FirstOrDefault()?.Address ?? "",
						Subject = msg.Headers.Subject,
						Body = bodypart?.GetBodyAsText() ?? "",
						MessageId = msg.Headers.MessageId,
						InReplyTo = msg.Headers.InReplyTo.FirstOrDefault(),
						Attachments = attachs.Select( a => new MailMessage.Attachment
						{
							Name = a.FileName,
							ContentType = a.ContentType.MediaType,
							Bytes = a.Body
						} )
					};

					result.Add( message );
				}

				pop.DeleteAllMessages();
				pop.Disconnect();

				return result.ToArray();
			}
		}

		public MailMessage Send( MailMessage message )
		{
			using ( var smtp = new SmtpClient( _settings.Smtp.Host, _settings.Smtp.Port ) )
			{
				string domain = _settings.Smtp.FromAddress.Split( '@' ).Last();
				message.From = _settings.Smtp.FromAddress;
				message.MessageId = $"{Guid.NewGuid()}@{domain}";
				if ( _settings.Smtp.CredentialsRequired )
					smtp.Credentials = new NetworkCredential( _settings.Smtp.Login, _settings.Smtp.Password );

				var toaddresses = message.To.Split( new[] { ';', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries );
				var msg = new System.Net.Mail.MailMessage( message.From, toaddresses.Join( "," ) )
				{
					Subject = message.Subject,
					Body = message.Body,
					IsBodyHtml = true,
					Headers = { { "Message-Id", $"<{message.MessageId}>" } }
				};

				if ( !string.IsNullOrEmpty( message.InReplyTo ) )
					msg.Headers.Add( "In-Reply-To", $"<{message.InReplyTo}>" );

				var streams = new List<Stream>();
				try
				{
					if ( message.Attachments != null )
					{
						foreach ( var attach in message.Attachments )
						{
							var stream = new MemoryStream( attach.Bytes );
							var msgattach = new Attachment( stream, attach.Name, attach.ContentType );
							msg.Attachments.Add( msgattach );
							streams.Add( stream );
						}
					}

					smtp.Send( msg );
				}
				//catch
				//{
				//    throw;
				//}
				finally
				{
					foreach ( var stream in streams )
						stream.Dispose();
				}


				return message;
			}
		}
	}
}