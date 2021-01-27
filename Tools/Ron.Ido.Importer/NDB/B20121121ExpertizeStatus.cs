using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class B20121121ExpertizeStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public string ViewGrants { get; set; }
        public string StepCondition { get; set; }
        public string AllowStepToStatuses { get; set; }
        public string StepAction { get; set; }
    }
}
