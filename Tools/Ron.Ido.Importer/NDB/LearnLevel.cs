using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LearnLevel
    {
        public LearnLevel()
        {
            ApplyDocTypes = new HashSet<ApplyDocType>();
            ConclusionLearnLevels = new HashSet<Conclusion>();
            ConclusionStandardLearnLevels = new HashSet<Conclusion>();
            StandardDocTypes = new HashSet<StandardDocType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<ApplyDocType> ApplyDocTypes { get; set; }
        public virtual ICollection<Conclusion> ConclusionLearnLevels { get; set; }
        public virtual ICollection<Conclusion> ConclusionStandardLearnLevels { get; set; }
        public virtual ICollection<StandardDocType> StandardDocTypes { get; set; }
    }
}
