using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyTemplateField
    {
        public int Id { get; set; }
        public bool InTemplate { get; set; }
        public bool InSearch { get; set; }
        public string Default { get; set; }
        public int FieldId { get; set; }
        public int TemplateId { get; set; }

        public virtual ApplyField Field { get; set; }
        public virtual ApplyTemplate Template { get; set; }
    }
}
