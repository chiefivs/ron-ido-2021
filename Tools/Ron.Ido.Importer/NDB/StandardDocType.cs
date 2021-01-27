using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardDocType
    {
        public StandardDocType()
        {
            Conclusions = new HashSet<Conclusion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public int LearnLevelId { get; set; }
        public string PublicName { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ShortName { get; set; }

        public virtual LearnLevel LearnLevel { get; set; }
        public virtual ICollection<Conclusion> Conclusions { get; set; }
    }
}
