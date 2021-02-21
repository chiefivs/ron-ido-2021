//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IGetUsersPageCommand, IODataPage } from './odata';

export namespace AdminAccessApi {
    export function getUsers(request:IGetUsersPageCommand): JQueryPromise<IODataPage<IUserDto>> {
        const segments = ['api', 'admin', 'access', 'users', 'get'];
        return WebApi.post(segments.join('/'), request);
    }

    //  Ron.Ido.BM.Models.Admin.Access.UserDto
    export interface IUserDto {
        surName:string;
        firstName:string;
        login:string;
        roles:string[];
        fullName:string;
    }

}
