using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardLearnProgram
    {
        public StandardLearnProgram()
        {
            StandardLearnProgramDetails = new HashSet<StandardLearnProgramDetail>();
            StandardSpecialityStandardLearnPrograms = new HashSet<StandardSpecialityStandardLearnProgram>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<StandardLearnProgramDetail> StandardLearnProgramDetails { get; set; }
        public virtual ICollection<StandardSpecialityStandardLearnProgram> StandardSpecialityStandardLearnPrograms { get; set; }
    }
}
