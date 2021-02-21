using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetUsersPageCommand: ODataRequest, IRequest<ODataPage<UserDto>>
    {
    }

    public class GetUsersPageCommandHandler : IRequestHandler<GetUsersPageCommand, ODataPage<UserDto>>
    {
        private ODataService _odataService;

        public GetUsersPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<UserDto>> Handle(GetUsersPageCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _odataService.GetPage(request,
                    new[] {
                        new ODataMapMemberConfig<EM.Entities.User, UserDto>(
                            userDto => userDto.Roles,
                            expr => expr.MapFrom(user => user.UserRoles.Select(ur => ur.Role.Name).ToArray())
                        ),
                        new ODataMapMemberConfig<EM.Entities.User, UserDto>(
                            userDto => userDto.FullName,
                            expr => expr.MapFrom(user => $"{user.SurName} {user.FirstName} {user.LastName}")
                        ),
                    });
            });
        }
    }
}
