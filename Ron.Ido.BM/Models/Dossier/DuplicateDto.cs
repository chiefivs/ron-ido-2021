using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ron.Ido.BM.Models.Dossier
{
    public class DuplicateDto : IValidatableObject
    {
        public long Id { get; set; }

        #region Служебная информация
        /// <summary>
        /// Дата создания заявления
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// Дата выдачи дубликата свидетельства ИДО
        /// </summary>
        public string HandoutTime { get; set; }
        [StringLength(12)]
        public string BarCode { get; set; }


        /// <summary>
        /// Место в накопителе
        /// </summary>
        [StringLength(1000)]
        public string Storage { get; set; }

        /// ////////////
        public long? CreateUserId { get; set; } //  NDB: CreatedById

        public long? HandoutUserId { get; set; }

        public bool IsEnglish { get; set; }

        public long StatusId { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [StringLength(1000)]
        public string Note { get; set; }
        #endregion

        #region Заявитель
        /// <summary>
        /// Фамилия, Имя, Отчество подавшего заявку
        /// </summary>
        [StringLength(200)]
        public string FullName { get; set; }

        /// <summary>
        /// Индекс
        /// </summary>
        [StringLength(200)]
        public string MailIndex { get; set; }

        /// <summary>
        /// Область, район, населенный пункт	
        /// </summary>
        [StringLength(200)]
        public string CityName { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [StringLength(200)]
        public string Street { get; set; }

        /// <summary>
        /// Дом	??
        /// </summary>
        [StringLength(200)]
        public string Block { get; set; }

        /// <summary>
        ///  Квартира
        /// </summary>
        [StringLength(200)]
        public string Flat { get; set; }
        /// <summary>
        /// Корпус	
        /// </summary>
        [StringLength(200)]
        public string Corpus { get; set; }

        /// <summary>
        /// Строение	
        /// </summary>
        [StringLength(200)]
        public string Building { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Phones { get; set; }


        public long? CreatorCountryId { get; set; }
        #endregion

        #region Документ
        /// <summary>
        /// Фамилия, Имя, Отчество по документу	
        /// </summary>
        [StringLength(200)]
        public string DocFullName { get; set; }

        /// <summary>
        /// Наименование учебного заведения
        /// </summary>
        [StringLength(200)]
        public string SchoolName { get; set; }

        /// <summary>
        /// Страна выдачи ИДО	
        /// </summary>
        public long? DocCountryId { get; set; }

        /// <summary>
        /// Название ИДО	
        /// </summary>
        public long? DocumentTypeId { get; set; }

        /// <summary>
        /// Дата выдачи ИДО	
        /// </summary>
        [StringLength(200)]
        public string DocumentDate { get; set; }
        #endregion

        #region Выдача результата
        /// <summary>
        /// Форма получения оригиналов документов
        /// </summary>
        public long? ReturnOriginalsFormId { get; set; }

        public string ReturnOriginalsPostAddress { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            /*
            if ( string.IsNullOrWhiteSpace(CreatorSurname) && !IsCreatorSurnameAbsent )
                yield return new ValidationResult("Поле обязательно к заполнению", new[] { nameof(CreatorSurname) });

            if ( string.IsNullOrWhiteSpace(CreatorFirstName) && !IsCreatorFirstNameAbsent )
                yield return new ValidationResult("Поле обязательно к заполнению", new[] { nameof(CreatorFirstName) });
            */
            yield break;
        }
        #endregion
    }
}
