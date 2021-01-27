using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class AgreementVersion
    {
        public int Id { get; set; }
        public DateTime? ValidDate { get; set; }
        public string Text { get; set; }
        public string Remark { get; set; }
        public int AgreementId { get; set; }

        public virtual Agreement Agreement { get; set; }
    }
}
