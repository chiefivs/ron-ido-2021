using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class EiisSession
    {
        public EiisSession()
        {
            EiisPackages = new HashSet<EiisPackage>();
        }

        public Guid SessionId { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ICollection<EiisPackage> EiisPackages { get; set; }
    }
}
