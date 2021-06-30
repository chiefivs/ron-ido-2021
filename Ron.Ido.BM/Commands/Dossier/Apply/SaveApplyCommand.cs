using MediatR;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Dossier.Apply
{
    public class SaveApplyCommand : IRequest<long>
    {
        public ApplyDto Apply { get; private set; }

        public SaveApplyCommand(ApplyDto dto)
        {
            Apply = dto;
        }
    }
    public class SaveApplyCommandHandler : ApplyHandlerBase, IRequestHandler<SaveApplyCommand, long>
    {
        public SaveApplyCommandHandler(ApplyService service) : base(service)
        {
        }

        public Task<long> Handle(SaveApplyCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateApply(request.Apply);
            if (errors.Any())
                throw new ODataValidationException(errors);

            var id = Service.SaveApplyDto(request.Apply);
            return Task.FromResult(id);
        }
    }
}
