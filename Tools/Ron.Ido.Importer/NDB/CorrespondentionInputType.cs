using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionInputType
    {
        public CorrespondentionInputType()
        {
            CorrespondentionInputs = new HashSet<CorrespondentionInput>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<CorrespondentionInput> CorrespondentionInputs { get; set; }
    }
}
