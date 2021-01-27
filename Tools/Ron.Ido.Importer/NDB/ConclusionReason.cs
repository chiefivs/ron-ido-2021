using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionReason
    {
        public ConclusionReason()
        {
            Conclusions = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ResultId { get; set; }

        public virtual ConclusionResult Result { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
    }
}
