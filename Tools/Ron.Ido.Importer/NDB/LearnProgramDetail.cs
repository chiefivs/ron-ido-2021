using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class LearnProgramDetail
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int LearnProgramLearnProgramDetailLearnProgramDetailId { get; set; }
        public int? CourseId { get; set; }

        public virtual LearnCourse Course { get; set; }
        public virtual LearnProgram LearnProgramLearnProgramDetailLearnProgramDetail { get; set; }
    }
}
