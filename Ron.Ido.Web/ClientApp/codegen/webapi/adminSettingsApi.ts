//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage, IODataForm } from './odata';

export namespace AdminSettingsApi {
    export function getApplyStatusesPage(request:IODataRequest): JQueryPromise<IODataPage<IApplyStatusPageItemDto>> {
        const segments = ['api', 'admin', 'settings', 'status', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getStatus(id:number): JQueryPromise<IODataForm<IApplyStatusDto>> {
        const segments = ['api', 'admin', 'settings', 'status', id, 'get'];
        return WebApi.get(segments.join('/'));
    }

    export function validateApplyStatus(status:IApplyStatusDto): JQueryPromise<{[key:string]:string[]}> {
        const segments = ['api', 'admin', 'settings', 'status', 'validate'];
        return WebApi.post(segments.join('/'), status);
    }

    export function saveStatus(status:IApplyStatusDto): JQueryPromise<any> {
        const segments = ['api', 'admin', 'settings', 'status', 'save'];
        return WebApi.post(segments.join('/'), status);
    }

    export function deleteStatus(id:number): JQueryPromise<any> {
        const segments = ['api', 'admin', 'settings', 'status', id, 'delete'];
        return WebApi.del(segments.join('/'));
    }

    //  Ron.Ido.BM.Models.Admin.Settings.ApplyStatusPageItemDto
    export interface IApplyStatusPageItemDto {
        id:number;
        name:string;
    }

    //  Ron.Ido.BM.Models.Admin.Settings.ApplyStatusDto
    export interface IApplyStatusDto {
        id:number;
        name:string;
        allowStepToStatuses:number[];
        nameForButton:string;
        nameForApplier:string;
        nameForApplierEng:string;
        descriptionForApplier:string;
        descriptionForApplierEng:string;
        visibleForApplier:any;
        statusEnumValue:string;
    }

}
