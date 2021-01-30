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
    //[Authorize]
    //[Route("api/account")]
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

        //[HttpGet]
        //[Route("api/account/getPerms/{permissionName}")]
        //public string GetUserPermission(string permissionName, int ts)
        //{
        //    return permissionName + ts.ToString();
        //}

        //[HttpGet]
        //[Route("test")]
        //public ListPage<string,int> GetTest(int? val)
        //{
        //    return new ListPage<string,int>();
        //}


        //[HttpPost]
        //[Route("api/account/getPage")]
        //public ListPage<string, int> GetPage([FromBody]ListPageRequest input)
        //{
        //    return new ListPage<string, int>()
        //    {
        //        Id = 123,
        //        Items = new[] { "one", "two" },
        //        Position = 4,
        //        Total = 200
        //    };
        //}

        //[HttpPost]
        //[Route("api/account/getPage1")]
        //public async Task<ListPage<string, int>> GetPageAsync([FromBody] ListPageRequest input)
        //{
        //    return new ListPage<string, int>()
        //    {
        //        Id = 12345,
        //        Items = new[] { "one", "two", "three" },
        //        Position = 4,
        //        Total = 100

        //    };
        //}

        //[HttpGet]
        //[Route("api/account/setParam")]
        //public void SetParam(string name, string value)
        //{

        //}

        //[HttpGet]
        //[Route("api/account/setParam1")]
        //public async Task SetParamAsync(string name, string value)
        //{

        //}
    }

    public class ListPage<T1,T2>
    {
        public int Total { get; set; }
        public int Position { get; set; }
        public IEnumerable<T1> Items { get; set; }
        public T2 Id { get; set; }
    }

    public class ListPageRequest
    {
        public string Filter { get; set; }
        public int Position { get; set; }
    }
}
