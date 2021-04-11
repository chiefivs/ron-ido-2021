using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public abstract class RoleHandlerBase
    {
        protected ODataService Service;

        public RoleHandlerBase(ODataService service)
        {
            Service = service;
        }

        protected Dictionary<string, List<string>> ValidateRole(RoleDto roleDto)
        {
            return Service.ValidateDto(roleDto, (role, context) => {
                var list = new List<ValidationResult>();
                if (context.Roles.Any(r => r.Name.ToLower() == (role.Name ?? "").ToLower() && r.Id != role.Id))
                    list.Add(new ValidationResult("Роль с таким названием уже есть", new[] { nameof(role.Name) }));

                return list;
            });
        }
    }
}
