using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class B20121121ApplyStatusHistory
    {
        public int Id { get; set; }
        public DateTime ChangeTime { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? RollbackTime { get; set; }
        public string ApplyBarCode { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
    }
}
