using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetUsersListDictionsCommand: IRequest<UsersListDictions>
    {
    }

    public class GetUsersListDictionsCommandHandler : IRequestHandler<GetUsersListDictionsCommand, UsersListDictions>
    {
        private ODataService _odataService;

        public GetUsersListDictionsCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<UsersListDictions> Handle(GetUsersListDictionsCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var roles = _odataService.GetOptions<Role>("Name", "Id");

                return new UsersListDictions
                {
                    Roles = roles
                };
            });
        }
    }
}
