using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Admin.Access;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using System.Threading.Tasks;

namespace Ron.Ido.Web.Controllers
{
    [ApiController]
    public class AdminAccessController : ControllerBase
    {
        private IMediator _mediator;

        public AdminAccessController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("api/admin/access/users/get")]
        public async Task<ODataPage<UserDto>> GetUsers([FromBody]GetUsersPageCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
