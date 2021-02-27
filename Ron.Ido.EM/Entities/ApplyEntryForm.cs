using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.EM.Entities
{
    public class ApplyEntryForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int OrderNum { get; set; }

    }
}
