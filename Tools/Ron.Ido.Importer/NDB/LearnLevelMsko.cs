using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LearnLevelMsko
    {
        public LearnLevelMsko()
        {
            Conclusions = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderNum { get; set; }
        public int? Type { get; set; }
        public string Code1997 { get; set; }
        public string Name1997 { get; set; }
        public string Code2011 { get; set; }
        public string Name2011 { get; set; }

        public virtual ICollection<Conclusion> Conclusions { get; set; }
    }
}
