//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage, IODataOption } from './odata';

export namespace DuplicatesApi {
    export function getDuplicatesSearchPage(request:IODataRequest): JQueryPromise<IODataPage<IDuplicatesPageItemDto>> {
        const segments = ['api', 'duplicates', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getDuplicatesSearchDictions(): JQueryPromise<IDuplicatesDictions> {
        const segments = ['api', 'duplicates', 'getdictions'];
        return WebApi.get(segments.join('/'));
    }

    //  Ron.Ido.BM.Models.Duplicates.DuplicatesPageItemDto
    export interface IDuplicatesPageItemDto {
        id:number;
        dossierId:number;
        createDate:string;
        barCode:string;
        certificateNum:string;
        creatorFullName:string;
        ownerFullName:string;
        storage:string;
        handoutDate:string;
        status:string;
    }

    //  Ron.Ido.BM.Models.Duplicates.DuplicatesDictions
    export interface IDuplicatesDictions {
        statuses:IODataOption[];
    }

}
