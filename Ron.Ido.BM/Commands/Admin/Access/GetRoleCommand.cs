using AutoMapper;
using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class GetRoleCommand: IRequest<ODataForm<RoleDto>>
    {
        public long Id { get; private set; }

        public GetRoleCommand(long id)
        {
            Id = id;
        }
    }

    public class GetRoleCommandHandler : IRequestHandler<GetRoleCommand, ODataForm<RoleDto>>
    {
        private ODataService _service;

        public GetRoleCommandHandler(ODataService service)
        {
            _service = service;
        }

        public Task<ODataForm<RoleDto>> Handle(GetRoleCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                var role = _service.GetDto(request.Id,
                    new[]
                    {
                        new ODataMapMemberConfig<Role, RoleDto>(
                            roleDto => roleDto.RolePermissions,
                            expr => expr.MapFrom(r => r.RolePermissions.Select(rp => (PermissionEnum)rp.PermissionId)))
                        ,new ODataMapMemberConfig<Role, RoleDto>(
                            roleDto => roleDto.ViewStatuses,
                            expr => expr.MapFrom(r => (r.ViewApplyStatusesString ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries)))
                       ,new ODataMapMemberConfig<Role, RoleDto>(
                            roleDto => roleDto.StepStatuses,
                            expr => expr.MapFrom(r => (r.StepApplyStatusesString ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries)))
                    });

                return new ODataForm<RoleDto>
                {
                    Item = role,
                    Options = new Dictionary<string, IEnumerable<ODataOption>>
                    {
                        { nameof(role.RolePermissions).ToCamel(), PermissionData.List.Select(i => new ODataOption{ Value = i.Id, Text = i.Name, Parent = i.GroupName }) },
                        { "permissionGroups", PermissionGroup.List.Select(i => new ODataOption { Value = i, Text = i}) },
						//{ "statuses", Enum.GetValues<ApplyStatusEnum>().Select(i => new ODataOption { Value = i, Text = i.ToString()}) }
						{ "statuses", _service.GetOptions<ApplyStatus>(nameof(ApplyStatus.Name), nameof(ApplyStatus.Id)) } // Id?
                    }
                };
            });
        }
    }
}
