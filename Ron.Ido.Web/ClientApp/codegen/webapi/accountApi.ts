//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';

export namespace AccountApi {
    export function getUserInfo(): JQueryPromise<IUserInfo> {
        const segments = ['api', 'account', 'getUserInfo'];
        return WebApi.get('/'+segments.join('/'));
    }

    export function getUserPermission(permissionName:string, ts:number): JQueryPromise<string> {
        const segments = ['api', 'account', 'getPerms', permissionName];
        const urlParams = [`ts=${ts}`];
        return WebApi.get('/'+segments.join('/')+'?'+urlParams.join('&'));
    }

    export function getPage(input:IListPageRequest): JQueryPromise<IListPage<string,number>> {
        const segments = ['api', 'account', 'getPage'];
        return WebApi.post('/'+segments.join('/'), input);
    }

    //  ForeignDocsRec2020.Web.UserClaim
    export interface IUserClaim {
        type:string;
        value:string;
    }

    //  ForeignDocsRec2020.Web.UserRole
    export enum UserRole {
        Admin = 0,
        Visitor = 1
    }

    //  ForeignDocsRec2020.Web.UserInfo
    export interface IUserInfo {
        userName:string;
        claims:IUserClaim[];
        role:UserRole;
    }

    //  ForeignDocsRec2020.Web.Controllers.ListPageRequest
    export interface IListPageRequest {
        filter:string;
        position:number;
    }

    //  ForeignDocsRec2020.Web.Controllers.ListPage<T1,T2>
    export interface IListPage<T1,T2> {
        total:number;
        position:number;
        items:T1[];
        id:T2;
    }

}
