using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForeignDocsRec2020.Web.Controllers
{
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("api/account")]
        public UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                UserName = "test",
                Claims = (from c in User.Claims select new UserClaim { Type = c.Type, Value = c.Value }).ToArray()
            };
        }

        [HttpGet]
        [Route("api/account/getperms/{permissionName}")]
        public string GetUserPermission(string permissionName, int ts)
        {
            return permissionName + ts.ToString();
        }
    }

}
