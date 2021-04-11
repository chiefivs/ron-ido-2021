using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class ValidateRoleCommand: IRequest<Dictionary<string, List<string>>>
    {
        public RoleDto Role { get; private set; }

        public ValidateRoleCommand(RoleDto role)
        {
            Role = role;
        }
    }

    public class ValidateRoleCommandHandler : RoleHandlerBase, IRequestHandler<ValidateRoleCommand, Dictionary<string, List<string>>>
    {

        public ValidateRoleCommandHandler(ODataService service): base(service)
        {
        }

        public Task<Dictionary<string, List<string>>> Handle(ValidateRoleCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return ValidateRole(request.Role);
            });
        }
    }
}
