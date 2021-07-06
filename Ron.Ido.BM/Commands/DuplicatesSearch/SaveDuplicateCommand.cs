using MediatR;
using Ron.Ido.BM.Models.Duplicate;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.DuplicatesSearch
{
    public class SaveDuplicateCommand : IRequest
    {
        public DuplicateDto Duplicate { get; private set; }

        public SaveDuplicateCommand(DuplicateDto dto)
        {
            Duplicate = dto;
        }
    }
    public class SaveDuplicateCommandHandler : DuplicateHandlerBase, IRequestHandler<SaveDuplicateCommand>
    {

        public SaveDuplicateCommandHandler(ODataService service) : base(service)
        {
        }

        public Task<Unit> Handle(SaveDuplicateCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateDuplicate(request.Duplicate);
            if ( errors.Any() )
                throw new ODataValidationException(errors);

            Service.SaveDto(request.Duplicate,
                Array.Empty<ODataMapMemberConfig<DuplicateDto, EM.Entities.Duplicate>>() /*
                new[] {
                    new ODataMapMemberConfig<DuplicateDto, EM.Entities.Duplicate>(
                        apply => apply.CertificateDeliveryForms,
                        expr => expr.Ignore()
                    )
                }*/,
                (dto, apply, context) =>
                {
                });

            return Task.FromResult(Unit.Value);
        }
    }
}
