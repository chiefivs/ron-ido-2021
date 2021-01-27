using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.Web.Authorization
{
    public class AuthorizedForAttribute : Attribute, IAuthorizationFilter
    {
        private IEnumerable<PermissionEnum> _permissions;
        public AuthorizedForAttribute(params PermissionEnum[] permission)
        {
            _permissions = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated)  // 
                context.Result = new UnauthorizedResult();
            else if (_permissions != null && _permissions.Any())
            {
                var allowed = context.HttpContext.User.ExtractPermissions();
                var intersect = _permissions.Intersect(allowed);
                if (!intersect.Any())
                    context.Result = new ForbidResult();
            }
        }
    }
}
