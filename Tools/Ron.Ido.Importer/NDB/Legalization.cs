using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Legalization
    {
        public Legalization()
        {
            Conclusions = new HashSet<Conclusion>();
            Countries = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Conclusion> Conclusions { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}
