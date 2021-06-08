using MediatR;
using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Settings
{
    public class GetApplyStatusCommand : IRequest<ODataForm<ApplyStatusDto>>
    {
        public long Id { get; private set; }

        public GetApplyStatusCommand(long id)
        {
            Id = id;
        }
    }

    public class GetApplyStatusCommandHandler : IRequestHandler<GetApplyStatusCommand, ODataForm<ApplyStatusDto>>
    {
        private ApplyStatusService _service;
        private ODataService _oDataService;

        public GetApplyStatusCommandHandler(ODataService dataService, ApplyStatusService service)
        {
            _service = service;
            _oDataService = dataService;
        }

        public Task<ODataForm<ApplyStatusDto>> Handle(GetApplyStatusCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
           {
               var status = _oDataService.GetDto(request.Id,
                   new[]
                   {
                            new ODataMapMemberConfig<ApplyStatus, ApplyStatusDto>(
                            statusDto => statusDto.AllowStepToStatuses,
                            expr => expr.MapFrom(r =>
                            (r.AllowStepToStatuses??"").Trim().Split(';', StringSplitOptions.RemoveEmptyEntries).Select(z=>Convert.ToInt64(z))

                            )),
                            new ODataMapMemberConfig<ApplyStatus, ApplyStatusDto>(
                            statusDto => statusDto.DenyDelete,
                            expr => expr.MapFrom(r => !string.IsNullOrEmpty(r.StatusEnumValue) || _service.DenyDelete(r.Id)))
                           });

               return new ODataForm<ApplyStatusDto>
               {
                   Item = status,
                   Options = new Dictionary<string, IEnumerable<ODataOption>>
                   {
                        { "allowStepToStatuses", _oDataService.GetOptions<ApplyStatus>(nameof(ApplyStatus.Name), nameof(ApplyStatus.Id)).Where(op=>op.Value.ToString() != status.Id.ToString()) } // Id?
                   }
               };
           });
        }
    }
}
