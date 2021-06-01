using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class ValidateUserCommand : IRequest<Dictionary<string, List<string>>>
    {
        public UserDto User { get; private set; }

        public ValidateUserCommand(UserDto user)
        {
            User = user;
        }
    }

    public class ValidateUserCommandHandler : UserHandlerBase, IRequestHandler<ValidateUserCommand, Dictionary<string, List<string>>>
    {

        public ValidateUserCommandHandler(ODataService service) : base(service)
        {
        }

        public Task<Dictionary<string, List<string>>> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return ValidateUser(request.User);
            });
        }
    }

}
