using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Admin.Access;
using Ron.Ido.BM.Commands.Admin.Settings;
using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]
    public class AdminSettingsController : ControllerBase
    {
        private IMediator _mediator;

        public AdminSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region ApplyStatus
        [HttpPost]
        [Route("api/admin/settings/status/getpage")]
        [AuthorizedFor(PermissionEnum.SETTINGS)]
        public async Task<ODataPage<ApplyStatusPageItemDto>> GetApplyStatusesPage([FromBody] GetApplyStatusesPageCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("api/admin/settings/status/{id}/get")]
        [AuthorizedFor(PermissionEnum.SETTINGS)]
        public async Task<ODataForm<ApplyStatusDto>> GetStatus(long id)
        {
            return await _mediator.Send(new GetApplyStatusCommand(id));
        }

        [HttpPost]
        [Route("api/admin/settings/status/validate")]
        [AuthorizedFor(PermissionEnum.SETTINGS)]
        public async Task<Dictionary<string, List<string>>> ValidateApplyStatus([FromBody] ApplyStatusDto status)
        {
            return await _mediator.Send(new ValidateApplyStatusCommand(status));
        }

        [HttpPost]
        [Route("api/admin/settings/status/save")]
        [AuthorizedFor(PermissionEnum.SETTINGS)]
        public async Task SaveStatus([FromBody] ApplyStatusDto status)
        {
            await _mediator.Send(new SaveApplyStatusCommand(status));
        }

        [HttpDelete]
        [Route("api/admin/settings/status/{id}/delete")]
        [AuthorizedFor(PermissionEnum.SETTINGS)]
        public async Task DeleteStatus(long id)
        {
            await _mediator.Send(new DeleteApplyStatusCommand(id));
        }
        #endregion
    } 
}
