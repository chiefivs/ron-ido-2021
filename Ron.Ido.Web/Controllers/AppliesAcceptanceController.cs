using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Applies.Acceptance;
using Ron.Ido.BM.Models.Applies.Acceptance;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]
    public class AppliesAcceptanceController : ControllerBase
    {
        private IMediator _mediator;

        public AppliesAcceptanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/acceptance/getpage")]
        //[AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<ODataPage<AppliesAcceptancePageItemDto>> GetAppliesPage([FromBody] ODataRequest request)
        {
            return await _mediator.Send(new GetAppliesAcceptancePageCommand(request));
        }
    }
}
