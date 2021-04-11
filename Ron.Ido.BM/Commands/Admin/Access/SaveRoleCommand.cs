using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class SaveRoleCommand: IRequest
    {
        public RoleDto Role { get; private set; }

        public SaveRoleCommand(RoleDto role)
        {
            Role = role;
        }
    }

    public class SaveRoleCommandHandler : RoleHandlerBase, IRequestHandler<SaveRoleCommand>
    {

        public SaveRoleCommandHandler(ODataService service): base(service)
        {
        }

        public Task<Unit> Handle(SaveRoleCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateRole(request.Role);
            if (errors.Any())
                throw new ODataValidationException(errors);

            Service.SaveDto(request.Role,
                new[] { 
                    new ODataMapMemberConfig<RoleDto, Role>(
                            role => role.RolePermissions,
                            expr => expr.Ignore()
                        )
                },
                (roleDto, role, context) =>
                {
                    role.RolePermissions.Clear();
                    var perms = roleDto.RolePermissions
                    .Cast<int>()
                    .Select(p => new RolePermission { PermissionId = p, Role = role});

                    role.RolePermissions.AddRange(perms);
                });

            return Task.FromResult(Unit.Value);
        }
    }
}
