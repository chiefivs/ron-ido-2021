using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Apostille
    {
        public string IsgaCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public string EduLevelIsgaCode { get; set; }
        public string ReasonIsgaCode { get; set; }
        public string ReasonText { get; set; }
        public string CountryIsgaCode { get; set; }
        public string CountryOutIsgaCode { get; set; }
    }
}
