using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyDocumentType
    {
        public ApplyDocumentType()
        {
            ApplyDocuments = new HashSet<ApplyDocument>();
            ApplyRonDocuments = new HashSet<ApplyRonDocument>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public bool Required { get; set; }
        public bool ForArchive { get; set; }
        public bool ForPortal { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<ApplyDocument> ApplyDocuments { get; set; }
        public virtual ICollection<ApplyRonDocument> ApplyRonDocuments { get; set; }
    }
}
