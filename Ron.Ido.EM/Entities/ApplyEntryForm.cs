using Microsoft.EntityFrameworkCore;
using Ron.Ido.EM.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(OrderNum))]
    public class ApplyEntryForm : IOrdered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int OrderNum { get; set; }

    }
}
