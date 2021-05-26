using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetApplyStatusesPageCommand : ODataRequest, IRequest<ODataPage<ApplyStatusPageItemDto>>
    {
    }

    public class GetApplyStatusesPageCommandHandler : IRequestHandler<GetApplyStatusesPageCommand, ODataPage<ApplyStatusPageItemDto>>
    {
        private ODataService _odataService;

        public GetApplyStatusesPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<ApplyStatusPageItemDto>> Handle(GetApplyStatusesPageCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var result = _odataService.GetPage<ApplyStatus, ApplyStatusPageItemDto>(request);

                return result;
            });
        }
    }
}
