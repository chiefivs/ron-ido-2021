using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

                return list;
            });
        }
    }
}
