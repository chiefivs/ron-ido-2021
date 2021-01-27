using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LearnCourse
    {
        public LearnCourse()
        {
            LearnProgramDetails = new HashSet<LearnProgramDetail>();
            StandardLearnProgramDetails = new HashSet<StandardLearnProgramDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LearnProgramDetail> LearnProgramDetails { get; set; }
        public virtual ICollection<StandardLearnProgramDetail> StandardLearnProgramDetails { get; set; }
    }
}
