using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardSpecialitiesCategory
    {
        public StandardSpecialitiesCategory()
        {
            StandardSpecialities = new HashSet<StandardSpeciality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Manual { get; set; }
        public string Code { get; set; }

        public virtual ICollection<StandardSpeciality> StandardSpecialities { get; set; }
    }
}
