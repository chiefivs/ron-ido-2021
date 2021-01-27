using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class SpecialitiesLearnProgram
    {
        public int SpecialitiesId { get; set; }
        public int LearnProgramsId { get; set; }

        public virtual LearnProgram LearnPrograms { get; set; }
        public virtual Speciality Specialities { get; set; }
    }
}
