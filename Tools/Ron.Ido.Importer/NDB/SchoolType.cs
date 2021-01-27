using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class SchoolType
    {
        public SchoolType()
        {
            Applies = new HashSet<Apply>();
            Schools = new HashSet<School>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
