using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class NostrificationV2Request
    {
        public string CurrentBarCode { get; set; }
        public string BarCode { get; set; }
        public string OutNum { get; set; }
        public DateTime? Odate { get; set; }
        public DateTime? Idate { get; set; }
        public string ToName { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
