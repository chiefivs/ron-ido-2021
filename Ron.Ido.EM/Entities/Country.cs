using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(Name))]
    [Index(nameof(NameEng))]
    [Index(nameof(OrderNum))]
    [Index(nameof(A2code))]
    [Index(nameof(A3code))]
    [Index(nameof(EiisCode))]
    [Index(nameof(IsgaCode))]
    [Index(nameof(OksmCode))]
    [Index(nameof(OldId))]
    public class Country
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string NameEng { get; set; }
        
        public bool? LegalizationNeeded { get; set; }
        
        public string LegalizationComment { get; set; }
        
        public int OrderNum { get; set; }

        #region Коды
        [StringLength(2)]
        public string A2code { get; set; }

        [StringLength(3)]
        public string A3code { get; set; }
 
        public int? EiisCode { get; set; }
        
        [StringLength(50)]
        public string IsgaCode { get; set; }

        [StringLength(50)]
        public string OksmCode { get; set; }
        #endregion

        public long? RegionId { get; set; }
        public virtual Region Region { get; set; }
        
        public int? LegalizationId { get; set; }
        
        public double? CoordX { get; set; }
        
        public double? CoordY { get; set; }


        public int OldId { get; set; }
    }
}
