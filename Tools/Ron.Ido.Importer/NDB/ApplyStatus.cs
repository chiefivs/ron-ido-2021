using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyStatus
    {
        public ApplyStatus()
        {
            Applies = new HashSet<Apply>();
            ApplyStatusHistoryPrevStatuses = new HashSet<ApplyStatusHistory>();
            ApplyStatusHistoryStatuses = new HashSet<ApplyStatusHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameForButton { get; set; }
        public string NameForApplier { get; set; }
        public string DescriptionForApplier { get; set; }
        public bool VisibleForApplier { get; set; }
        public string ViewGrants { get; set; }
        public string StepGrants { get; set; }
        public string StepCondition { get; set; }
        public string AllowStepToStatuses { get; set; }
        public string StepAction { get; set; }
        public string RollbackAction { get; set; }
        public string DateFieldName { get; set; }
        public int? EtapId { get; set; }
        public bool IsBuiltin { get; set; }
        public int OrderNum { get; set; }
        public bool EmailNotification { get; set; }
        public string NameForApplierEng { get; set; }
        public string DescriptionForApplierEng { get; set; }

        public virtual ReglamentEtap Etap { get; set; }
        public virtual ICollection<Apply> Applies { get; set; }
        public virtual ICollection<ApplyStatusHistory> ApplyStatusHistoryPrevStatuses { get; set; }
        public virtual ICollection<ApplyStatusHistory> ApplyStatusHistoryStatuses { get; set; }
    }
}
