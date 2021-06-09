using MediatR;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Dossier.Apply
{
    public class SaveApplyCommand : IRequest
    {
        public ApplyDto Apply { get; private set; }

        public SaveApplyCommand(ApplyDto dto)
        {
            Apply = dto;
        }
    }
    public class SaveApplyCommandHandler : ApplyHandlerBase, IRequestHandler<SaveApplyCommand>
    {

        public SaveApplyCommandHandler(ODataService service) : base(service)
        {
        }

        public Task<Unit> Handle(SaveApplyCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateApply(request.Apply);
            if (errors.Any())
                throw new ODataValidationException(errors);

            Service.SaveDto(request.Apply,
                new[] {
                    new ODataMapMemberConfig<ApplyDto, EM.Entities.Apply>(
                        apply => apply.CertificateDeliveryForms,
                        expr => expr.Ignore()
                    )
                },
                (dto, apply, context) =>
                {
                });

            return Task.FromResult(Unit.Value);
        }
    }
}
