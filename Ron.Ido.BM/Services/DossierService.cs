using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class DossierService: ODataService
    {

        public DossierService(AppDbContext context) : base(context) { }

        public DossierDataDto GetDossierById(long id)
        {
            var result = GetDto(id,
                new[] {
                        new ODataMapMemberConfig<Dossier, DossierDataDto>(
                            dto => dto.Apply,
                            expr => expr.MapFrom(dossier => dossier.ApplyId != null
                            ? new ApplyData {
                                    Id =  dossier.ApplyId.Value,
                                    BarCode = dossier.Apply.BarCode,
                                    CreateTime = dossier.Apply.CreateTime.ToString("dd.MM.yyyy HH:mm")
                                }
                            : null)),
                        new ODataMapMemberConfig<Dossier, DossierDataDto>(
                            dto => dto.Duplicate,
                            expr => expr.MapFrom(dossier => dossier.DuplicateId != null
                            ? new DuplicateData {
                                    Id =  dossier.DuplicateId.Value,
                                    BarCode = dossier.Duplicate.BarCode,
                                    CreateTime = dossier.Duplicate.CreateTime.ToString("dd.MM.yyyy HH:mm")
                                }
                            : null))
                });

            return result;
        }

        public DossierDataDto GetDossierByApplyId(long applyId)
        {
            var dossierId = AppDbContext.Dossiers.FirstOrDefault(d => d.ApplyId == applyId && d.DuplicateId == null)?.Id;
            if (dossierId.HasValue)
                return GetDossierById(dossierId.Value);

            var dossier = new Dossier {
                ApplyId = applyId
            };
            AppDbContext.Dossiers.Add(dossier);
            AppDbContext.SaveChanges();

            return GetDossierById(dossier.Id);
        }

        public DossierDataDto GetDossierByDuplicateId(long duplicateId)
        {
            var dossierId = AppDbContext.Dossiers.FirstOrDefault(d => d.DuplicateId == duplicateId)?.Id;
            if (dossierId.HasValue)
                return GetDossierById(dossierId.Value);

            var dossier = new Dossier
            {
                DuplicateId = duplicateId
            };
            AppDbContext.Dossiers.Add(dossier);
            AppDbContext.SaveChanges();

            return GetDossierById(dossier.Id);
        }

    }
}
