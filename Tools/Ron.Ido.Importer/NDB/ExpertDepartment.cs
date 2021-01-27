using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ExpertDepartment
    {
        public ExpertDepartment()
        {
            InverseParent = new HashSet<ExpertDepartment>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? OrganizationId { get; set; }
        public int? ParentId { get; set; }

        public virtual ExpertOrganization Organization { get; set; }
        public virtual ExpertDepartment Parent { get; set; }
        public virtual ICollection<ExpertDepartment> InverseParent { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
