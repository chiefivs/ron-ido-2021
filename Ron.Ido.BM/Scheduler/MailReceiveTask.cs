using System;
using System.Linq;

namespace Ron.Ido.BM.Scheduler
{
	public class MailReceiveTask : ISchedulerJob
	{
		private readonly IRepository _rep;
		private readonly IEventManager _manager;
		private readonly IMailService _mail;
		private readonly IFileStorageService _storage;

		public MailReceiveTask(
			IRepository rep,
			IEventManager manager,
			IMailService mail,
			IFileStorageService storage )
		{
			_rep = rep;
			_manager = manager;
			_mail = mail;
			_storage = storage;
		}

		public void Execute()
		{
			var messages = _mail.Receive();
			foreach ( var mailmsg in messages )
			{
				var inreplyto = mailmsg.InReplyTo;
				if ( string.IsNullOrEmpty( inreplyto ) )
					continue;

				var applies = _rep.Set<Apply>().Where( a => a.Messages.Any( m => m.MessageId == inreplyto ) );
				if ( !applies.Any() )
					continue;

				var message = new Message
				{
					Email = mailmsg.From,
					Time = DateTime.Now,
					MessageId = mailmsg.MessageId,
					InReplyTo = inreplyto,
					Subject = mailmsg.Subject.MaxLen( 200 ),
					Body = mailmsg.Body.MaxLen( 4000 )
				};

				foreach ( var file in mailmsg.Attachments ?? new MailMessage.Attachment[] { } )
				{
					var uid = _storage.SaveFile( file.Bytes );
					var attachment = new Attachment
					{
						Uid = uid,
						Name = file.Name,
						ContentType = file.ContentType,
						Size = file.Bytes.Length
					};

					_rep.Add( attachment );
					message.Attachments.Add( attachment );
				}

				_rep.Add( message );
				_rep.SaveChanges();

				foreach ( var apply in applies )
				{
					apply.Messages.Add( message );
					_rep.SaveChanges();

					var origmsg = apply.Messages.FirstOrDefault( m => m.MessageId == inreplyto );

					if ( apply.StatusId == (int)ApplyStatusEnum.WAITING && !(origmsg?.NoWaiting ?? true) )
					{
						apply.StatusId = (int)ApplyStatusEnum.THREATMENT;
						_rep.SaveChanges();
						_manager.Invoke<IApplyEvent>( e => e.StatusChanged( apply ) );
					}
				}



			}
		}
	}
}