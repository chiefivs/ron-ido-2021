using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ron.Ido.BM.Commands.Dossier.Apply
{
    public abstract class ApplyHandlerBase
    {
        protected ApplyService Service;

        public ApplyHandlerBase(ApplyService service)
        {
            Service = service;
        }

        protected Dictionary<string, List<string>> ValidateApply(ApplyDto applyDto)
        {
            return Service.ValidateDto(applyDto, (apply, context) =>
            {
                var list = new List<ValidationResult>();
                //TODO дополнительные проверки

                foreach(var attach in apply.Attachments)
                {
                    if (!attach.AttachmentTypeId.HasValue)
                        continue;

                    var type = context.ApplyAttachmentTypes.Find(attach.AttachmentTypeId.Value);
                    if (type.Required && !attach.FileInfo.Any())
                        list.Add(new ValidationResult($"{type.Id}:Отсутствует прикрепленный файл", new[] { nameof(apply.Attachments)}));
                }

                return list;
            });
        }
    }
}
