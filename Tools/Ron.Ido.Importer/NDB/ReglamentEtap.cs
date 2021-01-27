using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ReglamentEtap
    {
        public ReglamentEtap()
        {
            ApplyStatuses = new HashSet<ApplyStatus>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }
        public bool? Required { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<ApplyStatus> ApplyStatuses { get; set; }
    }
}
