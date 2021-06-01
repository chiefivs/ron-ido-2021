using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ron.Ido.BM.Commands.Admin.Access;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM.Enums;
using Ron.Ido.Web.Authorization;
using System.Collections.Generic;
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
        public async Task<ODataPage<UsersPageItemDto>> GetUsersPage([FromBody]ODataRequest request)
        {
            return await _mediator.Send(new GetUsersPageCommand(request));
        }

        [HttpGet]
        [Route("api/admin/access/users/getdictions")]
        [AuthorizedFor(PermissionEnum.USER_VIEW, PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT, PermissionEnum.USER_DEL)]
        public async Task<UsersListDictions> GetUsersDictions()
        {
            return await _mediator.Send(new GetUsersListDictionsCommand());
        }
        [HttpGet]
        [Route("api/admin/access/users/{id}/get")]
        [AuthorizedFor(PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT, PermissionEnum.USER_DEL)]
        public async Task<ODataForm<UserDto>> GetUser(long id)
        {
            return await _mediator.Send(new GetUserCommand(id));
        }

        [HttpPost]
        [Route("api/admin/access/users/validate")]
        [AuthorizedFor(PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT)]
        public async Task<Dictionary<string, List<string>>> ValidateUser([FromBody] UserDto user)
        {
            return await _mediator.Send(new ValidateUserCommand(user));
        }

        [HttpPost]
        [Route("api/admin/access/users/save")]
        [AuthorizedFor(PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT)]
        public async Task SaveUser([FromBody] UserDto user)
        {
            await _mediator.Send(new SaveUserCommand(user));
        }

        [HttpDelete]
        [Route("api/admin/access/users/{id}/delete")]
        [AuthorizedFor(PermissionEnum.USER_DEL)]
        public async Task DeleteUser(long id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
        }

        #endregion


        #region Roles
        [HttpPost]
        [Route("api/admin/access/roles/getpage")]
        [AuthorizedFor(PermissionEnum.ROLE_VIEW, PermissionEnum.ROLE_CREATE, PermissionEnum.ROLE_EDIT, PermissionEnum.ROLE_DEL)]
        public async Task<ODataPage<RolesPageItemDto>> GetRolesPage([FromBody] ODataRequest request)
        {
            return await _mediator.Send(new GetRolesPageCommand(request));
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
