using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Speciality
    {
        public Speciality()
        {
            Applies = new HashSet<Apply>();
            Conclusions = new HashSet<Conclusion>();
            SchoolsSpecialities = new HashSet<SchoolsSpeciality>();
            SpecialitiesLearnPrograms = new HashSet<SpecialitiesLearnProgram>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CreatedFromApply { get; set; }
        public int? CheckId { get; set; }
        public decimal? Conformity { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
        public virtual ICollection<SchoolsSpeciality> SchoolsSpecialities { get; set; }
        public virtual ICollection<SpecialitiesLearnProgram> SpecialitiesLearnPrograms { get; set; }
    }
}
