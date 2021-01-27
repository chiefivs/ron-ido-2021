using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class DuplicateStatus
    {
        public DuplicateStatus()
        {
            DuplicateStatusHistoryPrevStatuses = new HashSet<DuplicateStatusHistory>();
            DuplicateStatusHistoryStatuses = new HashSet<DuplicateStatusHistory>();
            Duplicates = new HashSet<Duplicate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<DuplicateStatusHistory> DuplicateStatusHistoryPrevStatuses { get; set; }
        public virtual ICollection<DuplicateStatusHistory> DuplicateStatusHistoryStatuses { get; set; }
        public virtual ICollection<Duplicate> Duplicates { get; set; }
    }
}
