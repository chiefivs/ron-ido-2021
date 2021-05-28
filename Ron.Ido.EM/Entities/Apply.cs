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

        public int? EpguStatus { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? AcceptTime { get; set; }

        public string Storage { get; set; }

        public bool IsEnglish { get; set; }

        /// <summary>
        /// Передача по открытым информационным каналам
        /// </summary>
        public bool TransmitOpenChannels { get; set; }

        public bool DocsWillSendByPost { get; set; }

        /// <summary>
        /// для инфописьма
        /// </summary>
        public bool ForInfoLetter { get; set; }

        /// <summary>
        /// заключение о признании в силу закона
        /// </summary>
        public bool ForOferta { get; set; }

        public bool Deleted { get; set; }
        
        public long StatusId { get; set; }
        public virtual ApplyStatus Status { get; set; }
        public DateTime? StatusChangeTime { get; set; }
        public virtual List<ApplyStatusHistory> StatusHistories { get; set; } = new List<ApplyStatusHistory>();
        #endregion

        #region Заявитель
        [StringLength(255)]
        public string CreatorFirstName { get; set; }
        
        [StringLength(255)]
        public string CreatorLastName { get; set; }
        
        [StringLength(255)]
        
        public string CreatorSurname { get; set; }
        
        public DateTime? CreatorBirthDate { get; set; }

        /// <summary>
        /// Гражданство заявителя
        /// </summary>
        public long? CreatorCitizenshipId { get; set; }
        public virtual Country CreatorCitizenship { get; set; }

        //public string CreatorPassportType { get; set; }
        public long? CreatorPassportTypeId { get; set; }
        public virtual ApplyPassportType CreatorPassportType { get; set; }

        /// <summary>
        /// Реквизиты документа
        /// </summary>
        [StringLength(250)]
        public string CreatorPassportReq { get; set; }
        #endregion

        #region Доверенность (если заявитель и владелец документа - разные лица
        public bool ByWarrant { get; set; }

        [StringLength(100)]
        public string WarrantReq { get; set; }

        public DateTime? WarrantDate { get; set; }

        public DateTime? WarrantTerm { get; set; }
        #endregion

        #region Контактная информация (Адрес доставки) для обратной связи с заявителем
        public long? CreatorCountryId { get; set; }
        public virtual Country CreatorCountry { get; set; }

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

        /// <summary>
        /// Форма получения результата
        /// </summary>
        public long? DeliveryFormId { get; set; }
        public virtual ApplyDeliveryForm DeliveryForm { get; set; }

        /// <summary>
        /// Формы получения электронного свидетельства
        /// </summary>
        public virtual List<ApplyCertificateDeliveryForm> CertificateDeliveryForms { get; set; } = new List<ApplyCertificateDeliveryForm>(); //DigSvidDelivery


        /// <summary>
        /// Форма получения результата
        /// </summary>
        public long? ReturnOriginalsFormId { get; set; }
        public virtual ApplyDeliveryForm ReturnOriginalsForm { get; set; }
        #endregion

        #region Обладатель документа
        [StringLength(255)]
        public string OwnerFirstName { get; set; }

        [StringLength(255)]
        public string OwnerLastName { get; set; }

        [StringLength(255)]
        public string OwnerSurname { get; set; }

        public DateTime? OwnerBirthDate { get; set; }

        /// <summary>
        /// Страна жительства обладателя
        /// </summary>
        public long? OwnerCountryId { get; set; }
        public virtual Country OwnerCountry { get; set; }

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

        /// <summary>
        /// Гражданство обладателя
        /// </summary>
        public long? OwnerCitizenshipId { get; set; }
        public virtual Country OwnerCitizenship { get; set; }

        //public string OwnerPassportType { get; set; }
        public long? OwnerPassportTypeId { get; set; }
        public virtual ApplyPassportType OwnerPassportType { get; set; }

        [StringLength(250)]
        public string OwnerPassportReq { get; set; }
        #endregion

        #region Документ
        public long? DocCountryId { get; set; }
        public virtual Country DocCountry { get; set; }

        public long? DocTypeId { get; set; }
        public virtual ApplyDocType DocType { get; set; }

        public string DocBlankNum { get; set; }

        public string DocRegNum { get; set; }

        public string DocDate { get; set; }
        public int? DocDateYear { get; set; }

        public int? DocAttachmentsCount { get; set; } // DocAttachment

        /// <summary>
        /// ФИО по документу
        /// </summary>
        public string DocFullName { get; set; }

        #region Учебное заведение, выдавшее документ
        public long? SchoolCountryId { get; set; }
        public virtual Country SchoolCountry { get; set; }

        public string SchoolName { get; set; }

        public long? SchoolTypeId { get; set; }
        public virtual SchoolType SchoolType { get; set; }

        #region Адрес учебного заведения
        public string SchoolPostIndex { get; set; }

        public string SchoolCityName { get; set; }

        public string SchoolAddress { get; set; }
        #endregion

        #region Контактные данные учебного заведения
        public string SchoolPhone { get; set; }

        public string SchoolFax { get; set; }

        public string SchoolEmail { get; set; }
        #endregion
        #endregion
        #endregion

        #region Сведения о полученном образовании
        #region Период обучения по общеобразовательной программе (аттестат)
        public DateTime? BaseLearnDateBegin { get; set; }
        public DateTime? BaseLearnDateEnd { get; set; }
        #endregion

        #region Период обучения по программе(ам) профессионального образования
        public DateTime? SpecialLearnDateBegin { get; set; }
        public DateTime? SpecialLearnDateEnd { get; set; }
        #endregion

        #region Сведения
        public string FixedLearnSpecialityName { get; set; }

        public long? SpecialLearnFormId { get; set; }
        public virtual ApplyLearnForm SpecialLearnForm { get; set; }
        #endregion

        #region Цель признания
        public long? AimId { get; set; }
        public virtual ApplyAim Aim { get; set; }

        /// <summary>
        /// Организация, запросившая признание
        /// </summary>
        public string OrgCreator { get; set; }

        /// <summary>
        /// Подробнее...
        /// </summary>
        public string Other { get; set; }
        #endregion

        #endregion

        #region Форма приема
        public long? EntryFormId { get; set; }
        public virtual ApplyEntryForm EntryForm { get; set; }

        /// <summary>
        /// ЮВ Украины
        /// </summary>
        public bool IsNovorossia { get; set; }

        /// <summary>
        /// филиал Ростов
        /// </summary>
        public bool IsRostovFilial { get; set; }
        #endregion

        public virtual List<ApplyAttachment> Attachments { get; set; } = new List<ApplyAttachment>();
    }
}
