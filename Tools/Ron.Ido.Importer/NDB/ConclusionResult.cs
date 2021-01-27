using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionResult
    {
        public ConclusionResult()
        {
            Applies = new HashSet<Apply>();
            ConclusionReasons = new HashSet<ConclusionReason>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<ConclusionReason> ConclusionReasons { get; set; }
    }
}
