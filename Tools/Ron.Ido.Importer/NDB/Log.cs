using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }
        public string ApplyBarCode { get; set; }
        public Guid? ForTest { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
