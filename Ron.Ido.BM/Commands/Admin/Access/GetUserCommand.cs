using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetUserCommand : IRequest<ODataForm<UserDto>>
    {
        public long Id { get; private set; }

        public GetUserCommand(long id)
        {
            Id = id;
        }
        public class GetUserCommandHandler : IRequestHandler<GetUserCommand, ODataForm<UserDto>>
        {
            private ODataService _service;

            public GetUserCommandHandler(ODataService service)
            {
                _service = service;
            }

            public Task<ODataForm<UserDto>> Handle(GetUserCommand request, CancellationToken cancellationToken)
            {
                return Task.Run(() => {
                    var user = _service.GetDto<User, UserDto>(request.Id,
                       new[]
                       {
                           new ODataMapMemberConfig<User, UserDto>(
                               userDto => userDto.Password,
                               expr => expr.Ignore()),
                           new ODataMapMemberConfig<User, UserDto>(
                               userDto => userDto.ConfirmPassword,
                               expr => expr.Ignore()),
                           new ODataMapMemberConfig<User, UserDto>(
                               userDto => userDto.Roles,
                               expr => expr.MapFrom(user => user.UserRoles.Select(ur => ur.RoleId)))
                       });

                    return new ODataForm<UserDto>
                    {
                        Item = user,
                        Options = new Dictionary<string, IEnumerable<ODataOption>>
                        {
                            { nameof(user.Roles).ToCamel(), _service.GetOptions<Role>(nameof(Role.Name), nameof(Role.Id)) },
                        }
                    };
                });
            }
        }

    }
}
