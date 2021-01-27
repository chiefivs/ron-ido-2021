using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyField
    {
        public ApplyField()
        {
            ApplyAdditionalFields = new HashSet<ApplyAdditionalField>();
            ApplyErrors = new HashSet<ApplyError>();
            ApplyTemplateFields = new HashSet<ApplyTemplateField>();
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
        public int InitialStatus { get; set; }
        public string Block { get; set; }
        public string Subblock { get; set; }
        public string OrderNum { get; set; }
        public bool IsArchiveField { get; set; }
        public bool IsArchiveRequired { get; set; }
        public bool? IsPortal { get; set; }
        public bool? IsTerminal { get; set; }
        public bool IsForInfoLetterField { get; set; }
        public bool IsForInfoLetterRequired { get; set; }
        public string LabelEng { get; set; }
        public string RemarkEng { get; set; }
        public string BlockEng { get; set; }
        public string SubblockEng { get; set; }

        public virtual ICollection<ApplyAdditionalField> ApplyAdditionalFields { get; set; }
        public virtual ICollection<ApplyError> ApplyErrors { get; set; }
        public virtual ICollection<ApplyTemplateField> ApplyTemplateFields { get; set; }
    }
}
