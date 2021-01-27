using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class AgreementEduDoc
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameNative { get; set; }
        public string EduLevel { get; set; }
        public int AgreementId { get; set; }
        public bool IsGraduation { get; set; }

        public virtual Agreement Agreement { get; set; }
    }
}
