using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.BM.Models.Dossier
{
    public class ApplyDto: IValidatableObject
    {
        public long Id { get; set; }

        public DateTime CreateTime { get; set; }

        //public bool IsEnglish { get; set; }

        //public bool ForInfoLetter { get; set; }

        //public bool ForOferta { get; set; }

        public bool TransmitOpenChannels { get; set; }

        public bool DocsWillSendByPost { get; set; }

        #region Заявитель
        [StringLength(255)]
        public string CreatorFirstName { get; set; }

        public bool IsCreatorFirstNameAbsent { get; set; }

        [StringLength(255)]
        public string CreatorLastName { get; set; }

        public bool IsCreatorLastNameAbsent { get; set; }

        [StringLength(255)]

        public string CreatorSurname { get; set; }

        public bool IsCreatorSurnameAbsent { get; set; }

        public int CreatorGender { get; set; }

        public DateTime? CreatorBirthDate { get; set; }

        public string CreatorBirthPlace { get; set; }

        /// <summary>
        /// Гражданство заявителя
        /// </summary>
        public long? CreatorCitizenshipId { get; set; }

        //public string CreatorPassportType { get; set; }
        public long? CreatorPassportTypeId { get; set; }

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

        /// <summary>
        /// Формы получения электронного свидетельства
        /// </summary>
        public IEnumerable<long> CertificateDeliveryForms { get; set; }


        /// <summary>
        /// Форма получения оригиналов документов
        /// </summary>
        public long? ReturnOriginalsFormId { get; set; }

        public bool IsReturnOriginalsPostAddressDifferent { get; set; }

        public string ReturnOriginalsPostAddress { get; set; }
        #endregion

        #region Обладатель документа
        [StringLength(255)]
        public string OwnerFirstName { get; set; }

        public bool IsOwnerFirstNameAbsent { get; set; }

        [StringLength(255)]
        public string OwnerLastName { get; set; }

        public bool IsOwnerLastNameAbsent { get; set; }

        [StringLength(255)]
        public string OwnerSurname { get; set; }

        public bool IsOwnerSurnameAbsent { get; set; }

        public int OwnerGender { get; set; }

        public DateTime? OwnerBirthDate { get; set; }

        public string OwnerBirthPlace { get; set; }

        /// <summary>
        /// Страна жительства обладателя
        /// </summary>
        public long? OwnerCountryId { get; set; }

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

        [StringLength(100)]
        public string OwnerEmail { get; set; }

        /// <summary>
        /// Гражданство обладателя
        /// </summary>
        public long? OwnerCitizenshipId { get; set; }

        //public string OwnerPassportType { get; set; }
        public long? OwnerPassportTypeId { get; set; }

        [StringLength(250)]
        public string OwnerPassportReq { get; set; }
        #endregion

        #region Документ
        public long? DocCountryId { get; set; }

        public long? DocTypeId { get; set; }

        /// <summary>
        /// Наименование иностранного документа
        /// </summary>
        public string DocDescription { get; set; }

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

        public string SchoolName { get; set; }

        public long? SchoolTypeId { get; set; }

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
        #endregion

        #region Цель признания
        public long? AimId { get; set; }

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

        /// <summary>
        /// ЮВ Украины
        /// </summary>
        public bool IsNovorossia { get; set; }

        /// <summary>
        /// филиал Ростов
        /// </summary>
        public bool IsRostovFilial { get; set; }
        #endregion

        #region Прилагаемые документы
        public IEnumerable<ApplyAttachmentDto> Attachments { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(CreatorSurname) && !IsCreatorSurnameAbsent)
                yield return new ValidationResult("Поле обязательно к заполнению", new[] { nameof(CreatorSurname) });

            if (string.IsNullOrWhiteSpace(CreatorFirstName) && !IsCreatorFirstNameAbsent)
                yield return new ValidationResult("Поле обязательно к заполнению", new[] { nameof(CreatorFirstName) });

            if (string.IsNullOrWhiteSpace(CreatorLastName) && !IsCreatorLastNameAbsent)
                yield return new ValidationResult("Поле обязательно к заполнению", new[] { nameof(CreatorLastName) });
        }
    }
}
