using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Blank
    {
        public string Ser { get; set; }
        public string Number { get; set; }
        public string ApplyBarCode { get; set; }
        public string DuplicateBarCode { get; set; }
        public DateTime UsedTime { get; set; }
        public bool IsBroken { get; set; }
        public int? UserId { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual Duplicate DuplicateBarCodeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
