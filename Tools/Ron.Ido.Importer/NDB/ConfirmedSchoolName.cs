using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConfirmedSchoolName
    {
        public ConfirmedSchoolName()
        {
            ConfirmedSchoolDocs = new HashSet<ConfirmedSchoolDoc>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string NameNative { get; set; }

        public virtual Country Country { get; set; }
        public virtual ConfirmedSchoolOrder Order { get; set; }
        public virtual ICollection<ConfirmedSchoolDoc> ConfirmedSchoolDocs { get; set; }
    }
}
