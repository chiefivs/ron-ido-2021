using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionLicenseConfirmType
    {
        public ConclusionLicenseConfirmType()
        {
            ConclusionLicenseConfirmTypeConclusions = new HashSet<ConclusionLicenseConfirmTypeConclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<ConclusionLicenseConfirmTypeConclusion> ConclusionLicenseConfirmTypeConclusions { get; set; }
    }
}
