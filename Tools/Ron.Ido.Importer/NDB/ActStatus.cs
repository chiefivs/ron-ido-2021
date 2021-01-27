using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ActStatus
    {
        public ActStatus()
        {
            Acts = new HashSet<Act>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Act> Acts { get; set; }
    }
}
