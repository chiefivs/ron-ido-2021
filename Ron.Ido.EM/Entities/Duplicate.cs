using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.EM.Entities
{
    public class Duplicate
    {
        [Key]
        public long Id { get; set; }

        #region Служебная информация
        [StringLength(12)]
        public string BarCode { get; set; }

        public DateTime CreateTime { get; set; }
        public long? CreateUserId { get; set; } //  NDB: CreatedById
        public virtual User CreateUser { get; set; }

        public DateTime? HandoutTime { get; set; }
        public long? HandoutUserId { get; set; }
        public virtual User HandoutUser { get; set; }

        public bool IsEnglish { get; set; }

        public int StatusId { get; set; }
        public virtual DuplicateStatus Status { get; set; }
        public virtual List<DuplicateStatusHistory> StatusHistories { get; set; } = new List<DuplicateStatusHistory>();
        public virtual List<Dossier> Dossiers { get; set; } = new List<Dossier>();

        /// <summary>
        /// Место в накопителе
        /// </summary>
        [StringLength(1000)]
        public string Storage { get; set; }

        [StringLength(100)]
        public string Note { get; set; }
        #endregion

        #region Заявитель
        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string MailIndex { get; set; }

        [StringLength(100)]
        public string CityName { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(100)]
        public string Block { get; set; }

        [StringLength(100)]
        public string Flat { get; set; }

        [StringLength(100)]
        public string Corpus { get; set; }

        [StringLength(100)]
        public string Building { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Phones { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public long? CreatorCountryId { get; set; }
        public Country CreatorCountry { get; set; }
        #endregion

        #region Документ
        [StringLength(200)]
        public string DocFullName { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }

        public long? DocCountryId { get; set; }
        public virtual Country DocCountry { get; set; }

        public long? DocumentTypeId { get; set; }
        public virtual ApplyDocType DocumentType { get; set; }

        [StringLength(100)]
        public string DocumentDate { get; set; }
        #endregion

        #region Выдача результата
        /// <summary>
        /// Форма получения оригиналов документов
        /// </summary>
        public long? ReturnOriginalsFormId { get; set; }
        public virtual ApplyDeliveryForm ReturnOriginalsForm { get; set; }

        public string ReturnOriginalsPostAddress { get; set; }

        /// <summary>
        /// Формы получения электронного свидетельства
        /// </summary>
        public virtual List<DuplicateCertificateDeliveryForm> CertificateDeliveryForms { get; set; } = new List<DuplicateCertificateDeliveryForm>(); //DigSvidDelivery

        #endregion
    }
}
