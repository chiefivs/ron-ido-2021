using MediatR;
using Ron.Ido.EM;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class DeleteRoleCommand: IRequest
    {
        public long Id { get; private set; }

        public DeleteRoleCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private AppDbContext _context;

        public DeleteRoleCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _context.Roles.Find(request.Id);
            _context.Remove(role);
            _context.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
