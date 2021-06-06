using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Dossier;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
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
        [Route("api/dossier/get")]
        //[AuthorizedFor(PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL)]
        public async Task<DossierDataDto> GetDossier(long id)
        {
            return await _mediator.Send(new GetDossierCommand(id));
        }
    }
}
