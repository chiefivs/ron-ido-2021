using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(ChangeTime))]
    [Index(nameof(EndTime))]
    public class DuplicateStatusHistory
    {
        [Key]
        public long Id { get; set; }

        public long DuplicateId { get; set; }
        public virtual Duplicate Duplicate { get; set; }

        public long StatusId { get; set; }
        public virtual DuplicateStatus Status { get; set; } 

        public long? PrevStatusId { get; set; }
        public virtual DuplicateStatus PrevStatus { get; set; }

        public DateTime ChangeTime { get; set; }

        public DateTime? EndTime { get; set; }

        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
