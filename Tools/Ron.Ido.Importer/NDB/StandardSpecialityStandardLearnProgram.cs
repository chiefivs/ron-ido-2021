using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardSpecialityStandardLearnProgram
    {
        public int SpecialitiesId { get; set; }
        public int LearnProgramsId { get; set; }

        public virtual StandardLearnProgram LearnPrograms { get; set; }
        public virtual StandardSpeciality Specialities { get; set; }
    }
}
