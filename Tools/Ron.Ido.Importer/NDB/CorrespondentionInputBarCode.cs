using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionInputBarCode
    {
        public int InputId { get; set; }
        public string BarCode { get; set; }

        public virtual CorrespondentionInput Input { get; set; }
    }
}
