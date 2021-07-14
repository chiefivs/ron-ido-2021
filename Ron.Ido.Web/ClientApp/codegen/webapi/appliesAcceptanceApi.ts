//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataRequest, IODataPage, IODataOption } from './odata';
import { ApplyEntryFormEnum } from './enums';

export namespace AppliesAcceptanceApi {
    export function getAppliesPage(request:IODataRequest): JQueryPromise<IODataPage<IAcceptancePageItemDto>> {
        const segments = ['api', 'acceptance', 'getpage'];
        return WebApi.post(segments.join('/'), request);
    }

    export function getAcceptanceDictions(): JQueryPromise<IAcceptanceDictions> {
        const segments = ['api', 'acceptance', 'getdictions'];
        return WebApi.get(segments.join('/'));
    }

    //  Ron.Ido.BM.Models.Applies.Acceptance.AcceptancePageItemDto
    export interface IAcceptancePageItemDto {
        id:number;
        dossierId:number;
        barCode:string;
        createDate:string;
        entryFormId:ApplyEntryFormEnum;
        creatorFullName:string;
        ownerFullName:string;
        status:string;
    }

    //  Ron.Ido.BM.Models.Applies.Acceptance.AcceptancePageItemDto - статические поля
    export class AcceptancePageItemDtoConstants {
        static primaryBarCodeFilterField = 'primaryBarCode';
        static barCodeFilterField = 'barCode';
        static createTimeFilterField = 'createTime';
        static creatorSurnameFilterField = 'creatorSurname';
        static creatorFirstNameFilterField = 'creatorFirstName';
        static creatorLastNameFilterField = 'creatorLastName';
        static ownerSurnameFilterField = 'ownerSurname';
        static ownerFirstNameFilterField = 'ownerFirstName';
        static ownerLastNameFilterField = 'ownerLastName';
        static statusesFilterField = 'statuses';
        static learnLevelsFilterField = 'learnLevels';
        static entryFormsFilterField = 'entryForms';
        static stagesFilterField = 'stages';
    }

    //  Ron.Ido.BM.Models.Applies.Acceptance.AcceptanceDictions
    export interface IAcceptanceDictions {
        statuses:IODataOption[];
        learnLevels:IODataOption[];
        entryForms:IODataOption[];
        stages:IODataOption[];
    }

}
