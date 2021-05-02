using System;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class ApplyBarCode
    {
        [Key]
        public string BarCode { get; set; }
        public long ApplyId { get; set; }
        public DateTime AssignTime { get; set; }
    }
}
