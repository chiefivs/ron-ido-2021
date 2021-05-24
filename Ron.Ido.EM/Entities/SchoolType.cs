using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(NameEng))]
    [Index(nameof(OrderNum))]
    public class SchoolType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string NameEng { get; set; }

        public int OrderNum { get; set; }
    }
}
