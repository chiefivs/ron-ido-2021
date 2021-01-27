using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyError
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ApplyApplyErrorApplyErrorBarCode { get; set; }
        public int FieldId { get; set; }

        public virtual Apply ApplyApplyErrorApplyErrorBarCodeNavigation { get; set; }
        public virtual ApplyField Field { get; set; }
    }
}
