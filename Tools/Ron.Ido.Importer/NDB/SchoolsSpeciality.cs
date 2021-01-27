using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class SchoolsSpeciality
    {
        public int SchoolId { get; set; }
        public int SpecialityId { get; set; }
        public int? EquivalentSpecialityId { get; set; }

        public virtual StandardSpeciality EquivalentSpeciality { get; set; }
        public virtual School School { get; set; }
        public virtual Speciality Speciality { get; set; }
    }
}
