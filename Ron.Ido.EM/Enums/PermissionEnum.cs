using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ron.Ido.EM.Enums
{
    public enum PermissionEnum
    {
        NULL = 0,
        SETTINGS = 1101,
        LOG_VIEW = 1102,
        LOG_CLEAN = 1103,
        USER_VIEW = 1201,
        USER_CREATE = 1202,
        USER_EDIT = 1203,
        USER_DEL = 1204,
        ROLE_VIEW = 1301,
        ROLE_CREATE = 1302,
        ROLE_EDIT = 1303,
        ROLE_DEL = 1304,
        PORTAL_WORKROOM_USER_VIEW = 1401,

        APPLY_VIEW = 2101,
        APPLY_CREATE = 2102,
        ARCHIVE_APPLY_CREATE = 2103,
        APPLY_MODERATE = 2104,
        APPLY_EDIT = 2105,
        APPLY_DEL = 2106,
        APPLY_CHANGE_CODE = 2107,
        APPLY_VIEW_COMMENTS = 2108,
        APPLY_ADD_COMMENTS = 2109,
        APPLY_EDIT_COMMENTS = 2110,
        APPLY_PRINT = 2111,
        APPLY_SERTIFICATE_CREATE = 2112,
        APPLY_ROLLBACK_STATUS = 2113,
        APPLY_CORRECT = 2114,
        APPLY_SET_PAYMENT_CHECKBOX = 2115,
        APPLY_IMPORTANT_FILTER = 2116,

        REQUEST_VIEW = 2201,
        REQUEST_ADD = 2202,
        REQUEST_EDIT = 2203,
        REQUEST_DEL = 2204,
        REQUEST_SENT_EDIT = 2205,
        REQUEST_SIGN = 2206,

        EXPERTIZE_CREATE = 2301,
        EXPERTIZE_EDIT = 2302,
        EXPERTIZE_DEL = 2303,
        EXPERTIZE_ACCEPT = 2304,
        EXPERTIZE_VIEW_COMMENTS = 2305,
        EXPERTIZE_ADD_COMMENTS = 2306,

        CONCLUSION_VIEW = 2401,
        CONCLUSION_CREATE = 2402,
        CONCLUSION_EDIT = 2403,
        CONCLUSION_DEL = 2404,
        CONCLUSION_PRINT = 2405,
        CONCLUSION_EDIT_ALL = 2406,

        LETTER_VIEW = 2501,
        LETTER_CREATE = 2502,
        LETTER_EDIT = 2503,
        LETTER_DEL = 2504,
        LETTER_PRINT = 2505,
        LETTER_SIGN = 2506,
        LETTER_DOSSIER = 2507,

        APPLY_RON_VIEW = 2601,
        APPLY_RON_CREATE = 2602,
        APPLY_RON_EDIT = 2603,
        APPLY_RON_DELETE = 2604,
        OFERTA_RON_VIEW = 2701,

        CERTIFICATE_SIGNING = 3101,
        CERTIFICATE_ASSIGN_REGNUM = 3102,
        CERTIFICATE_ASSIGN_STORAGE = 3103,
        CERTIFICATE_PUT = 3104,
        CERTIFICATE_SEND = 3105,
        CERTIFICATE_PRINT = 3106,
        CERTIFICATE_MAIL = 3107,

        PAYMENTS_VIEW = 3201,
        PAYMENTS_IMPORT = 3202,
        PAYMENTS_BINDING = 3203,

        SHARED_DICTIONARY_ITEM_CREATE = 4101,
        SHARED_DICTIONARY_ITEM_EDIT = 4102,
        SHARED_DICTIONARY_ITEM_DEL = 4103,

        APPLY_REESTR_TEMPLATE_CREATE = 4201,
        APPLY_REESTR_TEMPLATE_EDIT = 4202,
        APPLY_REESTR_TEMPLATE_DEL = 4203,
        APPLY_TEMPLATE_EDIT = 4204,
        APPLY_DICTIONARY_ITEM_CREATE = 4205,
        APPLY_DICTIONARY_ITEM_EDIT = 4206,
        APPLY_DICTIONARY_ITEM_DEL = 4206,
        
        EXPERTIZE_DICTIONARY_ITEM_CREATE = 4307,
        EXPERTIZE_DICTIONARY_ITEM_EDIT = 4308,
        EXPERTIZE_DICTIONARY_ITEM_DEL = 4309,

        CONCLUSION_TEMPLATE_EDIT = 4401,
        CONCLUSION_DICTIONARY_ITEM_CREATE = 4402,
        CONCLUSION_DICTIONARY_ITEM_EDIT = 4403,
        CONCLUSION_DICTIONARY_ITEM_DEL = 4404,

        ACTS_FOR_GEC = 4501,
        ACTS_FOR_RON = 4502,
        VISA_FOR_GEC = 4503,
        VISA_FOR_RON = 4504,
        ACTS_DIG_SVID = 4505,
        PRINT_SERTIF = 4506,
        ACT_DEL = 4507,
        ACT_DEL_CASE = 4507,

        LEARNBASE_VIEW = 5101,
        LEARNBASE_ADD = 5102,
        LEARNBASE_EDIT = 5103,
        LEARNBASE_DEL = 5104,
        PRECEDENTS_VIEW = 5105,
        CALCULATOR_VIEW = 5106,
        TO_MINZDRAV = 5107,

        CORR_INPUT_VIEW = 5201,
        CORR_INPUT_EDIT = 5202,
        CORR_OUTPUT_VIEW = 5203,
        CORR_OUTPUT_EDIT = 5204,
        CORR_PERFORMER = 5205,
        CORR_OUTPUT_SET_SIGNED = 5206,

        REPORTS_SCIENCE = 6101,
        REPORTS_RON = 6102,
        REPORTS_EXPERT = 6103,
        REPORTS_OPERATOR = 6104,
        REPORTS_CHIEF = 6105,
        REPORTS_OTHER = 6106,
        REPORTS_OPER_RON = 6107,

        CHIEFROOM = 7101,
        DUTY_VIEW = 7102,
        DUTY_EDIT = 7103,
        CHIEF_JOURNAL = 7104
    }

    public class PermissionData
    {
        public PermissionEnum Id { get; set; }
        public string Name { get; set; }
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
                            _list = (from field in type.GetFields()
                                     let attr = field.GetCustomAttribute<PermissionAttribute>()
                                     where attr != null
                                     select new PermissionData
                                     {
                                         Id = (PermissionEnum)field.GetValue(null),
                                         Name = attr.Name,
                                         OrderNum = attr.OrderNum,
                                         Included = attr.Included
                                     }).ToArray();
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
        public int OrderNum;
        public IEnumerable<PermissionEnum> Included;

        public PermissionAttribute(string name, int ordernum, params PermissionEnum[] included)
        {
            Name = name;
            OrderNum = ordernum;
            Included = included;
        }

        public PermissionAttribute(string name, params PermissionEnum[] included) : this(name, 0, included)
        {

        }
    }
}
