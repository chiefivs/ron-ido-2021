//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IGetUsersPageCommand, IODataPage, IODataOption, IGetRolesPageCommand, IODataForm } from './odata';

export namespace AdminAccessApi {
    export function getUsersPage(request:IGetUsersPageCommand): JQueryPromise<IODataPage<IUsersPageItemDto>> {
        const segments = ['api', 'admin', 'access', 'users', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getUsersDictions(): JQueryPromise<IUsersListDictions> {
        const segments = ['api', 'admin', 'access', 'users', 'getdictions'];
        return WebApi.get(segments.join('/'));
    }

    export function getRolesPage(request:IGetRolesPageCommand): JQueryPromise<IODataPage<IRolesPageItemDto>> {
        const segments = ['api', 'admin', 'access', 'roles', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getRole(id:number): JQueryPromise<IODataForm<IRoleDto>> {
        const segments = ['api', 'admin', 'access', 'roles', id, 'get'];
        return WebApi.get(segments.join('/'));
    }

    export function validateRole(role:IRoleDto): JQueryPromise<{[key:string]:string[]}> {
        const segments = ['api', 'admin', 'access', 'roles', 'validate'];
        return WebApi.post(segments.join('/'), role);
    }

    export function saveRole(role:IRoleDto): JQueryPromise<any> {
        const segments = ['api', 'admin', 'access', 'roles', 'save'];
        return WebApi.post(segments.join('/'), role);
    }

    export function deleteRole(id:number): JQueryPromise<any> {
        const segments = ['api', 'admin', 'access', 'roles', id, 'delete'];
        return WebApi.del(segments.join('/'));
    }

    //  Ron.Ido.BM.Models.Admin.Access.UsersPageItemDto
    export interface IUsersPageItemDto {
        fullName:string;
        login:string;
        isBlocked:any;
    }

    //  Ron.Ido.BM.Models.Admin.Access.UsersListDictions
    export interface IUsersListDictions {
        roles:IODataOption[];
    }

    //  Ron.Ido.BM.Models.Admin.Access.RolesPageItemDto
    export interface IRolesPageItemDto {
        id:number;
        name:string;
    }

    //  Ron.Ido.BM.Models.Admin.Access.RoleDto
    export interface IRoleDto {
        id:number;
        name:string;
        description:string;
        isDefault:any;
        isAdmin:any;
        rolePermissions:PermissionEnum[];
        viewStatuses:ApplyStatusEnum[];
        stepStatuses:ApplyStatusEnum[];
    }

    //  Ron.Ido.EM.Enums.PermissionEnum
    export enum PermissionEnum {
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
        SYSTEN_DICTIONARY_ALL = 4104,
        APPLY_REESTR_TEMPLATE_CREATE = 4201,
        APPLY_REESTR_TEMPLATE_EDIT = 4202,
        APPLY_REESTR_TEMPLATE_DEL = 4203,
        APPLY_TEMPLATE_EDIT = 4204,
        APPLY_DICTIONARY_ITEM_CREATE = 4205,
        APPLY_DICTIONARY_ITEM_EDIT = 4206,
        APPLY_DICTIONARY_ITEM_DEL = 4207,
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
        ACT_DEL_CASE = 4508,
        LEARNBASE_VIEW = 5101,
        LEARNBASE_ADD = 5102,
        LEARNBASE_EDIT = 5103,
        LEARNBASE_DEL = 5104,
        PRECEDENTS_VIEW = 5105,
        CALCULATOR_VIEW = 5201,
        TO_MINZDRAV = 5202,
        CORR_INPUT_VIEW = 5301,
        CORR_INPUT_EDIT = 5302,
        CORR_OUTPUT_VIEW = 5303,
        CORR_OUTPUT_EDIT = 5304,
        CORR_PERFORMER = 5305,
        CORR_OUTPUT_SET_SIGNED = 5306,
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

    //  Ron.Ido.EM.Enums.ApplyStatusEnum
    export enum ApplyStatusEnum {
        NO_VALIDATED = 1,
        HAS_ERRORS = 2,
        UNDERMANNED = 3,
        FIXED_BY_USER = 4,
        APPROVED = 5,
        ON_RESEARCH = 10,
        ON_RESEARCH_END = 11,
        ON_EXPERTIZE = 12,
        SUSPENDED = 13,
        DECISION_PREPARATION = 14,
        DECISION_ACT = 15,
        SIGNING_POSTED = 16,
        INFO_LETTER = 17,
        SECOND_REQUEST = 18,
        READY_TO_GIVE = 23,
        PREPARE_TO_GIVE = 24,
        WAIT_PAYMENT = 32,
        WAIT_DOCUMENTS = 33,
        GIVEN = 41,
        DELETED = 99
    }

}
