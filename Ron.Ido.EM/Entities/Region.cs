using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(OrderNum))]
    public class Region
    {
        [Key]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        
        public int OrderNum { get; set; }

        public virtual List<Country> Countries { get; set; } = new List<Country>();
    }
}
