using Ron.Ido.BM.Models.Admin.Settings;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ron.Ido.BM.Commands.Admin.Settings
{
    public abstract class ApplyStatusHandlerBase
    {
        protected ODataService Service;

        public ApplyStatusHandlerBase(ODataService service)
        {
            Service = service;
        }

        protected Dictionary<string, List<string>> ValidateApplyStatus(ApplyStatusDto statusDto)
        {
            return Service.ValidateDto(statusDto, (status, context) =>
           {
               var list = new List<ValidationResult>();
               if ( context.ApplyStatuses.Any(r => r.Name.ToLower() == (status.Name ?? "").ToLower() && r.Id != status.Id) )
                   list.Add(new ValidationResult("Статус с таким названием уже есть", new[] { nameof(status.Name) }));

               return list;
           });
        }
    }
}
