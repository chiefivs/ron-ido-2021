using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ApplyStatusHistory
    {
        public int Id { get; set; }
        public DateTime ChangeTime { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? RollbackTime { get; set; }
        public string ApplyBarCode { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int? StatusFromOldDb { get; set; }
        public int? PrevStatusId { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Apply ApplyBarCodeNavigation { get; set; }
        public virtual ApplyStatus PrevStatus { get; set; }
        public virtual ApplyStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}
