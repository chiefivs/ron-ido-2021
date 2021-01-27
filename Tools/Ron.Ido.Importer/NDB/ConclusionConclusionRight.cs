using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionConclusionRight
    {
        public int ConclusionConclusionRightConclusionRightId { get; set; }
        public int RightsId { get; set; }

        public virtual Conclusion ConclusionConclusionRightConclusionRight { get; set; }
        public virtual ConclusionRight Rights { get; set; }
    }
}
