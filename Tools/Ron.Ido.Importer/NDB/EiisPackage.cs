using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class EiisPackage
    {
        public EiisPackage()
        {
            EiisPackageParts = new HashSet<EiisPackagePart>();
        }

        public Guid SessionId { get; set; }
        public Guid CreatedId { get; set; }
        public Guid? CompletedId { get; set; }
        public string Error { get; set; }

        public virtual EiisSession Session { get; set; }
        public virtual ICollection<EiisPackagePart> EiisPackageParts { get; set; }
    }
}
