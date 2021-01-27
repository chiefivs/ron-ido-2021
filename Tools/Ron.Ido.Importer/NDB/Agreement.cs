using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Agreement
    {
        public Agreement()
        {
            AgreementApplyDocTypes = new HashSet<AgreementApplyDocType>();
            AgreementEduDocs = new HashSet<AgreementEduDoc>();
            AgreementVersions = new HashSet<AgreementVersion>();
            Ratifications = new HashSet<Ratification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AcceptDate { get; set; }
        public DateTime? SignatureDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? ValidDate { get; set; }
        public long? IdokbId { get; set; }
        public long? IdokbRev { get; set; }
        public string Comments { get; set; }
        public string Site { get; set; }

        public virtual ICollection<AgreementApplyDocType> AgreementApplyDocTypes { get; set; }
        public virtual ICollection<AgreementEduDoc> AgreementEduDocs { get; set; }
        public virtual ICollection<AgreementVersion> AgreementVersions { get; set; }
        public virtual ICollection<Ratification> Ratifications { get; set; }
    }
}
