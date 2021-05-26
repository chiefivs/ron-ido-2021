using MediatR;
using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Settings
{
	public class SaveApplyStatusCommand : IRequest
	{
		public ApplyStatusDto ApplyStatus { get; private set; }

		public SaveApplyStatusCommand(ApplyStatusDto status)
		{
			ApplyStatus = status;
		}
	}

	public class SaveApplyStatusCommandHandler : ApplyStatusHandlerBase, IRequestHandler<SaveApplyStatusCommand>
	{

		public SaveApplyStatusCommandHandler(ODataService service) : base( service )
		{
		}

		public Task<Unit> Handle( SaveApplyStatusCommand request, CancellationToken cancellationToken )
		{
			var errors = ValidateApplyStatus( request.ApplyStatus );
			if ( errors.Any() )
				throw new ODataValidationException( errors );

			Service.SaveDto( request.ApplyStatus,
				new[] {
					new ODataMapMemberConfig<ApplyStatusDto, ApplyStatus>(
							role => role.AllowStepToStatuses,
							expr => expr.Ignore()
						)
				},
				( statusDto, status, context ) =>
				{
					status.AllowStepToStatuses = string.Join( ";", statusDto.AllowStepToStatuses );
				}

				);

			return Task.FromResult( Unit.Value );
		}
	}
}
