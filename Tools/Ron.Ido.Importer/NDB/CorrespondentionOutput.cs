using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionOutput
    {
        public CorrespondentionOutput()
        {
            CorrespondentionOutputAttaches = new HashSet<CorrespondentionOutputAttach>();
            CorrespondentionOutputBarCodes = new HashSet<CorrespondentionOutputBarCode>();
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int OrderNum { get; set; }
        public string DocNum { get; set; }
        public string Organization { get; set; }
        public string Correspondent { get; set; }
        public string Description { get; set; }
        public int? PerformerId { get; set; }
        public int? SignerId { get; set; }
        public int Priority { get; set; }
        public string Remark { get; set; }
        public bool IsPrinted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedById { get; set; }
        public int? StatusId { get; set; }
        public int? InputId { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User DeletedBy { get; set; }
        public virtual CorrespondentionInput Input { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual User Performer { get; set; }
        public virtual User Signer { get; set; }
        public virtual CorrespondentionOutputStatus Status { get; set; }
        public virtual ICollection<CorrespondentionOutputAttach> CorrespondentionOutputAttaches { get; set; }
        public virtual ICollection<CorrespondentionOutputBarCode> CorrespondentionOutputBarCodes { get; set; }
    }
}
