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
    public class GetApplyStatusesPageCommand : IRequest<ODataPage<ApplyStatusPageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetApplyStatusesPageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetApplyStatusesPageCommandHandler : IRequestHandler<GetApplyStatusesPageCommand, ODataPage<ApplyStatusPageItemDto>>
    {
        private ODataService _odataService;

        public GetApplyStatusesPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<ApplyStatusPageItemDto>> Handle(GetApplyStatusesPageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var result = _odataService.GetPage<ApplyStatus, ApplyStatusPageItemDto>(cmd.Request);

                return result;
            });
        }
    }
}
