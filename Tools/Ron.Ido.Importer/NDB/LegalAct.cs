using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LegalAct
    {
        public LegalAct()
        {
            LegalActVersions = new HashSet<LegalActVersion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AcceptDate { get; set; }
        public int TypeId { get; set; }

        public virtual LegalActType Type { get; set; }
        public virtual ICollection<LegalActVersion> LegalActVersions { get; set; }
    }
}
