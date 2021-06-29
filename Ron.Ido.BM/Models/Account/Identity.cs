using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.BM.Models.Account
{
    public class Identity
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public IEnumerable<PermissionEnum> Permissions { get; set; }

        public Identity()
        {

        }

        public Identity(User user)
        {
            Id = user.Id;
            Login = user.Login;

            var name = user.SurName ?? "";
            if (!string.IsNullOrWhiteSpace(user.FirstName))
                name += $" {user.FirstName.Substring(0, 1)}.";
            if (!string.IsNullOrWhiteSpace(user.LastName))
                name += $" {user.LastName.Substring(0, 1)}.";
            Name = name;

            Permissions = user.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => (PermissionEnum)rp.PermissionId)
                .Distinct();
        }
    }
}
