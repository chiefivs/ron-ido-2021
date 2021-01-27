using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Qualification
    {
        public Qualification()
        {
            ConclusionQualificationIdos = new HashSet<Conclusion>();
            ConclusionQualifications = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Conclusion> ConclusionQualificationIdos { get; set; }
        public virtual ICollection<Conclusion> ConclusionQualifications { get; set; }
    }
}
