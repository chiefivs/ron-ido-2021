﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(NameEng))]
    [Index(nameof(OrderNum))]
    public class CertificateDeliveryForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string NameEng { get; set; }

        public int OrderNum { get; set; }
    }
}
