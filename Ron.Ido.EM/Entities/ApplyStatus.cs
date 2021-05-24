using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class ApplyStatus
    {
        [Key]
        public long Id { get; set; }

        public string StatusEnumValue { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NameForButton { get; set; }

        [StringLength(50)]
        public string NameForApplier { get; set; }

        [StringLength(50)]
        public string NameForApplierEng { get; set; }

        [StringLength(1000)]
        public string DescriptionForApplier { get; set; }

        [StringLength(1000)]
        public string DescriptionForApplierEng { get; set; }

        public bool VisibleForApplier { get; set; }

        public string AllowStepToStatuses { get; set; }

        public long? EtapId { get; set; }
        public virtual ReglamentEtap Etap { get; set; }

        public int OldId { get; set; }
    }
}
