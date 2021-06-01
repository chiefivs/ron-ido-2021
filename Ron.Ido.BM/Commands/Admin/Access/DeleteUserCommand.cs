using MediatR;
using Ron.Ido.EM;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class DeleteUserCommand : IRequest
    {
        public long Id { get; private set; }

        public DeleteUserCommand(long id)
        {
            Id = id;
        }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private AppDbContext _context;

        public DeleteUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Find(request.Id);
            user.IsDeleted = true;
            _context.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }

}
