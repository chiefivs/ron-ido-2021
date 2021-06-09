using MediatR;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Dossier.Apply
{
    public class GetApplyCommand : IRequest<ODataForm<ApplyDto>>
    {
        public long Id { get; private set; }

        public GetApplyCommand(long id)
        {
            Id = id;
        }
    }

    public class GetApplyCommandHandler : IRequestHandler<GetApplyCommand, ODataForm<ApplyDto>>
    {
        private ODataService _odataService;

        public GetApplyCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataForm<ApplyDto>> Handle(GetApplyCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var dto = _odataService.GetDto(cmd.Id,
                    new[] {
                        new ODataMapMemberConfig<EM.Entities.Apply, ApplyDto>(
                            dto => dto.CertificateDeliveryForms,
                            expr => expr.MapFrom(apply => apply.CertificateDeliveryForms.Select(f => f.DeliveryFormId)))
                    });

                var date = dto.Id > 0 ? dto.CreateTime : DateTime.Now;

                var allCountries = _odataService.GetOptions<EM.Entities.Country>("Name", "Id");
                var foreignCountries = _odataService.GetOptions<EM.Entities.Country>("Name", "Id", countries => countries.Where(c => c.A2code != "RU"));
                var genders = new[] { new ODataOption("не определен", 0), new ODataOption("мужской", 1), new ODataOption("женский", 2) };
                var deliveryForms = _odataService.GetOptions<EM.Entities.ApplyDeliveryForm>("Name", "Id");
                var passportTypes = _odataService.GetOptions<EM.Entities.ApplyPassportType>("Name", "Id");

                return new ODataForm<ApplyDto> { 
                    Item = dto,
                    Options = new Dictionary<string, IEnumerable<ODataOption>>
                    {
                        { nameof(ApplyDto.CreatorGender).ToCamel(), genders },
                        { nameof(ApplyDto.CreatorCitizenshipId).ToCamel(), allCountries },
                        { nameof(ApplyDto.CreatorPassportTypeId).ToCamel(), passportTypes },
                        { nameof(ApplyDto.CreatorCountryId).ToCamel(), allCountries },
                        { nameof(ApplyDto.DeliveryFormId).ToCamel(), deliveryForms },
                        { nameof(ApplyDto.CertificateDeliveryForms).ToCamel(), _odataService.GetOptions<EM.Entities.CertificateDeliveryForm>("Name", "Id") },
                        { nameof(ApplyDto.ReturnOriginalsFormId).ToCamel(), deliveryForms },
                        { nameof(ApplyDto.OwnerGender).ToCamel(), genders },
                        { nameof(ApplyDto.OwnerCountryId).ToCamel(), foreignCountries },
                        { nameof(ApplyDto.OwnerCitizenshipId).ToCamel(), foreignCountries },
                        { nameof(ApplyDto.OwnerPassportTypeId).ToCamel(), passportTypes },
                        { nameof(ApplyDto.DocCountryId).ToCamel(), foreignCountries },
                        { nameof(ApplyDto.DocTypeId).ToCamel(), _odataService.GetOptions<EM.Entities.ApplyDocType>("Name", "Id", date) },
                        { nameof(ApplyDto.SchoolCountryId).ToCamel(), foreignCountries },
                        { nameof(ApplyDto.SchoolTypeId).ToCamel(), _odataService.GetOptions<EM.Entities.SchoolType>("Name", "Id") },
                        { nameof(ApplyDto.SpecialLearnFormId).ToCamel(), _odataService.GetOptions<EM.Entities.ApplyLearnForm>("Name", "Id") },
                        { nameof(ApplyDto.AimId).ToCamel(), _odataService.GetOptions<EM.Entities.ApplyAim>("Name", "Id") },
                        { nameof(ApplyDto.EntryFormId).ToCamel(), _odataService.GetOptions<EM.Entities.ApplyEntryForm>("Name", "Id") },
                    }
                };
            });
        }
    }

}
