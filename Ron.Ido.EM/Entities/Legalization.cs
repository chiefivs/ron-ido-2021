using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(OrderNum))]
    public class Legalization
    {
        [Key]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int OrderNum { get; set; }
    }
}
