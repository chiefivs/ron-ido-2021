using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LegalActVersion
    {
        public int Id { get; set; }
        public DateTime AcceptDate { get; set; }
        public string Text { get; set; }
        public string Remark { get; set; }
        public int LegalActId { get; set; }

        public virtual LegalAct LegalAct { get; set; }
    }
}
