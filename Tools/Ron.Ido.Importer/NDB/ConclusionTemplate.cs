using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ConclusionTemplate
    {
        public ConclusionTemplate()
        {
            ConclusionTemplateFields = new HashSet<ConclusionTemplateField>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<ConclusionTemplateField> ConclusionTemplateFields { get; set; }
    }
}
