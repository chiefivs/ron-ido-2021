using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionOutputBarCode
    {
        public int OutputId { get; set; }
        public string BarCode { get; set; }
        public string Type { get; set; }

        public virtual CorrespondentionOutput Output { get; set; }
    }
}
