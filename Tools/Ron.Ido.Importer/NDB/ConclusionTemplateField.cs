using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionTemplateField
    {
        public ConclusionTemplateField()
        {
            ConclusionFields = new HashSet<ConclusionField>();
        }

        public int Id { get; set; }
        public bool InTemplate { get; set; }
        public bool InSearch { get; set; }
        public string Default { get; set; }
        public int DecisionTemplateDecisionTemplateFieldDecisionTemplateFieldId { get; set; }

        public virtual ConclusionTemplate DecisionTemplateDecisionTemplateFieldDecisionTemplateField { get; set; }
        public virtual ICollection<ConclusionField> ConclusionFields { get; set; }
    }
}
