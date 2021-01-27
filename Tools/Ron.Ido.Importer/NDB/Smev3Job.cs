using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Smev3Job
    {
        public int Id { get; set; }
        public string ApplyBarCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string Uri { get; set; }
        public string AppNum { get; set; }
        public int? Reason { get; set; }
        public string Parameters { get; set; }
        public string SendRequestStatus { get; set; }
        public DateTime? SendRequestTime { get; set; }
        public long? SendRequestId { get; set; }
        public string GetResponseStatus { get; set; }
        public DateTime? GetResponseTime { get; set; }
        public string GetResponseContext { get; set; }
    }
}
