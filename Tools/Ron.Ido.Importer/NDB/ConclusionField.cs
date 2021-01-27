using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionField
    {
        public ConclusionField()
        {
            ConclusionAdditionalFields = new HashSet<ConclusionAdditionalField>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
        public int TypeLen { get; set; }
        public bool IsBuiltin { get; set; }
        public bool IsRequired { get; set; }
        public bool IsEnabled { get; set; }
        public string OrderNum { get; set; }
        public string Block { get; set; }
        public string Subblock { get; set; }
        public int InitialStatus { get; set; }
        public int? ParentFieldId { get; set; }
        public int? DecisionTemplateFieldDecisionFieldDecisionFieldId { get; set; }
        public bool? IsArchiveField { get; set; }
        public bool IsArchiveRequired { get; set; }

        public virtual ConclusionTemplateField DecisionTemplateFieldDecisionFieldDecisionField { get; set; }
        public virtual ICollection<ConclusionAdditionalField> ConclusionAdditionalFields { get; set; }
    }
}
