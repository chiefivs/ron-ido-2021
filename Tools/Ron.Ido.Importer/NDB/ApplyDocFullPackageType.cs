using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyDocFullPackageType
    {
        public ApplyDocFullPackageType()
        {
            Applies = new HashSet<Apply>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Apply> Applies { get; set; }
    }
}
