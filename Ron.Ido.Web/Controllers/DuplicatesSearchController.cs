using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Duplicates.DuplicatesSearch;
using Ron.Ido.BM.Commands.DuplicatesSearch;
using Ron.Ido.BM.Models.DuplicatesSearch;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]

    public class DuplicatesSearchController : ControllerBase
    {
        private IMediator _mediator;

        public DuplicatesSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/duplicatesSearch/getpage")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW)]
        public async Task<ODataPage<DuplicatesSearchPageItemDto>> GetDuplicatesSearchPage([FromBody] ODataRequest request)
        {
            return await _mediator.Send(new GetDuplicatesSearchPageCommand(request));
        }

        [HttpGet]
        [Route("api/duplicatesSearch/getdictions")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<DuplicatesSearchDictions> GetDuplicatesSearchDictions()
        {
            return await _mediator.Send(new GetDuplicatesSearchDictionsCommand());
        }

    }
}
