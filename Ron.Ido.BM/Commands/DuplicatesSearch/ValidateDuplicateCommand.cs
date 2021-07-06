using MediatR;
using Ron.Ido.BM.Models.Duplicate;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.DuplicatesSearch
{
    public class ValidateDuplicateCommand : IRequest<Dictionary<string, List<string>>>
    {
        public DuplicateDto Duplicate { get; private set; }

        public ValidateDuplicateCommand(DuplicateDto apply)
        {
            Duplicate = apply;
        }
    }

    public class ValidateDuplicateCommandHandler : DuplicateHandlerBase, IRequestHandler<ValidateDuplicateCommand, Dictionary<string, List<string>>>
    {

        public ValidateDuplicateCommandHandler(ODataService service) : base(service)
        {
        }

        public Task<Dictionary<string, List<string>>> Handle(ValidateDuplicateCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return ValidateDuplicate(cmd.Duplicate);
            });
        }
    }
}
