using MediatR;
using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Settings
{
	public class ValidateApplyStatusCommand : IRequest<Dictionary<string, List<string>>>
	{
		public ApplyStatusDto Status { get; private set; }

		public ValidateApplyStatusCommand(ApplyStatusDto status)
		{
			Status = status;
		}
	}

	public class ValidateApplyStatusCommandHandler : ApplyStatusHandlerBase, IRequestHandler<ValidateApplyStatusCommand, Dictionary<string, List<string>>>
	{

		public ValidateApplyStatusCommandHandler(ODataService service) : base( service )
		{
		}

		public Task<Dictionary<string, List<string>>> Handle(ValidateApplyStatusCommand request, CancellationToken cancellationToken)
		{
			return Task.Run( () =>
			 {
				 return ValidateApplyStatus( request.Status );
			 } );
		}
	}
}
