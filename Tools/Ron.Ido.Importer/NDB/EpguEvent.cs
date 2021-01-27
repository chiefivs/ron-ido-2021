using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class EpguEvent
    {
        public int Id { get; set; }
        public string EpguCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string EventType { get; set; }
        public int? EpguStatus { get; set; }
        public string Uin { get; set; }
        public string Comment { get; set; }
        public DateTime? ThreatTime { get; set; }
        public int? ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }
}
