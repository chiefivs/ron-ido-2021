using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LegalActType
    {
        public LegalActType()
        {
            LegalActs = new HashSet<LegalAct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<LegalAct> LegalActs { get; set; }
    }
}
