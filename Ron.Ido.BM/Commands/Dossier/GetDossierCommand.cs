using MediatR;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Dossier
{
    public class GetDossierCommand: IRequest<DossierDataDto>
    {
        public long Id { get; private set; }

        public GetDossierCommand(long id)
        {
            Id = id;
        }
    }

    public class GetDossierCommandHandler : IRequestHandler<GetDossierCommand, DossierDataDto>
    {
        private DossierService _service;

        public GetDossierCommandHandler(DossierService service)
        {
            _service = service;
        }

        public Task<DossierDataDto> Handle(GetDossierCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() => _service.GetDossierById(cmd.Id));
        }
    }
}
