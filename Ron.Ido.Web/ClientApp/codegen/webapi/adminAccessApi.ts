//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IGetUsersPageCommand, IODataPage } from './odata';

export namespace AdminAccessApi {
    export function getUsers(request:IGetUsersPageCommand): JQueryPromise<IODataPage<IUsersPageItemDto>> {
        const segments = ['api', 'admin', 'access', 'users', 'get'];
        return WebApi.post(segments.join('/'), request);
    }

    //  Ron.Ido.BM.Models.Admin.Access.UsersPageItemDto
    export interface IUsersPageItemDto {
        fullName:string;
        login:string;
        isBlocked:any;
    }

}
