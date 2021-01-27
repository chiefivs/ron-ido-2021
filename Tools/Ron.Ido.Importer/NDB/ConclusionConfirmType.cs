using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionConfirmType
    {
        public ConclusionConfirmType()
        {
            Conclusions = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Conclusion> Conclusions { get; set; }
    }
}
