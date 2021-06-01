//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage, IODataOption, IODataForm } from './odata';
import { PermissionEnum } from './enums';

export namespace AdminAccessApi {
    export function getUsersPage(request:IODataRequest): JQueryPromise<IODataPage<IUsersPageItemDto>> {
        const segments = ['api', 'admin', 'access', 'users', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getUsersDictions(): JQueryPromise<IUsersListDictions> {
        const segments = ['api', 'admin', 'access', 'users', 'getdictions'];
        return WebApi.get(segments.join('/'));
    }

    export function getUser(id:number): JQueryPromise<IODataForm<IUserDto>> {
        const segments = ['api', 'admin', 'access', 'users', id, 'get'];
        return WebApi.get(segments.join('/'));
    }

    export function validateUser(user:IUserDto): JQueryPromise<any> {
        const segments = ['api', 'admin', 'access', 'users', 'validate'];
        return WebApi.post(segments.join('/'), user);
    }

    export function saveUser(user:IUserDto): JQueryPromise<any> {
        const segments = ['api', 'admin', 'access', 'users', 'save'];
        return WebApi.post(segments.join('/'), user);
    }

    export function deleteUser(id:number): JQueryPromise<any> {
        const segments = ['api', 'admin', 'access', 'users', id, 'delete'];
        return WebApi.del(segments.join('/'));
    }

    export function getRolesPage(request:IODataRequest): JQueryPromise<IODataPage<IRolesPageItemDto>> {
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
        id:number;
        fullName:string;
        login:string;
        isBlocked:any;
    }

    //  Ron.Ido.BM.Models.Admin.Access.UsersListDictions
    export interface IUsersListDictions {
        roles:IODataOption[];
    }

    //  Ron.Ido.BM.Models.Admin.Access.UserDto
    export interface IUserDto {
        id:number;
        surName:string;
        firstName:string;
        lastName:string;
        login:string;
        email:string;
        snils:string;
        remark:string;
        isBlocked:any;
        password:string;
        confirmPassword:string;
        roles:number[];
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
        viewStatuses:number[];
        stepStatuses:number[];
    }

}
