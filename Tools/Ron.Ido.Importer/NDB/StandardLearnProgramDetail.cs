using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class StandardLearnProgramDetail
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int StandardLearnProgramStandardLearnProgramDetailStandardLearnProgramDetailId { get; set; }
        public int? CourseId { get; set; }

        public virtual LearnCourse Course { get; set; }
        public virtual StandardLearnProgram StandardLearnProgramStandardLearnProgramDetailStandardLearnProgramDetail { get; set; }
    }
}
