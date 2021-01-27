using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Ratification
    {
        public int Id { get; set; }
        public DateTime AcceptDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public int CountryId { get; set; }
        public int AgreementId { get; set; }

        public virtual Agreement Agreement { get; set; }
        public virtual Country Country { get; set; }
    }
}
