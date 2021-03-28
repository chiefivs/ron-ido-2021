using Ron.Ido.EM.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    public class ApplyPassportType : IOrdered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(30)]
        public string Code { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int OrderNum { get; set; }

    }
}
