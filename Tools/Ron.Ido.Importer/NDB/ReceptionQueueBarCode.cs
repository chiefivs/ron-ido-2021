using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ReceptionQueueBarCode
    {
        public int QueueId { get; set; }
        public string BarCode { get; set; }

        public virtual ReceptionQueue Queue { get; set; }
    }
}
