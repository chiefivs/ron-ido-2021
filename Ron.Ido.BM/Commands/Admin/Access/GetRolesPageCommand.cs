using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetRolesPageCommand : ODataRequest, IRequest<ODataPage<RolesPageItemDto>>
    {
    }

    public class GetRolesPageCommandHandler : IRequestHandler<GetRolesPageCommand, ODataPage<RolesPageItemDto>>
    {
        private ODataService _odataService;

        public GetRolesPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<RolesPageItemDto>> Handle(GetRolesPageCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var result = _odataService.GetPage<Role, RolesPageItemDto>(request);

                return result;
            });
        }
    }
}
