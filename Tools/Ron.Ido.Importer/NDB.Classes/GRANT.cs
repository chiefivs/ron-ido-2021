using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.Importer.NDB.Classes
{
    public static class GRANT
    {
        #region Константы
        //==============================================================================
        //  Админ. интерфейс
        //==============================================================================
        [GrantDetails("Системные настройки", "Администрирование", 1, PermissionEnum.SETTINGS)]
        public const string SETTINGS = "000000000000000000000001";
        [GrantDetails("Просмотр журнала", "Администрирование", 2, PermissionEnum.LOG_VIEW)]
        public const string LOG_VIEW = "000000000000000000000002";
        [GrantDetails("Очистка журнала", "Администрирование", 3, PermissionEnum.LOG_CLEAN)]
        public const string LOG_CLEAN = "000000000000000000000004";
        //             const                                          = "000000000000000000000008";
        [GrantDetails("Просмотр пользователей", "Доступ", 1, PermissionEnum.USER_VIEW)]
        public const string USER_VIEW = "000000000000000000000010";
        [GrantDetails("Создание пользователей", "Доступ", 2, PermissionEnum.USER_CREATE)]
        public const string USER_CREATE = "000000000000000000000020";
        [GrantDetails("Редактирование пользователей", "Доступ", 3, PermissionEnum.USER_EDIT)]
        public const string USER_EDIT = "000000000000000000000040";
        [GrantDetails("Удаление пользователей", "Доступ", 4, PermissionEnum.USER_DEL)]
        public const string USER_DEL = "000000000000000000000080";

        [GrantDetails("Просмотр ролей", "Доступ", 1, PermissionEnum.ROLE_VIEW)]
        public const string ROLE_VIEW = "000000000000000000000100";
        [GrantDetails("Создание ролей", "Доступ", 2, PermissionEnum.ROLE_CREATE)]
        public const string ROLE_CREATE = "000000000000000000000200";
        [GrantDetails("Редактирование ролей", "Доступ", 3, PermissionEnum.ROLE_EDIT)]
        public const string ROLE_EDIT = "000000000000000000000400";
        [GrantDetails("Удаление ролей", "Доступ", 4, PermissionEnum.ROLE_DEL)]
        public const string ROLE_DEL = "000000000000000000000800";

        [GrantDetails("Просмотр пользователей Личного кабинета", "Доступ", 1, PermissionEnum.PORTAL_WORKROOM_USER_VIEW)]
        public const string PORTAL_WORKROOM_USER_VIEW = "000000000000000000001000";
        //             const                                          = "000000000000000000004000";
        //             const                                          = "000000000000000000008000";
        //==============================================================================
        //  Заявление
        //==============================================================================
        [GrantDetails("Просмотр", "Заявление", 1, PermissionEnum.APPLY_VIEW)]
        public const string APPLY_VIEW = "000000000000000000010000";
        [GrantDetails("Создание", "Заявление", 2, PermissionEnum.APPLY_CREATE)]
        public const string APPLY_CREATE = "000000000000000000020000";
        [GrantDetails("Создание архивного дела", "Заявление", 2, PermissionEnum.ARCHIVE_APPLY_CREATE)]
        public const string ARCHIVE_APPLY_CREATE = "000000000000000004000000";
        [GrantDetails("Модерирование", "Заявление", 3, PermissionEnum.APPLY_MODERATE)]
        public const string APPLY_MODERATE = "000000000000000000040000";
        [GrantDetails("Редактирование", "Заявление", 4, PermissionEnum.APPLY_EDIT)]
        public const string APPLY_EDIT = "000000000000000000080000";
        [GrantDetails("Удаление", "Заявление", 5, PermissionEnum.APPLY_DEL)]
        public const string APPLY_DEL = "000000000000000000100000";
        [GrantDetails("Изменение диапазона штрих-кода", "Заявление", 5, PermissionEnum.APPLY_CHANGE_CODE)]
        public const string APPLY_CHANGE_CODE = "0800000000000000000000000000000";
        [GrantDetails("Просмотр комментариев", "Заявление", 6, PermissionEnum.APPLY_VIEW_COMMENTS)]
        public const string APPLY_VIEW_COMMENTS = "000000000000000000200000";
        [GrantDetails("Добавление комментариев", "Заявление", 7, PermissionEnum.APPLY_ADD_COMMENTS)]
        public const string APPLY_ADD_COMMENTS = "000000000000000000400000";
        [GrantDetails("Редактирование комментариев", "Заявление", 8, PermissionEnum.APPLY_EDIT_COMMENTS)]
        public const string APPLY_EDIT_COMMENTS = "000000000000000008000000";
        [GrantDetails("Печать", "Заявление", 9, PermissionEnum.APPLY_PRINT)]
        public const string APPLY_PRINT = "000000000000000000800000";
        [GrantDetails("Производство свидетельств", "Заявление", 10, PermissionEnum.APPLY_SERTIFICATE_CREATE)]
        public const string APPLY_SERTIFICATE_CREATE = "000000000000000001000000";
        [GrantDetails("Возврат в предыдущий статус", "Заявление", 11, PermissionEnum.APPLY_ROLLBACK_STATUS)]
        public const string APPLY_ROLLBACK_STATUS = "000000000000000002000000";
        [GrantDetails("Исправление дел в актах", "Заявление", 12, PermissionEnum.APPLY_CORRECT)]
        public const string APPLY_CORRECT = "000000000000040000000000";
        [GrantDetails("Ручная отметка признака 'Оплачено'", "Заявление", 13, PermissionEnum.APPLY_SET_PAYMENT_CHECKBOX)]
        public const string APPLY_SET_PAYMENT_CHECKBOX = "000000000000000000002000";
        [GrantDetails("Фильтр 'Важные'", "Заявление", 14, PermissionEnum.APPLY_IMPORTANT_FILTER)]
        public const string APPLY_IMPORTANT_FILTER = "1000000000000000000000000000000";
        public const string APPLY_REESTR = "00000000000004000FFF0000";


        //==============================================================================
        //  Запросы
        //==============================================================================
        [GrantDetails("Просмотр", "Запросы", 1, PermissionEnum.REQUEST_VIEW)]
        public const string REQUEST_VIEW = "000000000000000010000000";
        [GrantDetails("Создание", "Запросы", 2, PermissionEnum.REQUEST_ADD)]
        public const string REQUEST_ADD = "000000000000000020000000";
        [GrantDetails("Редактирование", "Запросы", 3, PermissionEnum.REQUEST_EDIT)]
        public const string REQUEST_EDIT = "000000000000000040000000";
        [GrantDetails("Удаление", "Запросы", 5, PermissionEnum.REQUEST_DEL)]
        public const string REQUEST_DEL = "000000000000000080000000";
        [GrantDetails("Редактирование отправленных", "Запросы", 4, PermissionEnum.REQUEST_SENT_EDIT)]
        public const string REQUEST_SENT_EDIT = "000000000000000100000000";
        [GrantDetails("Подпись", "Запросы", 6, PermissionEnum.REQUEST_SIGN)]
        public const string REQUEST_SIGN = "000000000000000200000000";
        //==============================================================================
        //  Экспертизы
        //==============================================================================
        [GrantDetails("Назначение экспертизы", "Экспертизы", 1, PermissionEnum.EXPERTIZE_CREATE)]
        public const string EXPERTIZE_CREATE = "000000000000001000000000";
        [GrantDetails("Работы по экспертизе", "Экспертизы", 3, PermissionEnum.EXPERTIZE_EDIT)]
        public const string EXPERTIZE_EDIT = "000000000000002000000000";
        // Есть удаление заявления
        [GrantDetails("Удаление экспертизы", "Экспертизы", 4, PermissionEnum.EXPERTIZE_DEL)]
        public const string EXPERTIZE_DEL = "000000000000004000000000";
        [GrantDetails("Приемка экспертизы", "Экспертизы", 2, PermissionEnum.EXPERTIZE_ACCEPT)]
        public const string EXPERTIZE_ACCEPT = "000000000000008000000000";

        [GrantDetails("Просмотр комментариев и файлов", "Экспертизы", 5, PermissionEnum.EXPERTIZE_VIEW_COMMENTS)]
        public const string EXPERTIZE_VIEW_COMMENTS = "000000000000010000000000";
        [GrantDetails("Добавление комментариев и файлов", "Экспертизы", 6, PermissionEnum.EXPERTIZE_ADD_COMMENTS)]
        public const string EXPERTIZE_ADD_COMMENTS = "000000000000020000000000";
        //             const                                          = "000000000000080000000000";
        //==============================================================================
        //  Экспертные заключения
        //==============================================================================
        [GrantDetails("Просмотр", "Экспертные заключения", 1, PermissionEnum.CONCLUSION_VIEW)]
        public const string CONCLUSION_VIEW = "000000000000100000000000";
        [GrantDetails("Создание", "Экспертные заключения", 2, PermissionEnum.CONCLUSION_CREATE)]
        public const string CONCLUSION_CREATE = "000000000000200000000000";
        [GrantDetails("Редактирование своих", "Экспертные заключения", 3, PermissionEnum.CONCLUSION_EDIT)]
        public const string CONCLUSION_EDIT = "000000000000400000000000";
        [GrantDetails("Удаление", "Экспертные заключения", 5, PermissionEnum.CONCLUSION_DEL)]
        public const string CONCLUSION_DEL = "000000000000800000000000";
        [GrantDetails("Печать", "Экспертные заключения", 6, PermissionEnum.CONCLUSION_PRINT)]
        public const string CONCLUSION_PRINT = "000000000001000000000000";
        [GrantDetails("Редактирование всех", "Экспертные заключения", 4, PermissionEnum.CONCLUSION_EDIT_ALL)]
        public const string CONCLUSION_EDIT_ALL = "000000000002000000000000";
        //              const                                         = "000000000004000000000000";
        //              const                                         = "000000000008000000000000";
        //==============================================================================
        //  Информационные письма
        //==============================================================================
        [GrantDetails("Просмотр", "Информационные письма", 1, PermissionEnum.LETTER_VIEW)]
        public const string LETTER_VIEW = "10000000000000000000000000";
        [GrantDetails("Создание", "Информационные письма", 2, PermissionEnum.LETTER_CREATE)]
        public const string LETTER_CREATE = "20000000000000000000000000";
        [GrantDetails("Редактирование своих", "Информационные письма", 3, PermissionEnum.LETTER_EDIT)]
        public const string LETTER_EDIT = "40000000000000000000000000";
        [GrantDetails("Удаление", "Информационные письма", 5, PermissionEnum.LETTER_DEL)]
        public const string LETTER_DEL = "80000000000000000000000000";
        [GrantDetails("Печать", "Информационные письма", 6, PermissionEnum.LETTER_PRINT)]
        public const string LETTER_PRINT = "100000000000000000000000000";
        [GrantDetails("Подпись", "Информационные письма", 4, PermissionEnum.LETTER_SIGN)]
        public const string LETTER_SIGN = "200000000000000000000000000";
        [GrantDetails("Доступ к делам с кодом '5'", "Информационные письма", 7, PermissionEnum.LETTER_DOSSIER)]
        public const string LETTER_DOSSIER = "400000000000000000000000000";
        //              const                              = "800000000000000000000000000";
        public const string LETTER_ALL = "7F0000000000000000000000000";
        //==============================================================================
        // Заявления в РОН
        //==============================================================================
        [GrantDetails("Просмотр", "Заявления в РОН", 1, PermissionEnum.APPLY_RON_VIEW)]
        public const string APPLY_RON_VIEW = "01000000000000000000000000000000000";
        [GrantDetails("Создание", "Заявления в РОН", 2, PermissionEnum.APPLY_RON_CREATE)]
        public const string APPLY_RON_CREATE = "02000000000000000000000000000000000";
        [GrantDetails("Редактирование", "Заявления в РОН", 3, PermissionEnum.APPLY_RON_EDIT)]
        public const string APPLY_RON_EDIT = "04000000000000000000000000000000000";
        [GrantDetails("Удаление", "Заявления в РОН", 4, PermissionEnum.APPLY_RON_DELETE)]
        public const string APPLY_RON_DELETE = "08000000000000000000000000000000000";
        public const string APPLY_RON_ALL = "FF000000000000000000000000000000000";
        //     const                               = "10000000000000000000000000000000000";
        //     const                               = "20000000000000000000000000000000000";
        //     const                               = "40000000000000000000000000000000000";
        //     const                               = "80000000000000000000000000000000000";
        //==============================================================================
        // Заявления по оферте
        //==============================================================================
        [GrantDetails("Просмотр", "Заявления (оферта)", 1, PermissionEnum.OFERTA_RON_VIEW)]
        public const string OFERTA_RON_VIEW = "0100000000000000000000000000000000000";
        //     const                             = "0200000000000000000000000000000000000";
        //     const                             = "0400000000000000000000000000000000000";
        //     const                             = "0800000000000000000000000000000000000";
        //     const                             = "1000000000000000000000000000000000000";
        //     const                             = "2000000000000000000000000000000000000";
        //     const                             = "4000000000000000000000000000000000000";
        //     const                             = "8000000000000000000000000000000000000";

        //==============================================================================
        // производство свидетельства (визирование)
        //==============================================================================
        [GrantDetails("Визирование", "Свидетельство", 1, PermissionEnum.CERTIFICATE_SIGNING)]
        public const string CERTIFICATE_SIGNING = "000000000010000000000000";
        [GrantDetails("Присвоение номера", "Свидетельство", 3, PermissionEnum.CERTIFICATE_ASSIGN_REGNUM)]
        public const string CERTIFICATE_ASSIGN_REGNUM = "000000000020000000000000";
        [GrantDetails("Назначение хранилища", "Свидетельство", 4, PermissionEnum.CERTIFICATE_ASSIGN_STORAGE)]
        public const string CERTIFICATE_ASSIGN_STORAGE = "000000000040000000000000";
        [GrantDetails("Выдача", "Свидетельство", 5, PermissionEnum.CERTIFICATE_PUT)]
        public const string CERTIFICATE_PUT = "000000000080000000000000";
        [GrantDetails("Отправка", "Свидетельство", 6, PermissionEnum.CERTIFICATE_SEND)]
        public const string CERTIFICATE_SEND = "000000000100000000000000";
        [GrantDetails("Печать", "Свидетельство", 2, PermissionEnum.CERTIFICATE_PRINT)]
        public const string CERTIFICATE_PRINT = "000000000200000000000000";
        [GrantDetails("Отправка почтой", "Свидетельство", 7, PermissionEnum.CERTIFICATE_MAIL)]
        public const string CERTIFICATE_MAIL = "000000000400000000000000";
        //              const                                         = "000000000800000000000000";
        //==============================================================================
        // Платежи
        //==============================================================================
        [GrantDetails("Просмотр реестра", "Платежи", 1, PermissionEnum.PAYMENTS_VIEW)]
        public const string PAYMENTS_VIEW = "000000001000000000000000";
        [GrantDetails("Импорт", "Платежи", 2, PermissionEnum.PAYMENTS_IMPORT)]
        public const string PAYMENTS_IMPORT = "000000002000000000000000";
        [GrantDetails("Связывание с заявлением", "Платежи", 3, PermissionEnum.PAYMENTS_BINDING)]
        public const string PAYMENTS_BINDING = "000000004000000000000000";
        //              const                                         = "000000008000000000000000";

        //==============================================================================
        //  Админ. интерфейс, Справочники
        //==============================================================================
        [GrantDetails("Создание элемента общего справочника", "Справочники", 1, PermissionEnum.SHARED_DICTIONARY_ITEM_CREATE)]
        public const string SHARED_DICTIONARY_ITEM_CREATE = "000000010000000000000000";
        [GrantDetails("Редактирование общего элемента справочника", "Справочники", 2, PermissionEnum.SHARED_DICTIONARY_ITEM_EDIT)]
        public const string SHARED_DICTIONARY_ITEM_EDIT = "000000020000000000000000";
        [GrantDetails("Удаление элемента общего справочника", "Справочники", 3, PermissionEnum.SHARED_DICTIONARY_ITEM_DEL)]
        public const string SHARED_DICTIONARY_ITEM_DEL = "000000040000000000000000";
        [GrantDetails("Системные справочники", "Справочники", 4)]
        public const string SYSTEN_DICTIONARY_ALL = "000000080000000000000000";
        public const string SHARED_DICTIONARY_ALL = "0000000F0000000000000000";

        //==============================================================================
        //  Админ. интерфейс, реестр заявлений, шаблон заявления
        //==============================================================================
        [GrantDetails("Создание шаблона реестра", "Заявления: шаблоны и справочники", 1, PermissionEnum.APPLY_REESTR_TEMPLATE_CREATE)]
        public const string APPLY_REESTR_TEMPLATE_CREATE = "000000100000000000000000";
        [GrantDetails("Редактирование шаблона реестра", "Заявления: шаблоны и справочники", 2, PermissionEnum.APPLY_REESTR_TEMPLATE_EDIT)]
        public const string APPLY_REESTR_TEMPLATE_EDIT = "000000200000000000000000";
        [GrantDetails("Удаление шаблона реестра", "Заявления: шаблоны и справочники", 3, PermissionEnum.APPLY_REESTR_TEMPLATE_DEL)]
        public const string APPLY_REESTR_TEMPLATE_DEL = "000000400000000000000000";
        [GrantDetails("Редактирование шаблона заявления", "Заявления: шаблоны и справочники", 4, PermissionEnum.APPLY_TEMPLATE_EDIT)]
        public const string APPLY_TEMPLATE_EDIT = "000000800000000000000000";
        public const string APPLY_REESTR_TEMPLATE_ALL = "000000F00000000000000000";

        [GrantDetails("Создание элементов справочников", "Заявления: шаблоны и справочники", 5, PermissionEnum.APPLY_DICTIONARY_ITEM_CREATE)]
        public const string APPLY_DICTIONARY_ITEM_CREATE = "000001000000000000000000";
        [GrantDetails("Редактирование элементов справочников", "Заявления: шаблоны и справочники", 6, PermissionEnum.APPLY_DICTIONARY_ITEM_EDIT)]
        public const string APPLY_DICTIONARY_ITEM_EDIT = "000002000000000000000000";
        [GrantDetails("Удаление элементов справочников", "Заявления: шаблоны и справочники", 7, PermissionEnum.APPLY_DICTIONARY_ITEM_DEL)]
        public const string APPLY_DICTIONARY_ITEM_DEL = "000004000000000000000000";
        //             const                                          = "000008000000000000000000";

        [GrantDetails("Создание элементов справочников", "Администрирование: справочники", 5, PermissionEnum.EXPERTIZE_DICTIONARY_ITEM_CREATE)]
        public const string EXPERTIZE_DICTIONARY_ITEM_CREATE = "000010000000000000000000";
        [GrantDetails("Редактирование элементов справочников", "Администрирование: справочники", 6, PermissionEnum.EXPERTIZE_DICTIONARY_ITEM_EDIT)]
        public const string EXPERTIZE_DICTIONARY_ITEM_EDIT = "000020000000000000000000";
        [GrantDetails("Удаление элементов справочников", "Администрирование: справочники", 7, PermissionEnum.EXPERTIZE_DICTIONARY_ITEM_DEL)]
        public const string EXPERTIZE_DICTIONARY_ITEM_DEL = "000040000000000000000000";
        //             const                                          = "000080000000000000000000";

        [GrantDetails("Редактирование шаблона заключения", "Экспертные заключения: шаблоны и справочники", 4, PermissionEnum.CONCLUSION_TEMPLATE_EDIT)]
        public const string CONCLUSION_TEMPLATE_EDIT = "000100000000000000000000";
        [GrantDetails("Создание элемента справочника", "Экспертные заключения: шаблоны и справочники", 5, PermissionEnum.CONCLUSION_DICTIONARY_ITEM_CREATE)]
        public const string CONCLUSION_DICTIONARY_ITEM_CREATE = "000200000000000000000000";
        [GrantDetails("Редактирование элемента справочника", "Экспертные заключения: шаблоны и справочники", 6, PermissionEnum.CONCLUSION_DICTIONARY_ITEM_EDIT)]
        public const string CONCLUSION_DICTIONARY_ITEM_EDIT = "000400000000000000000000";
        [GrantDetails("Удаление элемента справочника", "Экспертные заключения: шаблоны и справочники", 7, PermissionEnum.CONCLUSION_DICTIONARY_ITEM_DEL)]
        public const string CONCLUSION_DICTIONARY_ITEM_DEL = "000800000000000000000000";
        //==============================================================================
        //  Аналитика
        //==============================================================================
        [GrantDetails("Просмотр документов", "База знаний", 1, PermissionEnum.LEARNBASE_VIEW)]
        public const string LEARNBASE_VIEW = "001000000000000000000000";
        [GrantDetails("Создание документов", "База знаний", 2, PermissionEnum.LEARNBASE_ADD)]
        public const string LEARNBASE_ADD = "002000000000000000000000";
        [GrantDetails("Редактирование документов", "База знаний", 3, PermissionEnum.LEARNBASE_EDIT)]
        public const string LEARNBASE_EDIT = "004000000000000000000000";
        [GrantDetails("Удаление документов", "База знаний", 4, PermissionEnum.LEARNBASE_DEL)]
        public const string LEARNBASE_DEL = "008000000000000000000000";
        public const string LEARNBASE_ALL = "00F000000000000000000000";
        [GrantDetails("Просмотр прецедентов", "База знаний", 5, PermissionEnum.PRECEDENTS_VIEW)]
        public const string PRECEDENTS_VIEW = "020000000000000000000000";
        [GrantDetails("Калькулятор оценок", "Аналитика", 6, PermissionEnum.CALCULATOR_VIEW)]
        public const string CALCULATOR_VIEW = "010000000000000000000000";
        [GrantDetails("В Росздрав", "Аналитика", 7, PermissionEnum.TO_MINZDRAV)]
        public const string TO_MINZDRAV = "040000000000000000000000";
        //[GrantDetails("", "Аналитика", 6)]
        //public const string TO_MINZDRAV                     = "080000000000000000000000";

        //==============================================================================
        //  Распорядительные акты
        //==============================================================================
        [GrantDetails("Работа с актами в ГЭЦ", "Распорядительные акты", 1, PermissionEnum.ACTS_FOR_GEC)]
        public const string ACTS_FOR_GEC = "200000000000000000000000";
        [GrantDetails("Работа с актами в РОН", "Распорядительные акты", 2, PermissionEnum.ACTS_FOR_RON)]
        public const string ACTS_FOR_RON = "400000000000000000000000";
        [GrantDetails("Согласование актов в ГЭЦ", "Распорядительные акты", 3, PermissionEnum.VISA_FOR_GEC)]
        public const string VISA_FOR_GEC = "800000000000000000000000";
        [GrantDetails("Визирование актов в РОН", "Распорядительные акты", 4, PermissionEnum.VISA_FOR_RON)]
        public const string VISA_FOR_RON = "1000000000000000000000000";
        [GrantDetails("Создание электронных свидетельста", "Распорядительные акты", 5, PermissionEnum.ACTS_DIG_SVID)]
        public const string ACTS_DIG_SVID = "10000000000000000000000000000000000000";
        [GrantDetails("Печать свидетельств", "Распорядительные акты", 6, PermissionEnum.PRINT_SERTIF)]
        public const string PRINT_SERTIF = "2000000000000000000000000";
        /// <summary>
        /// Удаление акта
        /// </summary>
        [GrantDetails("Удаление акта", "Распорядительные акты", 5, PermissionEnum.ACT_DEL)]
        public const string ACT_DEL = "4000000000000000000000000";
        /// <summary>
        /// Удаление дела из акта
        /// </summary>
        [GrantDetails("Удаление дела из акта", "Распорядительные акты", 5, PermissionEnum.ACT_DEL_CASE)]
        public const string ACT_DEL_CASE = "8000000000000000000000000";
        //     const                            = "20000000000000000000000000000000000000";
        //     const                            = "40000000000000000000000000000000000000";
        //     const                            = "80000000000000000000000000000000000000";

        //==============================================================================
        //  Журналы входящих/исходящих писем
        //==============================================================================
        /// <summary>
        /// Просмотр журнала входящих писем
        /// </summary>
        [GrantDetails("Просмотр журнала входящих писем", "Журналы входящих/исходящих писем", 1, PermissionEnum.CORR_INPUT_VIEW)]
        public const string CORR_INPUT_VIEW = "010000000000000000000000000000000";
        /// <summary>
        /// Редактирование журнала входящих писем
        /// </summary>
        [GrantDetails("Редактирование журнала входящих писем", "Журналы входящих/исходящих писем", 2, PermissionEnum.CORR_INPUT_EDIT)]
        public const string CORR_INPUT_EDIT = "020000000000000000000000000000000";
        /// <summary>
        /// Просмотр журнала исходящих писем
        /// </summary>
        [GrantDetails("Просмотр журнала исходящих писем", "Журналы входящих/исходящих писем", 3, PermissionEnum.CORR_OUTPUT_VIEW)]
        public const string CORR_OUTPUT_VIEW = "040000000000000000000000000000000";
        /// <summary>
        /// Редактирование журнала исходящих писем
        /// </summary>
        [GrantDetails("Редактирование журнала исходящих писем", "Журналы входящих/исходящих писем", 4, PermissionEnum.CORR_OUTPUT_EDIT)]
        public const string CORR_OUTPUT_EDIT = "080000000000000000000000000000000";
        /// <summary>
        /// Исполнитель
        /// </summary>
        [GrantDetails("Исполнитель", "Журналы входящих/исходящих писем", 5, PermissionEnum.CORR_PERFORMER)]
        public const string CORR_PERFORMER = "100000000000000000000000000000000";
        /// <summary>
        /// Исполнитель
        /// </summary>
        [GrantDetails("Перевод в статус 'завизировано'", "Журналы входящих/исходящих писем", 6, PermissionEnum.CORR_OUTPUT_SET_SIGNED)]
        public const string CORR_OUTPUT_SET_SIGNED = "200000000000000000000000000000000";
        //public const string SET1                   = "400000000000000000000000000000000";
        //public const string SET1                   = "800000000000000000000000000000000";

        //==============================================================================
        //  Отчеты
        //==============================================================================
        [GrantDetails("Отчеты для науки", "Отчеты", 1, PermissionEnum.REPORTS_SCIENCE)]
        public const string REPORTS_SCIENCE = "01000000000000000000000000000";
        [GrantDetails("Отчеты для РОН", "Отчеты", 2, PermissionEnum.REPORTS_RON)]
        public const string REPORTS_RON = "02000000000000000000000000000";
        [GrantDetails("Отчеты для экспертов", "Отчеты", 3, PermissionEnum.REPORTS_EXPERT)]
        public const string REPORTS_EXPERT = "04000000000000000000000000000";
        [GrantDetails("Отчеты для приема и выдачи документов", "Отчеты", 4, PermissionEnum.REPORTS_OPERATOR)]
        public const string REPORTS_OPERATOR = "08000000000000000000000000000";
        [GrantDetails("Отчеты для руководства", "Отчеты", 5, PermissionEnum.REPORTS_CHIEF)]
        public const string REPORTS_CHIEF = "10000000000000000000000000000";
        [GrantDetails("Прочие отчеты", "Отчеты", 7, PermissionEnum.REPORTS_OTHER)]
        public const string REPORTS_OTHER = "20000000000000000000000000000";
        [GrantDetails("Оперативные отчеты для РОН", "Отчеты", 6, PermissionEnum.REPORTS_OPER_RON)]
        public const string REPORTS_OPER_RON = "40000000000000000000000000000";
        //             const                             = "80000000000000000000000000000";
        //==============================================================================
        //  Прочее
        //==============================================================================
        [GrantDetails("Доступ к ЛК", "Прочее", 1, PermissionEnum.CHIEFROOM)]
        public const string CHIEFROOM = "100000000000000000000000";
        [GrantDetails("Просмотр журнала дежурств", "Прочее", 2, PermissionEnum.DUTY_VIEW)]
        public const string DUTY_VIEW = "0100000000000000000000000000000";
        [GrantDetails("Редактирование журнала дежурств", "Прочее", 3, PermissionEnum.DUTY_EDIT)]
        public const string DUTY_EDIT = "0200000000000000000000000000000";
        [GrantDetails("Просмотр журнала записи на прием к руководству", "Прочее", 4, PermissionEnum.CHIEF_JOURNAL)]
        public const string CHIEF_JOURNAL = "0400000000000000000000000000000";

        #endregion
    }
}
