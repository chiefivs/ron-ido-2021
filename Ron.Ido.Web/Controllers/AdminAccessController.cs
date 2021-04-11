using ForeignDocsRec2020.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Admin.Access;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Collections.Generic;
using System.Security.Authentication;
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

        #region Users
        [HttpPost]
        [Route("api/admin/access/users/getpage")]
        [AuthorizedFor(PermissionEnum.USER_VIEW, PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT, PermissionEnum.USER_DEL)]
        public async Task<ODataPage<UsersPageItemDto>> GetUsersPage([FromBody]GetUsersPageCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("api/admin/access/users/getdictions")]
        [AuthorizedFor(PermissionEnum.USER_VIEW, PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT, PermissionEnum.USER_DEL)]
        public async Task<UsersListDictions> GetUsersDictions()
        {
            return await _mediator.Send(new GetUsersListDictionsCommand());
        }
        #endregion

        #region Roles
        [HttpPost]
        [Route("api/admin/access/roles/getpage")]
        [AuthorizedFor(PermissionEnum.ROLE_VIEW, PermissionEnum.ROLE_CREATE, PermissionEnum.ROLE_EDIT, PermissionEnum.ROLE_DEL)]
        public async Task<ODataPage<RolesPageItemDto>> GetRolesPage([FromBody] GetRolesPageCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("api/admin/access/roles/{id}/get")]
        [AuthorizedFor(PermissionEnum.ROLE_CREATE, PermissionEnum.ROLE_EDIT, PermissionEnum.ROLE_DEL)]
        public async Task<ODataForm<RoleDto>> GetRole(long id)
        {
            return await _mediator.Send(new GetRoleCommand(id));
        }

        [HttpPost]
        [Route("api/admin/access/roles/validate")]
        [AuthorizedFor(PermissionEnum.ROLE_CREATE, PermissionEnum.ROLE_EDIT)]
        public async Task<Dictionary<string, List<string>>> ValidateRole([FromBody] RoleDto role)
        {
            return await _mediator.Send(new ValidateRoleCommand(role));
        }

        [HttpPost]
        [Route("api/admin/access/roles/save")]
        [AuthorizedFor(PermissionEnum.ROLE_CREATE, PermissionEnum.ROLE_EDIT)]
        public async Task SaveRole([FromBody] RoleDto role)
        {
            await _mediator.Send(new SaveRoleCommand(role));
        }

        [HttpDelete]
        [Route("api/admin/access/roles/{id}/delete")]
        [AuthorizedFor(PermissionEnum.ROLE_DEL)]
        public async Task DeleteRole(long id)
        {
            await _mediator.Send(new DeleteRoleCommand(id));
        }
        #endregion
    }
}
