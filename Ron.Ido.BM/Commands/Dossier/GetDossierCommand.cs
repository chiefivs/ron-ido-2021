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
        private ODataService _odataService;

        public GetDossierCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<DossierDataDto> Handle(GetDossierCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                var result = _odataService.GetDto<EM.Entities.Dossier, DossierDataDto>(cmd.Id,
                    new[] { 
                        new ODataMapMemberConfig<EM.Entities.Dossier, DossierDataDto>(
                            dto => dto.Apply,
                            expr => expr.MapFrom(dossier => dossier.ApplyId != null
                            ? new ApplyData {
                                    Id =  dossier.ApplyId.Value,
                                    BarCode = dossier.Apply.BarCode,
                                    CreateTime = dossier.Apply.CreateTime.ToString("dd.MM.yyyy HH:mm")
                                }
                            : null))
                    });

                return result;
            });
        }
    }
}
