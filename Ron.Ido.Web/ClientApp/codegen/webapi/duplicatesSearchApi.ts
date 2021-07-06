//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage, IODataOption, IODataForm } from './odata';

export namespace DuplicatesSearchApi {
    export function getDuplicatesSearchPage(request:IODataRequest): JQueryPromise<IODataPage<IDuplicatesSearchPageItemDto>> {
        const segments = ['api', 'duplicatesSearch', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getDuplicatesSearchDictions(): JQueryPromise<IDuplicatesSearchDictions> {
        const segments = ['api', 'duplicatesSearch', 'getdictions'];
        return WebApi.get(segments.join('/'));
    }

    export function getDuplicate(id:number): JQueryPromise<IODataForm<IDuplicateDto>> {
        const segments = ['api', 'duplicate', id, 'get'];
        return WebApi.get(segments.join('/'));
    }

    export function validateDuplicate(apply:IDuplicateDto): JQueryPromise<{[key:string]:string[]}> {
        const segments = ['api', 'duplicate', 'validate'];
        return WebApi.post(segments.join('/'), apply);
    }

    export function saveDuplicate(apply:IDuplicateDto): JQueryPromise<any> {
        const segments = ['api', 'duplicate', 'save'];
        return WebApi.post(segments.join('/'), apply);
    }

    //  Ron.Ido.BM.Models.Duplicate.DuplicatesSearchPageItemDto
    export interface IDuplicatesSearchPageItemDto {
        id:number;
        dossierId:number;
        barCode:string;
        createDate:string;
        creatorFullName:string;
        ownerFullName:string;
        status:string;
    }

    //  Ron.Ido.BM.Models.DuplicatesSearch.DuplicatesSearchDictions
    export interface IDuplicatesSearchDictions {
        statuses:IODataOption[];
    }

    //  Ron.Ido.BM.Models.Duplicate.DuplicateDto
    export interface IDuplicateDto {
        id:number;
        createTime:string;
        handoutTime:string;
        barCode:string;
        storage:string;
        createUserId:number;
        handoutUserId:number;
        isEnglish:boolean;
        statusId:number;
        email:string;
        note:string;
        fullName:string;
        mailIndex:string;
        cityName:string;
        street:string;
        block:string;
        flat:string;
        corpus:string;
        building:string;
        address:string;
        phones:string;
        creatorCountryId:number;
        docFullName:string;
        schoolName:string;
        docCountryId:number;
        documentTypeId:number;
        documentDate:string;
        returnOriginalsFormId:number;
        returnOriginalsPostAddress:string;
    }

}
