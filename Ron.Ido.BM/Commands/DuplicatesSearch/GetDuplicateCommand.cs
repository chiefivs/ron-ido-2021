using MediatR;
using Microsoft.EntityFrameworkCore;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Duplicate;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E = Ron.Ido.EM.Entities;

namespace Ron.Ido.BM.Commands.DuplicatesSearch
{
    public class GetDuplicateCommand : IRequest<ODataForm<DuplicateDto>>
    {
        public long Id { get; private set; }

        public GetDuplicateCommand(long id)
        {
            Id = id;
        }
    }

    public class GetDuplicateCommandHandler : IRequestHandler<GetDuplicateCommand, ODataForm<DuplicateDto>>
    {
        private ODataService _odataService;

        public GetDuplicateCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataForm<DuplicateDto>> Handle(GetDuplicateCommand cmd, CancellationToken cancellationToken)
        {

            return Task.Run(() =>
            {
                var dto = _odataService.GetDto(cmd.Id, 
                    new[] {
                        new ODataMapMemberConfig<EM.Entities.Duplicate, DuplicateDto>(
                            dto => dto.CreateTime,
                            expr => expr.MapFrom(dup => dup.CreateTime.FormatDate())
                            )
                            ,
                        new ODataMapMemberConfig<EM.Entities.Duplicate, DuplicateDto>(
                            dto => dto.HandoutTime,
                            expr => expr.MapFrom(dup => dup.HandoutTime.FormatDate())
                            )
                    });


                var docTypes = _odataService.GetOptions<EM.Entities.ApplyDocType>("Name", "Id");
                var deliveryForm = _odataService.GetOptions<EM.Entities.ApplyDeliveryForm>("Name", "Id");
                var allCountries = _odataService.GetOptions<EM.Entities.Country>("Name", "Id");
                var foreignCountries = _odataService.GetOptions<EM.Entities.Country>("Name", "Id", countries => countries.Where(c => c.A2code != "RU"));
                var status = _odataService.GetOptions<EM.Entities.DuplicateStatus>("Name", "Id");

                return new ODataForm<DuplicateDto>
                {
                    Item = dto,
                    Options = new Dictionary<string, IEnumerable<ODataOption>>
                    {
                        { nameof(DuplicateDto.DocumentTypeId).ToCamel(), docTypes },
                        { nameof(DuplicateDto.CreatorCountryId).ToCamel(), allCountries },
                        { nameof(DuplicateDto.DocCountryId).ToCamel(), foreignCountries },
                        { nameof(DuplicateDto.ReturnOriginalsFormId).ToCamel(), deliveryForm },
                        { nameof(DuplicateDto.StatusId).ToCamel(), status },
                        /*
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
                        { nameof(ApplyDto.EntryFormId).ToCamel(), _odataService.GetOptions<EM.Entities.ApplyEntryForm>("Name", "Id") },*/
                    }
                };
            });
        }

    }

}
