using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConfirmedSchoolOrder
    {
        public ConfirmedSchoolOrder()
        {
            ConfirmedSchoolNames = new HashSet<ConfirmedSchoolName>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? AcceptDate { get; set; }
        public long IdokbRev { get; set; }
        public string Site { get; set; }

        public virtual ICollection<ConfirmedSchoolName> ConfirmedSchoolNames { get; set; }
    }
}
