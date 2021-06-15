using Microsoft.AspNetCore.Http;
using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.Web.Authorization;

namespace Ron.Ido.Web.Services
{
    public class IdentityService : IIdentityService
    {
        public IdentityService( IHttpContextAccessor accessor )
        {
            var principal = accessor.HttpContext?.User;
            if ( principal == null )
                Identity = null;

            Identity = principal == null ? null : new Identity
            {
                Id = principal.ExtractUserId() ?? 0,
                Login = principal.Identity?.Name ?? "system",
                Permissions = principal.ExtractPermissions()
            };
        }

        public Identity Identity { get; private set; }
    }
}
