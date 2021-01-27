using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyAdditionalField
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int FieldId { get; set; }
        public string ApplyAdditionalFieldApplyApplyAdditionalFieldBarCode { get; set; }

        public virtual Apply ApplyAdditionalFieldApplyApplyAdditionalFieldBarCodeNavigation { get; set; }
        public virtual ApplyField Field { get; set; }
    }
}
