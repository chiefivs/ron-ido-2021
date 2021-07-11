using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ron.Ido.BM.Commands.Dossier.Apply
{
    public abstract class ApplyHandlerBase
    {
        protected ApplyService Service;

        private const string _requiredMessage = "Поле обязательно к заполнению";

        public ApplyHandlerBase(ApplyService service)
        {
            Service = service;
        }

        protected Dictionary<string, List<string>> ValidateApply(ApplyDto applyDto)
        {
            return Service.ValidateDto(applyDto, (apply, context) =>
            {
                var list = new List<ValidationResult>();

                //  ФИО заявителя
                if (string.IsNullOrWhiteSpace(apply.CreatorSurname) && !apply.IsCreatorSurnameAbsent)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorSurname) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorFirstName) && !apply.IsCreatorFirstNameAbsent)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorFirstName) }));

                //if (string.IsNullOrWhiteSpace(apply.CreatorLastName) && !apply.IsCreatorLastNameAbsent)
                //    list.Add(new ValidationResult("Поле обязательно к заполнению", new[] { nameof(apply.CreatorLastName) }));

                if(!apply.CreatorCitizenshipId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorCitizenshipId) }));

                if (!apply.CreatorPassportTypeId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorPassportTypeId) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorPassportReq))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorPassportReq) }));

                if (!apply.CreatorCountryId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorCountryId) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorMailIndex))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorMailIndex) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorCityName))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorCityName) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorStreet))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorStreet) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorBlock))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorBlock) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorPhone))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorPhone) }));

                if (string.IsNullOrWhiteSpace(apply.CreatorEmail))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.CreatorEmail) }));
                else if(!Regex.IsMatch(apply.CreatorEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    list.Add(new ValidationResult("Поле не соответствует формату E-mail", new[] { nameof(apply.CreatorEmail) }));

                if(apply.EntryFormId.HasValue 
                    && (new [] { (long)ApplyEntryFormEnum.CABINET, (long)ApplyEntryFormEnum.EPGU }).Contains(apply.EntryFormId.Value))
                {
                    if (!apply.DeliveryFormId.HasValue)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.DeliveryFormId) }));
                }

                if(apply.DeliveryFormId == (long)ApplyDeliveryFormEnum.POST && string.IsNullOrWhiteSpace(apply.ReturnOriginalsPostAddress))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.ReturnOriginalsPostAddress) }));
                //TODO: Необходимо предусмотреть переключатель «Адрес доставки совпадает адресом получения», если указывается «нет», то появляется поле для ввода нового адреса

                if(!apply.CertificateDeliveryForms.Any())
                    list.Add(new ValidationResult("Указать хотя бы один способ получения", new[] { nameof(apply.CertificateDeliveryForms) }));

                if (apply.ByWarrant)
                {
                    if (string.IsNullOrWhiteSpace(apply.WarrantReq))
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.WarrantReq) }));

                    if (!apply.WarrantDate.HasValue)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.WarrantDate) }));

                    if (!apply.WarrantTerm.HasValue)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.WarrantTerm) }));

                    //  ФИО обладателя
                    if (string.IsNullOrWhiteSpace(apply.OwnerSurname) && !apply.IsOwnerSurnameAbsent)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerSurname) }));

                    if (string.IsNullOrWhiteSpace(apply.OwnerFirstName) && !apply.IsOwnerFirstNameAbsent)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerFirstName) }));

                    //if (string.IsNullOrWhiteSpace(apply.OwnerLastName) && !apply.IsOwnerLastNameAbsent)
                    //    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerLastName) }));

                    if (!apply.OwnerBirthDate.HasValue)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerFirstName) }));

                    if(!apply.OwnerCountryId.HasValue)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerCountryId) }));

                    if (!apply.OwnerPassportTypeId.HasValue)
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerPassportTypeId) }));

                    if (string.IsNullOrWhiteSpace(apply.OwnerPassportReq))
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerPassportReq) }));

                    if (string.IsNullOrWhiteSpace(apply.OwnerPhone))
                        list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.OwnerPhone) }));
                }

                if(!apply.SchoolCountryId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.SchoolCountryId) }));

                if(!apply.DocTypeId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.DocTypeId) }));

                if (string.IsNullOrWhiteSpace(apply.DocDescription))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.DocDescription) }));

                if (string.IsNullOrWhiteSpace(apply.DocBlankNum))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.DocBlankNum) }));

                if (string.IsNullOrWhiteSpace(apply.DocDate))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.DocDate) }));

                if (string.IsNullOrWhiteSpace(apply.DocFullName))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.DocFullName) }));

                if (string.IsNullOrWhiteSpace(apply.SchoolName))
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.SchoolName) }));

                if(!apply.SpecialLearnFormId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.SpecialLearnFormId) }));

                if(!apply.AimId.HasValue)
                    list.Add(new ValidationResult(_requiredMessage, new[] { nameof(apply.AimId) }));

                //  приложенные документы
                var n = -1;
                foreach(var attach in apply.Attachments)
                {
                    n++;
                    if (attach.AttachmentTypeId.HasValue)
                    { 
                        var type = context.ApplyAttachmentTypes.Find(attach.AttachmentTypeId.Value);
                        if (type.Required && !attach.FileInfo.Any())
                            list.Add(new ValidationResult($"{n}:Отсутствует прикрепленный файл", new[] { nameof(apply.Attachments)}));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(attach.Description))
                            list.Add(new ValidationResult($"{n}:Отсутствует описание дополнительного документа", new[] { nameof(apply.Attachments) }));
                    }
                }

                return list;
            });
        }
    }
}
