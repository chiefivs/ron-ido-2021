using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class EiisPackagePart
    {
        public Guid PackageId { get; set; }
        public int PartNum { get; set; }
        public string PartText { get; set; }
        public int PartBytesCount { get; set; }

        public virtual EiisPackage Package { get; set; }
    }
}
