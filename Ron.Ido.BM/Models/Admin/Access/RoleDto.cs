using Ron.Ido.EM.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ron.Ido.BM.Models.Admin.Access
{
    public class RoleDto: IValidatableObject
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Название роли не может быть пустым", AllowEmptyStrings = false)]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<PermissionEnum> RolePermissions { get; set; } = new PermissionEnum[] { };

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!RolePermissions.Any())
            {
                errors.Add(new ValidationResult("Должно быть выбрано хотя бы одно разрешение", new[] { nameof(RolePermissions) }));
            }
            return errors;
        }
    }
}
