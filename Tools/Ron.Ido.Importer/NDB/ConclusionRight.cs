using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionRight
    {
        public ConclusionRight()
        {
            ConclusionConclusionRights = new HashSet<ConclusionConclusionRight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<ConclusionConclusionRight> ConclusionConclusionRights { get; set; }
    }
}
