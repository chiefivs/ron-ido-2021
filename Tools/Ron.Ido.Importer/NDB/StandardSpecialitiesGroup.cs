using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardSpecialitiesGroup
    {
        public StandardSpecialitiesGroup()
        {
            StandardSpecialities = new HashSet<StandardSpeciality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<StandardSpeciality> StandardSpecialities { get; set; }
    }
}
