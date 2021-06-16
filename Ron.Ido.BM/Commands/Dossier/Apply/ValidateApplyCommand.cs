using MediatR;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Dossier.Apply
{
    public class ValidateApplyCommand : IRequest<Dictionary<string, List<string>>>
    {
        public ApplyDto Apply { get; private set; }

        public ValidateApplyCommand(ApplyDto apply)
        {
            Apply = apply;
        }
    }

    public class ValidateApplyCommandHandler : ApplyHandlerBase, IRequestHandler<ValidateApplyCommand, Dictionary<string, List<string>>>
    {

        public ValidateApplyCommandHandler(ODataService service) : base(service)
        {
        }

        public Task<Dictionary<string, List<string>>> Handle(ValidateApplyCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return ValidateApply(cmd.Apply);
            });
        }
    }
}
