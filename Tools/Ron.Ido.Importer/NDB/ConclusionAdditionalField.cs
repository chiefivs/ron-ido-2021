using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionAdditionalField
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int DecisionDecisionAdditionalFieldDecisionAdditionalFieldId { get; set; }
        public int FieldId { get; set; }

        public virtual Conclusion DecisionDecisionAdditionalFieldDecisionAdditionalField { get; set; }
        public virtual ConclusionField Field { get; set; }
    }
}
