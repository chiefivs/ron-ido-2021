using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.EM.Entities
{
    [Index(nameof(EpguCode))]
    public class Apply
    {
        [Key]
        public long Id { get; set; }

        #region Служебная информация
        public virtual List<ApplyBarCode> BarCodes { get; set; } = new List<ApplyBarCode>();

        [StringLength(100)]
        public string EpguCode { get; set; }
        #endregion

        #region Заявитель
        [StringLength(255)]
        public string CreatorFirstName { get; set; }
        
        [StringLength(255)]
        public string CreatorLastName { get; set; }
        
        [StringLength(255)]
        
        public string CreatorSurname { get; set; }
        
        public DateTime? CreatorBirthDate { get; set; }
        
        //public string CreatorPassportType { get; set; }
        public long? CreatorPassportTypeId { get; set; }
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

        [StringLength(100)]
        public string WarrantReq { get; set; }

        public DateTime? WarrantDate { get; set; }

        public DateTime? WarrantTerm { get; set; }
        #endregion

        #region Обладатель документа
        [StringLength(255)]
        public string OwnerFirstName { get; set; }

        [StringLength(255)]
        public string OwnerLastName { get; set; }

        [StringLength(255)]
        public string OwnerSurname { get; set; }

        public DateTime? OwnerBirthDate { get; set; }

        //public string OwnerPassportType { get; set; }
        public long? OwnerPassportTypeId { get; set; }
        public virtual ApplyPassportType OwnerPassportType { get; set; }

        [StringLength(250)]
        public string OwnerPassportReq { get; set; }

        [StringLength(10)]
        public string OwnerMailIndex { get; set; }

        [StringLength(100)]
        public string OwnerCityName { get; set; }

        [StringLength(100)]
        public string OwnerStreet { get; set; }

        [StringLength(10)]
        public string OwnerCorpus { get; set; }

        [StringLength(10)]
        public string OwnerBuilding { get; set; }

        [StringLength(10)]
        public string OwnerBlock { get; set; }

        [StringLength(10)]
        public string OwnerFlat { get; set; }

        [StringLength(100)]
        public string OwnerPhone { get; set; }
        #endregion

        #region Документ
        public long? DocCountryId { get; set; }
        #endregion

    }
}
