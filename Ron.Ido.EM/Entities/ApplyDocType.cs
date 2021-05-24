using Microsoft.EntityFrameworkCore;
using Ron.Ido.EM.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(NameEng))]
    [Index(nameof(OrderNum))]
    [Index(nameof(BeginDate))]
    [Index(nameof(EndDate))]
    public class ApplyDocType : IOrdered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int OrderNum { get; set; }

        public long LearnLevelId { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(200)]
        public string NameEng { get; set; }

        public virtual LearnLevel LearnLevel { get; set; }
    }
}
