using MediatR;
using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Settings
{
	public class GetApplyStatusCommand : IRequest<ODataForm<ApplyStatusDto>>
	{
		public long Id { get; private set; }

		public GetApplyStatusCommand( long id )
		{
			Id = id;
		}
	}

	public class GetApplyStatusCommandHandler : IRequestHandler<GetApplyStatusCommand, ODataForm<ApplyStatusDto>>
	{
		private ODataService _service;

		public GetApplyStatusCommandHandler( ODataService service )
		{
			_service = service;
		}

		public Task<ODataForm<ApplyStatusDto>> Handle( GetApplyStatusCommand request, CancellationToken cancellationToken )
		{
			return Task.Run( () =>
			{
				var status = _service.GetDto( request.Id,
					new[]
					{
							new ODataMapMemberConfig<ApplyStatus, ApplyStatusDto>(
							statusDto => statusDto.AllowStepToStatuses,
							expr => expr.MapFrom(r => r.AllowStepToStatuses.Split(';', StringSplitOptions.RemoveEmptyEntries)))
					} );

				return new ODataForm<ApplyStatusDto>
				{
					Item = status,
					Options = new Dictionary<string, IEnumerable<ODataOption>>
					{
						//{ nameof(role.ApplyStatusPermissions).ToCamel(), PermissionData.List.Select(i => new ODataOption{ Value = i.Id, Text = i.Name, Parent = i.GroupName }) },
						//{ "permissionGroups", PermissionGroup.List.Select(i => new ODataOption { Value = i, Text = i}) },
						//{ "statuses", Enum.GetValues<ApplyStatusEnum>().Select(i => new ODataOption { Value = i, Text = i.ToString()}) }
						{ "statuses", _service.GetOptions<ApplyStatus>(nameof(ApplyStatus.Name), nameof(ApplyStatus.Id)) } // Id?
                    }
				};
			} );
		}
	}
}
