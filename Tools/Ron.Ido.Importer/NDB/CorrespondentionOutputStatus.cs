using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class CorrespondentionOutputStatus
    {
        public CorrespondentionOutputStatus()
        {
            CorrespondentionOutputs = new HashSet<CorrespondentionOutput>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }

        public virtual ICollection<CorrespondentionOutput> CorrespondentionOutputs { get; set; }
    }
}
