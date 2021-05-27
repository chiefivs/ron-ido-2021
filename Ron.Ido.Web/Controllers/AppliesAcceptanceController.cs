using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
