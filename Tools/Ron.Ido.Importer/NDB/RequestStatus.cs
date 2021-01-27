using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StepCondition { get; set; }
        public string AllowStepToStatuses { get; set; }
        public string StepAction { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
