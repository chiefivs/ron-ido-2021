using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetUsersPageCommand: IRequest<ODataPage<UsersPageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetUsersPageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetUsersPageCommandHandler : IRequestHandler<GetUsersPageCommand, ODataPage<UsersPageItemDto>>
    {
        private ODataService _odataService;

        public GetUsersPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<UsersPageItemDto>> Handle(GetUsersPageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                request.ReplaceOrder("FullName", "SurName", "FirstName");
                var result = _odataService.GetPage(request,
                    new[]
                    {
                        request.CreateCustomFilter<EM.Entities.User>(query => {
                            foreach(var filter in request.Filters)
                            {
                                if(filter.Field.ToCamel() == "roles" && filter.Values.Any())
                                {
                                    var roleids = filter.Values.Select(v => v.Parse<long>(0)).ToArray();
                                    query = query.Where(i => i.UserRoles.Select(ur => ur.RoleId).Any(rid => roleids.Contains(rid)));
                                }
                            }

                            return query;
                        })
                    },
                    new[] {
                        //new ODataMapMemberConfig<EM.Entities.User, UsersPageItemDto>(
                        //    userDto => userDto.Roles,
                        //    expr => expr.MapFrom(user => user.UserRoles.Select(ur => ur.Role.Name).ToArray())
                        //),
                        new ODataMapMemberConfig<EM.Entities.User, UsersPageItemDto>(
                            userDto => userDto.FullName,
                            expr => expr.MapFrom(user => $"{user.SurName} {user.FirstName} {user.LastName}")
                        ),
                    });

                return result;
            });
        }
    }
}
