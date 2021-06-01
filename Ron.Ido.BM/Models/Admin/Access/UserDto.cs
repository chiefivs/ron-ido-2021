using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Models.Admin.Access
{
    public class UserDto : IValidatableObject
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Фамилия длжна быть указана", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string SurName { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Snils { get; set; }

        public string Remark { get; set; }

        public bool IsBlocked { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [Required]
        public IEnumerable<long> Roles { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (!Roles.Any())
            {
                errors.Add(new ValidationResult("Должна быть назначена хотя бы одна роль", new[] { nameof(Roles) }));
            }

            string loginPattern = "^[A-Za-z0-9]{4,10}$";
                        if ((!string.IsNullOrEmpty(Login)) && (!Regex.IsMatch(Login, loginPattern)))
                errors.Add(new ValidationResult("'Логин' может содержать только латинские буквы(маленькие и большие) и цифры, от 4 до 10 символов", new[] { nameof(Password) }));

            if (Id < 1 && string.IsNullOrEmpty(Password))
                errors.Add(new ValidationResult("Для нового пользователя должен быть указан пароль", new[] { nameof(Password) }));

            if (!string.IsNullOrEmpty(Password) && Password != ConfirmPassword)
                errors.Add(new ValidationResult("'Пароль' и 'Подтверждение пароля' не совпадают", new[] { nameof(Password) }));

            string passwordPattern = "^[A-Za-z0-9@_]+$";
            if (!string.IsNullOrEmpty(Password) && !Regex.IsMatch(Password, passwordPattern))
                errors.Add(new ValidationResult("'Пароль' может содержать только латинские буквы(маленькие и большие),цифры и символы @ и _", new[] { nameof(Password) }));

            return errors;
        }
    }
}
