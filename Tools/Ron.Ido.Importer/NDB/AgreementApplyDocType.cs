using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class AgreementApplyDocType
    {
        public int AgreementsId { get; set; }
        public int DocTypesId { get; set; }

        public virtual Agreement Agreements { get; set; }
        public virtual ApplyDocType DocTypes { get; set; }
    }
}
