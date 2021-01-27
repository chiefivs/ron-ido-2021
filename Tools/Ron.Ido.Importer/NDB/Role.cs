using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Grants { get; set; }
        public string ViewApplyStatusesString { get; set; }
        public string StepApplyStatusesString { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
