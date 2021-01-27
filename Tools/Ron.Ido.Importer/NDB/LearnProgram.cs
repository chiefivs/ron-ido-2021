using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LearnProgram
    {
        public LearnProgram()
        {
            LearnProgramDetails = new HashSet<LearnProgramDetail>();
            SpecialitiesLearnPrograms = new HashSet<SpecialitiesLearnProgram>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LearnProgramDetail> LearnProgramDetails { get; set; }
        public virtual ICollection<SpecialitiesLearnProgram> SpecialitiesLearnPrograms { get; set; }
    }
}
