using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(BarCode))]
    [Index(nameof(AssignTime))]
    public class ApplyBarCode
    {
        [Key, StringLength(12)]
        public string BarCode { get; set; }
        public long ApplyId { get; set; }
        public DateTime AssignTime { get; set; }

        public virtual Apply Apply { get; set; }
    }
}
