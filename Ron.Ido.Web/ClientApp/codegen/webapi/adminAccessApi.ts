//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IGetUsersPageCommand, IODataPage, IODataOption, IGetRolesPageCommand } from './odata';

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
        name:string;
    }

}
