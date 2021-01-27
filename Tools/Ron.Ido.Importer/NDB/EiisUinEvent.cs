using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class EiisUinEvent
    {
        public string Uin { get; set; }
        public string Operation { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? Date { get; set; }
        public int? PayReason { get; set; }
        public int? Amount { get; set; }
        public int? DocType { get; set; }
        public string DocNumber { get; set; }
        public int? CountryCode { get; set; }
        public int? Discount { get; set; }
        public DateTime? DiscountDate { get; set; }
        public Guid? SmevId { get; set; }
        public string Result { get; set; }
        public string ResultCode { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DoneTime { get; set; }
        public string EpguId { get; set; }
        public string PayerName { get; set; }
    }
}
