using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Account;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.Web.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace ForeignDocsRec2020.Web.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/account/login")]
        public async Task<Identity> Login(string login, string password)
        {
            var identity = await _mediator.Send(new LoginCommand(login, password));

            if (identity == null)
                throw new AuthenticationException("Неправильный логин или пароль");

            identity.Token = AuthOptions.CreateToken(identity);

            return identity;
        }

        [HttpGet]
        [Route("api/account/getmenu")]
        [AuthorizedFor()]
        public async Task<IEnumerable<MenuItem>> GetMenu()
        {
            var permissions = AuthOptions.ExtractPermissions(HttpContext.User);
            return await _mediator.Send(new GetMenuCommand(MainMenu.Items, permissions));
        }
    }
}
