using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRonStatus
    {
        public ApplyRonStatus()
        {
            ApplyRonStatusHistoryPrevStatuses = new HashSet<ApplyRonStatusHistory>();
            ApplyRonStatusHistoryStatuses = new HashSet<ApplyRonStatusHistory>();
            ApplyRons = new HashSet<ApplyRon>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<ApplyRonStatusHistory> ApplyRonStatusHistoryPrevStatuses { get; set; }
        public virtual ICollection<ApplyRonStatusHistory> ApplyRonStatusHistoryStatuses { get; set; }
        public virtual ICollection<ApplyRon> ApplyRons { get; set; }
    }
}
