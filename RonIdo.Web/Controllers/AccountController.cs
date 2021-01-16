using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForeignDocsRec2020.Web.Controllers
{
    [Route("api/account")]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = new
            {
                User = "test",
                Claims = from c in User.Claims select new { c.Type, c.Value }
            };
            return new JsonResult(result);
        }
    }
}
