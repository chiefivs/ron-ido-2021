using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Duplicates;
using Ron.Ido.BM.Models.Duplicates;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]

    public class DuplicatesController : ControllerBase
    {
        private IMediator _mediator;

        public DuplicatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/duplicates/getpage")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW)]
        public async Task<ODataPage<DuplicatesPageItemDto>> GetDuplicatesSearchPage([FromBody] ODataRequest request)
        {
            return await _mediator.Send(new GetDuplicatesPageCommand(request));
        }

        [HttpGet]
        [Route("api/duplicates/getdictions")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<DuplicatesDictions> GetDuplicatesSearchDictions()
        {
            return await _mediator.Send(new GetDuplicatesDictionsCommand());
        }
    }
}
