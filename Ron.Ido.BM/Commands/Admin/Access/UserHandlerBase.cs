using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public abstract class UserHandlerBase
    {
        protected ODataService Service;

        public UserHandlerBase(ODataService service)
        {
            Service = service;
        }

        protected Dictionary<string, List<string>> ValidateUser(UserDto userDto)
        {
            return Service.ValidateDto(userDto, (user, context) =>
            {
                var list = new List<ValidationResult>();
                if (context.Users.Any(r => r.Login.ToLower() == (user.Login ?? "").ToLower() && r.Id != user.Id))
                    list.Add(new ValidationResult("Пользователь с таким логином уже есть", new[] { nameof(user.Login) }));

                return list;
            });
        }
    }
}