using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class DocxTemplate
    {
        public DocxTemplate()
        {
            ApplyRonLetters = new HashSet<ApplyRonLetter>();
            InfoLetters = new HashSet<InfoLetter>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public int DataLength { get; set; }
        public int ReceiverAs { get; set; }
        public string Signers { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public string Param { get; set; }

        public virtual ICollection<ApplyRonLetter> ApplyRonLetters { get; set; }
        public virtual ICollection<InfoLetter> InfoLetters { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
