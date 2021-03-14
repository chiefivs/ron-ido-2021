using Ron.Ido.EM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    public class LearnLevel : IOrdered, IDateDependent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
        
        public int OrderNum { get; set; }
        
        public DateTime? BeginDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }
        
        public virtual List<ApplyDocType> ApplyDocTypes { get; set; } = new List<ApplyDocType>();
    }
}
