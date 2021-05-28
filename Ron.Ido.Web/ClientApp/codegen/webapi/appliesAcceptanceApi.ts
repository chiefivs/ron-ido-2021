//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage } from './odata';
import { ApplyEntryFormEnum } from './enums';

export namespace AppliesAcceptanceApi {
    export function getAppliesPage(request:IODataRequest): JQueryPromise<IODataPage<IAppliesAcceptancePageItemDto>> {
        const segments = ['api', 'acceptance', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    //  Ron.Ido.BM.Models.Applies.Acceptance.AppliesAcceptancePageItemDto
    export interface IAppliesAcceptancePageItemDto {
        barCode:string;
        entryFormId:ApplyEntryFormEnum;
        creatorFullName:string;
        ownerFullName:string;
        status:string;
    }

}
