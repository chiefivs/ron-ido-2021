using Ron.Ido.BM.Models.Duplicate;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.BM.Commands.DuplicatesSearch
{
    public abstract class DuplicateHandlerBase
    {
        protected ODataService Service;

        public DuplicateHandlerBase(ODataService service)
        {
            Service = service;
        }

        protected Dictionary<string, List<string>> ValidateDuplicate(DuplicateDto applyDto)
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
