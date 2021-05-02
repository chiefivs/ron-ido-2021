using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class Apply
    {
        [Key]
        public long Id { get; set; }



        public virtual List<ApplyBarCode> BarCodes { get; set; } = new List<ApplyBarCode>();
    }
}
