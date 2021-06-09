﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Dossier;
using Ron.Ido.BM.Commands.Dossier.Apply;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]
    public class DossierController : ControllerBase
    {
        private IMediator _mediator;

        public DossierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/dossier/{id}/get")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<DossierDataDto> GetDossier(long id)
        {
            return await _mediator.Send(new GetDossierCommand(id));
        }

        #region Apply
        [HttpGet]
        [Route("api/dossier/apply/{id}/get")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<ODataForm<ApplyDto>> GetApply(long id)
        {
            return await _mediator.Send(new GetApplyCommand(id));
        }

        [HttpPost]
        [Route("api/dossier/apply/validate")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<Dictionary<string, List<string>>> ValidateApply([FromBody] ApplyDto apply)
        {
            return await _mediator.Send(new ValidateApplyCommand(apply));
        }

        [HttpPost]
        [Route("api/dossier/apply/save")]
        [AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task SaveApply([FromBody] ApplyDto apply)
        {
            await _mediator.Send(new SaveApplyCommand(apply));
        }
        #endregion
    }
}
