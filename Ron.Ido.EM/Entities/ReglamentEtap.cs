using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(OrderNum))]
    public class ReglamentEtap
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int MinTerm { get; set; }

        public int MaxTerm { get; set; }

        public bool? Required { get; set; }

        public int OrderNum { get; set; }
    }
}
