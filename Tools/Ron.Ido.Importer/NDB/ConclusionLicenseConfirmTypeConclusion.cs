using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionLicenseConfirmTypeConclusion
    {
        public int LicenseConfirmTypeId { get; set; }
        public int ConclusionId { get; set; }

        public virtual Conclusion Conclusion { get; set; }
        public virtual ConclusionLicenseConfirmType LicenseConfirmType { get; set; }
    }
}
