using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class School
    {
        public School()
        {
            Applies = new HashSet<Apply>();
            Conclusions = new HashSet<Conclusion>();
            SchoolsSpecialities = new HashSet<SchoolsSpeciality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PostIndex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool CreatedFromApply { get; set; }
        public string CityName { get; set; }
        public string Shingle { get; set; }
        public int? TypeId { get; set; }
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual SchoolType Type { get; set; }
        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
        public virtual ICollection<SchoolsSpeciality> SchoolsSpecialities { get; set; }
    }
}
