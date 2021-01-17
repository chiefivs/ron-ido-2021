import WebApi from '../../modules/webapi';

export namespace AccountApi {
    export function getUserInfo(): JQueryPromise<IUserInfo> {
        const segments = ['api', 'account'];
        return WebApi.get('/'+segments.join('/'));
    }

    export function getUserPermission(permissionName:string, ts:number): JQueryPromise<string> {
        const segments = ['api', 'account', 'getperms', permissionName];
        const urlParams = [`ts=${ts}`];
        return WebApi.get('/'+segments.join('/')+'?'+urlParams.join('&'));
    }

    //  ForeignDocsRec2020.Web.UserClaim
    export interface IUserClaim {
        type:string
        value:string
    }

    //  ForeignDocsRec2020.Web.UserRole
    export enum UserRole {
        Admin = 0,
        Visitor = 1
    }

    //  ForeignDocsRec2020.Web.UserInfo
    export interface IUserInfo {
        userName:string
        claims:IUserClaim[]
        role:UserRole
    }

}
