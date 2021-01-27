using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardQualification
    {
        public StandardQualification()
        {
            Conclusions = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StandardSpecialityId { get; set; }

        public virtual StandardSpeciality StandardSpeciality { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
    }
}
