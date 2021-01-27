using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ExpertOrganization
    {
        public ExpertOrganization()
        {
            ExpertDepartments = new HashSet<ExpertDepartment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ExpertDepartment> ExpertDepartments { get; set; }
    }
}
