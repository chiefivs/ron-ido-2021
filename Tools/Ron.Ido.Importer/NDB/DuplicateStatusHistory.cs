using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class DuplicateStatusHistory
    {
        public int Id { get; set; }
        public string DuplicateBarCode { get; set; }
        public int StatusId { get; set; }
        public DateTime ChangeTime { get; set; }
        public int? PrevStatusId { get; set; }
        public int? UserId { get; set; }

        public virtual Duplicate DuplicateBarCodeNavigation { get; set; }
        public virtual DuplicateStatus PrevStatus { get; set; }
        public virtual DuplicateStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}
