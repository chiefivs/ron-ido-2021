using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConfirmedSchoolDoc
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Equivalent { get; set; }
        public bool IsGraduation { get; set; }

        public virtual ConfirmedSchoolName School { get; set; }
    }
}
