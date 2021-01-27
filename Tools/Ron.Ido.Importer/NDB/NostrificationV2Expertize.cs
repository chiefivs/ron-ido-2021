using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class NostrificationV2Expertize
    {
        public string BarCode { get; set; }
        public string CbarCode { get; set; }
        public int? Ayear { get; set; }
        public string Amonth { get; set; }
        public string Aday { get; set; }
        public DateTime? EndDate { get; set; }
        public string SourceDocumentType { get; set; }
        public string StandardDocumentType { get; set; }
        public string SchoolName { get; set; }
        public string LearnForm { get; set; }
        public string Aim { get; set; }
        public string LearnLevelMsko { get; set; }
        public string LearnLevel { get; set; }
        public string StandardSpeciality { get; set; }
        public string StandardQualification { get; set; }
        public string Result { get; set; }
        public string Reason { get; set; }
        public string FullName { get; set; }
        public string IdoCountry { get; set; }
    }
}
