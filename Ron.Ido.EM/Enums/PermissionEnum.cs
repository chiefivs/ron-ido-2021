using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ron.Ido.EM.Enums
{
    public class PermissionGroup
    {
        public const string Admin = "Администрирование";
        public const string Access = "Доступ";
        public const string Applies = "Заявления";
        public const string Requests = "Запросы";
        public const string Expertizes = "Экспертизы";
        public const string Conclusions = "Экспертные заключения";
        public const string Letters = "Информационные письма";
        public const string AppliesRon = "Заявления в РОН";
        public const string Ofertas = "Заявления (оферта)";
        public const string Certificates = "Свидетельства";
        public const string Payments = "Платежи";
        public const string Dictions = "Справочники";
        public const string ApplyTemplatesAndDictions = "Заявления: шаблоны и справочники";
        public const string AdminDictions = "Администрирование: справочники";
        public const string ConclusionTemplatesAndDictions = "Экспертные заключения: шаблоны и справочники";
        public const string Acts = "Распорядительные акты";
        public const string LearnBase = "База знаний";
        public const string Analytics = "Аналитика";
        public const string LettersRegister = "Журналы входящих/исходящих писем";
        public const string Reports = "Отчеты";
        public const string Others = "Прочее";

        public static string[] List = new[]
        {
            Admin,
            Access,
            Applies,
            Requests,
            Expertizes,
            Conclusions,
            Letters,
            AppliesRon,
            Ofertas,
            Certificates,
            Payments,
            Dictions,
            ApplyTemplatesAndDictions,
            AdminDictions,
            ConclusionTemplatesAndDictions,
            Acts,
            LearnBase,
            Analytics,
            LettersRegister,
            Reports,
            Others
        };
    }

    public enum PermissionEnum
    {
        NULL = 0,

        [Permission("Системные настройки", PermissionGroup.Admin)]
        SETTINGS = 1101,
        [Permission("Просмотр журнала", PermissionGroup.Admin)]
        LOG_VIEW = 1102,
        [Permission("Очистка журнала", PermissionGroup.Admin, LOG_VIEW)]
        LOG_CLEAN = 1103,
        [Permission("Просмотр пользователей", PermissionGroup.Access)]
        USER_VIEW = 1201,
        [Permission("Создание пользователей", PermissionGroup.Access, USER_VIEW)]
        USER_CREATE = 1202,
        [Permission("Редактирование пользователей", PermissionGroup.Access, USER_VIEW)]
        USER_EDIT = 1203,
        [Permission("Удаление пользователей", PermissionGroup.Access, USER_EDIT)]
        USER_DEL = 1204,
        [Permission("Просмотр ролей", PermissionGroup.Access, 3)]
        ROLE_VIEW = 1301,
        [Permission("Создание ролей", PermissionGroup.Access, 2, ROLE_VIEW)]
        ROLE_CREATE = 1302,
        [Permission("Редактирование ролей", PermissionGroup.Access, ROLE_VIEW)]
        ROLE_EDIT = 1303,
        [Permission("Удаление ролей", PermissionGroup.Access, ROLE_EDIT)]
        ROLE_DEL = 1304,
        [Permission("Просмотр пользователей Личного кабинета", PermissionGroup.Access)]
        PORTAL_WORKROOM_USER_VIEW = 1401,

        [Permission("Просмотр", PermissionGroup.Applies)]
        APPLY_VIEW = 2101,
        [Permission("Создание", PermissionGroup.Applies)]
        APPLY_CREATE = 2102,
        [Permission("Создание архивного дела", PermissionGroup.Applies)]
        ARCHIVE_APPLY_CREATE = 2103,
        [Permission("Модерирование", PermissionGroup.Applies)]
        APPLY_MODERATE = 2104,
        [Permission("Редактирование", PermissionGroup.Applies)]
        APPLY_EDIT = 2105,
        [Permission("Удаление", PermissionGroup.Applies)]
        APPLY_DEL = 2106,
        [Permission("Изменение диапазона штрих-кода", PermissionGroup.Applies)]
        APPLY_CHANGE_CODE = 2107,
        [Permission("Просмотр комментариев", PermissionGroup.Applies)]
        APPLY_VIEW_COMMENTS = 2108,
        [Permission("Добавление комментариев", PermissionGroup.Applies)]
        APPLY_ADD_COMMENTS = 2109,
        [Permission("Редактирование комментариев", PermissionGroup.Applies)]
        APPLY_EDIT_COMMENTS = 2110,
        [Permission("Печать", PermissionGroup.Applies)]
        APPLY_PRINT = 2111,
        [Permission("Производство свидетельств", PermissionGroup.Applies)]
        APPLY_SERTIFICATE_CREATE = 2112,
        [Permission("Возврат в предыдущий статус", PermissionGroup.Applies)]
        APPLY_ROLLBACK_STATUS = 2113,
        [Permission("Исправление дел в актах", PermissionGroup.Applies)]
        APPLY_CORRECT = 2114,
        [Permission("Ручная отметка признака 'Оплачено'", PermissionGroup.Applies)]
        APPLY_SET_PAYMENT_CHECKBOX = 2115,
        [Permission("Фильтр 'Важные'", PermissionGroup.Applies)]
        APPLY_IMPORTANT_FILTER = 2116,

        [Permission("Просмотр", PermissionGroup.Requests)]
        REQUEST_VIEW = 2201,
        [Permission("Создание", PermissionGroup.Requests)]
        REQUEST_ADD = 2202,
        [Permission("Редактирование", PermissionGroup.Requests)]
        REQUEST_EDIT = 2203,
        [Permission("Удаление", PermissionGroup.Requests)]
        REQUEST_DEL = 2204,
        [Permission("Редактирование отправленных", PermissionGroup.Requests)]
        REQUEST_SENT_EDIT = 2205,
        [Permission("Подпись", PermissionGroup.Requests)]
        REQUEST_SIGN = 2206,

        [Permission("Назначение экспертизы", PermissionGroup.Expertizes)]
        EXPERTIZE_CREATE = 2301,
        [Permission("Работы по экспертизе", PermissionGroup.Expertizes)]
        EXPERTIZE_EDIT = 2302,
        [Permission("Удаление экспертизы", PermissionGroup.Expertizes)]
        EXPERTIZE_DEL = 2303,
        [Permission("Приемка экспертизы", PermissionGroup.Expertizes)]
        EXPERTIZE_ACCEPT = 2304,
        [Permission("Просмотр комментариев и файлов", PermissionGroup.Expertizes)]
        EXPERTIZE_VIEW_COMMENTS = 2305,
        [Permission("Добавление комментариев и файлов", PermissionGroup.Expertizes)]
        EXPERTIZE_ADD_COMMENTS = 2306,

        [Permission("Просмотр", PermissionGroup.Conclusions)]
        CONCLUSION_VIEW = 2401,
        [Permission("Создание", PermissionGroup.Conclusions)]
        CONCLUSION_CREATE = 2402,
        [Permission("Редактирование своих", PermissionGroup.Conclusions)]
        CONCLUSION_EDIT = 2403,
        [Permission("Удаление", PermissionGroup.Conclusions)]
        CONCLUSION_DEL = 2404,
        [Permission("Печать", PermissionGroup.Conclusions)]
        CONCLUSION_PRINT = 2405,
        [Permission("Редактирование всех", PermissionGroup.Conclusions)]
        CONCLUSION_EDIT_ALL = 2406,

        [Permission("Просмотр", "Информационные письма")]
        LETTER_VIEW = 2501,
        [Permission("Создание", "Информационные письма")]
        LETTER_CREATE = 2502,
        [Permission("Редактирование своих", "Информационные письма")]
        LETTER_EDIT = 2503,
        [Permission("Удаление", "Информационные письма")]
        LETTER_DEL = 2504,
        [Permission("Печать", "Информационные письма")]
        LETTER_PRINT = 2505,
        [Permission("Подпись", "Информационные письма")]
        LETTER_SIGN = 2506,
        [Permission("Доступ к делам с кодом '5'", "Информационные письма")]
        LETTER_DOSSIER = 2507,

        [Permission("Просмотр", PermissionGroup.AppliesRon)]
        APPLY_RON_VIEW = 2601,
        [Permission("Создание", PermissionGroup.AppliesRon)]
        APPLY_RON_CREATE = 2602,
        [Permission("Редактирование", PermissionGroup.AppliesRon)]
        APPLY_RON_EDIT = 2603,
        [Permission("Удаление", PermissionGroup.AppliesRon)]
        APPLY_RON_DELETE = 2604,
        [Permission("Просмотр", PermissionGroup.Ofertas)]
        OFERTA_RON_VIEW = 2701,

        [Permission("Визирование", PermissionGroup.Certificates)]
        CERTIFICATE_SIGNING = 3101,
        [Permission("Присвоение номера", PermissionGroup.Certificates)]
        CERTIFICATE_ASSIGN_REGNUM = 3102,
        [Permission("Назначение хранилища", PermissionGroup.Certificates)]
        CERTIFICATE_ASSIGN_STORAGE = 3103,
        [Permission("Выдача", PermissionGroup.Certificates)]
        CERTIFICATE_PUT = 3104,
        [Permission("Отправка", PermissionGroup.Certificates)]
        CERTIFICATE_SEND = 3105,
        [Permission("Печать", PermissionGroup.Certificates)]
        CERTIFICATE_PRINT = 3106,
        [Permission("Отправка почтой", PermissionGroup.Certificates)]
        CERTIFICATE_MAIL = 3107,

        [Permission("Просмотр реестра", PermissionGroup.Payments)]
        PAYMENTS_VIEW = 3201,
        [Permission("Импорт", PermissionGroup.Payments)]
        PAYMENTS_IMPORT = 3202,
        [Permission("Связывание с заявлением", PermissionGroup.Payments)]
        PAYMENTS_BINDING = 3203,

        [Permission("Создание элемента общего справочника", PermissionGroup.Dictions)]
        SHARED_DICTIONARY_ITEM_CREATE = 4101,
        [Permission("Редактирование общего элемента справочника", PermissionGroup.Dictions)]
        SHARED_DICTIONARY_ITEM_EDIT = 4102,
        [Permission("Удаление элемента общего справочника", PermissionGroup.Dictions)]
        SHARED_DICTIONARY_ITEM_DEL = 4103,
        [Permission("Системные справочники", PermissionGroup.Dictions)]
        SYSTEN_DICTIONARY_ALL = 4104,

        [Permission("Создание шаблона реестра", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_REESTR_TEMPLATE_CREATE = 4201,
        [Permission("Редактирование шаблона реестра", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_REESTR_TEMPLATE_EDIT = 4202,
        [Permission("Удаление шаблона реестра", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_REESTR_TEMPLATE_DEL = 4203,
        [Permission("Редактирование шаблона заявления", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_TEMPLATE_EDIT = 4204,
        [Permission("Создание элементов справочников", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_DICTIONARY_ITEM_CREATE = 4205,
        [Permission("Редактирование элементов справочников", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_DICTIONARY_ITEM_EDIT = 4206,
        [Permission("Удаление элементов справочников", PermissionGroup.ApplyTemplatesAndDictions)]
        APPLY_DICTIONARY_ITEM_DEL = 4207,

        [Permission("Создание элементов справочников", PermissionGroup.AdminDictions)]
        EXPERTIZE_DICTIONARY_ITEM_CREATE = 4307,
        [Permission("Редактирование элементов справочников", PermissionGroup.AdminDictions)]
        EXPERTIZE_DICTIONARY_ITEM_EDIT = 4308,
        [Permission("Удаление элементов справочников", PermissionGroup.AdminDictions)]
        EXPERTIZE_DICTIONARY_ITEM_DEL = 4309,

        [Permission("Редактирование шаблона заключения", PermissionGroup.ConclusionTemplatesAndDictions)]
        CONCLUSION_TEMPLATE_EDIT = 4401,
        [Permission("Создание элемента справочника", PermissionGroup.ConclusionTemplatesAndDictions)]
        CONCLUSION_DICTIONARY_ITEM_CREATE = 4402,
        [Permission("Редактирование элемента справочника", PermissionGroup.ConclusionTemplatesAndDictions)]
        CONCLUSION_DICTIONARY_ITEM_EDIT = 4403,
        [Permission("Удаление элемента справочника", PermissionGroup.ConclusionTemplatesAndDictions)]
        CONCLUSION_DICTIONARY_ITEM_DEL = 4404,

        [Permission("Работа с актами в ГЭЦ", PermissionGroup.Acts)]
        ACTS_FOR_GEC = 4501,
        [Permission("Работа с актами в РОН", PermissionGroup.Acts)]
        ACTS_FOR_RON = 4502,
        [Permission("Согласование актов в ГЭЦ", PermissionGroup.Acts)]
        VISA_FOR_GEC = 4503,
        [Permission("Визирование актов в РОН", PermissionGroup.Acts)]
        VISA_FOR_RON = 4504,
        [Permission("Создание электронных свидетельста", PermissionGroup.Acts)]
        ACTS_DIG_SVID = 4505,
        [Permission("Печать свидетельств", PermissionGroup.Acts)]
        PRINT_SERTIF = 4506,
        [Permission("Удаление акта", PermissionGroup.Acts)]
        ACT_DEL = 4507,
        [Permission("Удаление дела из акта", PermissionGroup.Acts)]
        ACT_DEL_CASE = 4508,

        [Permission("Просмотр документов", PermissionGroup.LearnBase)]
        LEARNBASE_VIEW = 5101,
        [Permission("Создание документов", PermissionGroup.LearnBase)]
        LEARNBASE_ADD = 5102,
        [Permission("Редактирование документов", PermissionGroup.LearnBase)]
        LEARNBASE_EDIT = 5103,
        [Permission("Удаление документов", PermissionGroup.LearnBase)]
        LEARNBASE_DEL = 5104,
        [Permission("Просмотр прецедентов", PermissionGroup.LearnBase)]
        PRECEDENTS_VIEW = 5105,

        [Permission("Калькулятор оценок", PermissionGroup.Analytics)]
        CALCULATOR_VIEW = 5201,
        [Permission("В Росздрав", PermissionGroup.Analytics)]
        TO_MINZDRAV = 5202,

        [Permission("Просмотр журнала входящих писем", PermissionGroup.LettersRegister)]
        CORR_INPUT_VIEW = 5301,
        [Permission("Редактирование журнала входящих писем", PermissionGroup.LettersRegister)]
        CORR_INPUT_EDIT = 5302,
        [Permission("Просмотр журнала исходящих писем", PermissionGroup.LettersRegister)]
        CORR_OUTPUT_VIEW = 5303,
        [Permission("Редактирование журнала исходящих писем", PermissionGroup.LettersRegister)]
        CORR_OUTPUT_EDIT = 5304,
        [Permission("Исполнитель", PermissionGroup.LettersRegister)]
        CORR_PERFORMER = 5305,
        [Permission("Перевод в статус 'завизировано'", PermissionGroup.LettersRegister)]
        CORR_OUTPUT_SET_SIGNED = 5306,

        [Permission("Отчеты для науки", PermissionGroup.Reports)]
        REPORTS_SCIENCE = 6101,
        [Permission("Отчеты для РОН", PermissionGroup.Reports)]
        REPORTS_RON = 6102,
        [Permission("Отчеты для экспертов", PermissionGroup.Reports)]
        REPORTS_EXPERT = 6103,
        [Permission("Отчеты для приема и выдачи документов", PermissionGroup.Reports)]
        REPORTS_OPERATOR = 6104,
        [Permission("Отчеты для руководства", PermissionGroup.Reports)]
        REPORTS_CHIEF = 6105,
        [Permission("Прочие отчеты", PermissionGroup.Reports)]
        REPORTS_OTHER = 6106,
        [Permission("Оперативные отчеты для РОН", PermissionGroup.Reports)]
        REPORTS_OPER_RON = 6107,

        [Permission("Доступ к ЛК", PermissionGroup.Others)]
        CHIEFROOM = 7101,
        [Permission("Просмотр журнала дежурств", PermissionGroup.Others)]
        DUTY_VIEW = 7102,
        [Permission("Редактирование журнала дежурств", PermissionGroup.Others)]
        DUTY_EDIT = 7103,
        [Permission("Просмотр журнала записи на прием к руководству", PermissionGroup.Others)]
        CHIEF_JOURNAL = 7104
    }

    public class PermissionData
    {
        public PermissionEnum Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int OrderNum { get; set; }
        public IEnumerable<PermissionEnum> Included { get; set; }

        private static readonly object _listLock = new object();
        private static PermissionData[] _list;

        public static IEnumerable<PermissionData> List
        {
            get
            {
                if (_list == null)
                {
                    lock (_listLock)
                    {
                        if (_list == null)
                        {
                            var type = typeof(PermissionEnum);
                            var groups = type.GetFields()
                                .Select(field => new { field, attr = field.GetCustomAttribute<PermissionAttribute>() })
                                .Where(i => i.attr != null)
                                .GroupBy(i => i.attr.GroupName);

                            var joined = PermissionGroup.List
                                .Join(groups,
                                    gname => gname,
                                    group => group.Key,
                                    (gname, group) => group.OrderBy(gi => gi.attr.OrderNum));

                            _list = joined
                                .SelectMany(group => group
                                    .OrderBy(item => item.attr.OrderNum)
                                    .Select(item => new PermissionData
                                    {
                                        Id = (PermissionEnum)item.field.GetValue(null),
                                        Name = item.attr.Name,
                                        GroupName = item.attr.GroupName,
                                        OrderNum = item.attr.OrderNum,
                                        Included = item.attr.Included
                                    }))
                                .ToArray();
                        }
                    }
                }

                return _list;
            }
        }
    }

    public class PermissionAttribute : Attribute
    {
        public string Name;
        public string GroupName;
        public int OrderNum;
        public IEnumerable<PermissionEnum> Included;

        public PermissionAttribute(string name, string groupname, int ordernum, params PermissionEnum[] included)
        {
            Name = name;
            GroupName = groupname;
            OrderNum = ordernum;
            Included = included;
        }

        public PermissionAttribute(string name, string groupname, params PermissionEnum[] included) : this(name, groupname, 0, included)
        {

        }
    }
}
