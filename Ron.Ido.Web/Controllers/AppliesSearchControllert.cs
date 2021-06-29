using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Applies.Acceptance;
using Ron.Ido.BM.Commands.Applies.AppliesSearch;
using Ron.Ido.BM.Models.Applies.AppliesSearch;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]

    public class AppliesSearchControllert : ControllerBase
    {
        private IMediator _mediator;

        public AppliesSearchControllert(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/appliesSearch/getpage")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<ODataPage<AppliesSearchPageItemDto>> GetAppliesSearchPage([FromBody] ODataRequest request)
        {
            return await _mediator.Send(new GetAppliesSearchPageCommand(request));
        }

        [HttpGet]
        [Route("api/appliesSearch/getdictions")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<AppliesSearchDictions> GetAppliesSearchDictions()
        {
            return await _mediator.Send(new GetAppliesSearchDictinsCommand());
        }

    }
}
