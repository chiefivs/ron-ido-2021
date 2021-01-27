using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRonAppeal
    {
        public int Id { get; set; }
        public string ApplyRonBarCode { get; set; }
        public string RegNum { get; set; }
        public DateTime? RegDate { get; set; }

        public virtual ApplyRon ApplyRonBarCodeNavigation { get; set; }
    }
}
