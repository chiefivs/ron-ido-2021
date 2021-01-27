using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyRonStatusHistory
    {
        public int Id { get; set; }
        public DateTime ChangeTime { get; set; }
        public string ApplyRonBarCode { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int? PrevStatusId { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual ApplyRon ApplyRonBarCodeNavigation { get; set; }
        public virtual ApplyRonStatus PrevStatus { get; set; }
        public virtual ApplyRonStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}
