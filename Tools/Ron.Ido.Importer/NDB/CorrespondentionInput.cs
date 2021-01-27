using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionInput
    {
        public CorrespondentionInput()
        {
            CorrespondentionInputAttaches = new HashSet<CorrespondentionInputAttach>();
            CorrespondentionInputBarCodes = new HashSet<CorrespondentionInputBarCode>();
            CorrespondentionOutputs = new HashSet<CorrespondentionOutput>();
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int OrderNum { get; set; }
        public string DocNum { get; set; }
        public string Organization { get; set; }
        public string Correspondent { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string Remark { get; set; }
        public bool IsPrinted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedById { get; set; }
        public int? PerformerId { get; set; }
        public int? TypeId { get; set; }
        public string InpNum { get; set; }
        public DateTime? TermTime { get; set; }
        public bool IsTermNotify { get; set; }
        public bool IsTermNotified { get; set; }
        public int? DeliveryTypeId { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User DeletedBy { get; set; }
        public virtual CorrespondentionInputDeliveryType DeliveryType { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual User Performer { get; set; }
        public virtual CorrespondentionInputType Type { get; set; }
        public virtual ICollection<CorrespondentionInputAttach> CorrespondentionInputAttaches { get; set; }
        public virtual ICollection<CorrespondentionInputBarCode> CorrespondentionInputBarCodes { get; set; }
        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputs { get; set; }
    }
}
