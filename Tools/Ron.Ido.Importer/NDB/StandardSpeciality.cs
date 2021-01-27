using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardSpeciality
    {
        public StandardSpeciality()
        {
            Conclusions = new HashSet<Conclusion>();
            SchoolsSpecialities = new HashSet<SchoolsSpeciality>();
            StandardQualifications = new HashSet<StandardQualification>();
            StandardSpecialityStandardLearnPrograms = new HashSet<StandardSpecialityStandardLearnProgram>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int StandardSpecialitiesCategoryId { get; set; }
        public int? GroupId { get; set; }
        public string Ugs { get; set; }

        public virtual StandardSpecialitiesGroup Group { get; set; }
        public virtual StandardSpecialitiesCategory StandardSpecialitiesCategory { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
        public virtual ICollection<SchoolsSpeciality> SchoolsSpecialities { get; set; }
        public virtual ICollection<StandardQualification> StandardQualifications { get; set; }
        public virtual ICollection<StandardSpecialityStandardLearnProgram> StandardSpecialityStandardLearnPrograms { get; set; }
    }
}
