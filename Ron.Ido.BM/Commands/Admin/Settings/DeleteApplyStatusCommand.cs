using MediatR;
using Ron.Ido.EM;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Settings
{
	public class DeleteApplyStatusCommand : IRequest
	{
		public long Id { get; private set; }

		public DeleteApplyStatusCommand( long id )
		{
			Id = id;
		}
	}

	public class DeleteApplyStatusCommandHandler : IRequestHandler<DeleteApplyStatusCommand>
	{
		private AppDbContext _context;

		public DeleteApplyStatusCommandHandler( AppDbContext context )
		{
			_context = context;
		}

		public Task<Unit> Handle( DeleteApplyStatusCommand request, CancellationToken cancellationToken )
		{
			var status = _context.ApplyStatuses.Find( request.Id );
			_context.Remove( status );
			_context.SaveChanges();

			return Task.FromResult( Unit.Value );
		}
	}
}
