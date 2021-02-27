using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    public class ApplyTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public int OrderNum { get; set; }


    }
}
