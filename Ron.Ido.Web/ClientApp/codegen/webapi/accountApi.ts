//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { PermissionEnum } from './enums';

export namespace AccountApi {
    export function login(login:string, password:string): JQueryPromise<IIdentity> {
        const segments = ['api', 'account', 'login'];
        const urlParams = [`login=${login}`, `password=${password}`];
        return WebApi.get(segments.join('/')+'?'+urlParams.join('&'));
    }

    export function getMenu(): JQueryPromise<IMenuItem[]> {
        const segments = ['api', 'account', 'getmenu'];
        return WebApi.get(segments.join('/'));
    }

    //  Ron.Ido.BM.Models.Account.Identity
    export interface IIdentity {
        id:number;
        login:string;
        name:string;
        token:string;
        permissions:PermissionEnum[];
    }

    //  Ron.Ido.BM.Models.Account.MenuItem
    export interface IMenuItem {
        title:string;
        path:string;
        params:any;
        submenu:IMenuItem[];
    }

}
