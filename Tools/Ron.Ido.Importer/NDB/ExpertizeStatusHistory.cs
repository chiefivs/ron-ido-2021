using System;
using System.Collections.Generic;

#nullable disable

namespace Ron.Ido.Importer
{
    public partial class ExpertizeStatusHistory
    {
        public int Id { get; set; }
        public DateTime? ChangeTime { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int ExpertizeId { get; set; }

        public virtual Expertize Expertize { get; set; }
        public virtual ExpertizeStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}
