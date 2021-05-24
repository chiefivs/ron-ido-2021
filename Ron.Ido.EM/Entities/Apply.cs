using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    public class Apply
    {
        [Key]
        public long Id { get; set; }

        public virtual List<ApplyBarCode> BarCodes { get; set; } = new List<ApplyBarCode>();

        [StringLength(100)]
        public string EpguCode { get; set; }

        #region Заявитель
        [StringLength(255)]
        public string CreatorFirstName { get; set; }
        
        [StringLength(255)]
        public string CreatorLastName { get; set; }
        
        [StringLength(255)]
        
        public string CreatorSurname { get; set; }
        
        public DateTime? CreatorBirthDate { get; set; }
        
        //public string CreatorPassportType { get; set; }
        public int? CreatorPassportTypeId { get; set; }
        public virtual ApplyPassportType CreatorPassportType { get; set; }

        [StringLength(250)]
        public string CreatorPassportReq { get; set; }

        [StringLength(10)]
        public string CreatorMailIndex { get; set; }

        [StringLength(100)]
        public string CreatorCityName { get; set; }

        [StringLength(100)]
        public string CreatorStreet { get; set; }

        [StringLength(10)]
        public string CreatorCorpus { get; set; }

        [StringLength(10)]
        public string CreatorBuilding { get; set; }

        [StringLength(10)]
        public string CreatorBlock { get; set; }

        [StringLength(10)]
        public string CreatorFlat { get; set; }

        [StringLength(100)]
        public string CreatorPhone { get; set; }

        [StringLength(100)]
        public string CreatorEmail { get; set; }
        #endregion

        #region Доверенность (если заявитель и владелец документа - разные лица
        public bool ByWarrant { get; set; }
        public string WarrantReq { get; set; }
        public DateTime? WarrantDate { get; set; }
        public DateTime? WarrantTerm { get; set; }
        #endregion

        #region Обладатель документа
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerSurname { get; set; }
        public DateTime? OwnerBirthDate { get; set; }
        public string OwnerPassportType { get; set; }
        public string OwnerPassportReq { get; set; }
        public string OwnerMailIndex { get; set; }
        public string OwnerCityName { get; set; }
        public string OwnerStreet { get; set; }
        public string OwnerCorpus { get; set; }
        public string OwnerBuilding { get; set; }
        public string OwnerBlock { get; set; }
        public string OwnerFlat { get; set; }
        public string OwnerPhone { get; set; }
        #endregion

    }
}
