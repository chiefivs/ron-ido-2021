using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyTemplate
    {
        public ApplyTemplate()
        {
            ApplyTemplateFields = new HashSet<ApplyTemplateField>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int? OrderNum { get; set; }

        public virtual ICollection<ApplyTemplateField> ApplyTemplateFields { get; set; }
    }
}
