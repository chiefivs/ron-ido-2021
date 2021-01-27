using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class UserRole
    {
        public int UsersId { get; set; }
        public int RolesId { get; set; }

        public virtual Role Roles { get; set; }
        public virtual User Users { get; set; }
    }
}
