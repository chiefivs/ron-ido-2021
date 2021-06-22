//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage, IODataOption } from './odata';
import { ApplyEntryFormEnum } from './enums';

export namespace AppliesSearchtApi {
    export function getAppliesSearchPage(request:IODataRequest): JQueryPromise<IODataPage<IAppliesSearchPageItemDto>> {
        const segments = ['api', 'appliesSearch', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getAppliesSearchDictions(): JQueryPromise<IAppliesSearchDictions> {
        const segments = ['api', 'appliesSearch', 'getdictions'];
        return WebApi.get(segments.join('/'));
    }

    //  Ron.Ido.BM.Models.Applies.AppliesSearch.AppliesSearchPageItemDto
    export interface IAppliesSearchPageItemDto {
        id:number;
        dossierId:number;
        barCode:string;
        createDate:string;
        entryFormId:ApplyEntryFormEnum;
        creatorFullName:string;
        ownerFullName:string;
        status:string;
    }

    //  Ron.Ido.BM.Models.Applies.AppliesSearch.AppliesSearchDictions
    export interface IAppliesSearchDictions {
        statuses:IODataOption[];
        learnLevels:IODataOption[];
        entryForms:IODataOption[];
        stages:IODataOption[];
    }

}
