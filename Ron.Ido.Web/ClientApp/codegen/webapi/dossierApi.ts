//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IODataForm, IFileInfoDto } from './odata';

export namespace DossierApi {
    export function getDossier(id:number): JQueryPromise<IDossierDataDto> {
        const segments = ['api', 'dossier', id, 'get'];
        return WebApi.get(segments.join('/'));
    }

    export function getApply(id:number): JQueryPromise<IODataForm<IApplyDto>> {
        const segments = ['api', 'dossier', 'apply', id, 'get'];
        return WebApi.get(segments.join('/'));
    }

    export function validateApply(apply:IApplyDto): JQueryPromise<{[key:string]:string[]}> {
        const segments = ['api', 'dossier', 'apply', 'validate'];
        return WebApi.post(segments.join('/'), apply);
    }

    export function saveApply(apply:IApplyDto): JQueryPromise<IDossierDataDto> {
        const segments = ['api', 'dossier', 'apply', 'save'];
        return WebApi.post(segments.join('/'), apply);
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

    //  Ron.Ido.BM.Models.Dossier.DossierDataDto
    export interface IDossierDataDto {
        id:number;
        apply:IApplyData;
        duplicate:IDuplicateData;
    }

    //  Ron.Ido.BM.Models.Dossier.ApplyData
    export interface IApplyData {
        id:number;
        barCode:string;
        createTime:string;
    }

    //  Ron.Ido.BM.Models.Dossier.DuplicateData
    export interface IDuplicateData {
        id:number;
        barCode:string;
        createTime:string;
    }

    //  Ron.Ido.BM.Models.Dossier.ApplyDto
    export interface IApplyDto {
        id:number;
        createTime:string;
        transmitOpenChannels:boolean;
        docsWillSendByPost:boolean;
        creatorFirstName:string;
        isCreatorFirstNameAbsent:boolean;
        creatorLastName:string;
        isCreatorLastNameAbsent:boolean;
        creatorSurname:string;
        isCreatorSurnameAbsent:boolean;
        creatorGender:number;
        creatorBirthDate:string;
        creatorBirthPlace:string;
        creatorCitizenshipId:number;
        creatorPassportTypeId:number;
        creatorPassportReq:string;
        byWarrant:boolean;
        warrantReq:string;
        warrantDate:string;
        warrantTerm:string;
        creatorCountryId:number;
        creatorMailIndex:string;
        creatorCityName:string;
        creatorStreet:string;
        creatorCorpus:string;
        creatorBuilding:string;
        creatorBlock:string;
        creatorFlat:string;
        creatorPhone:string;
        creatorEmail:string;
        deliveryFormId:number;
        certificateDeliveryForms:number[];
        returnOriginalsFormId:number;
        isReturnOriginalsPostAddressDifferent:boolean;
        returnOriginalsPostAddress:string;
        ownerFirstName:string;
        isOwnerFirstNameAbsent:boolean;
        ownerLastName:string;
        isOwnerLastNameAbsent:boolean;
        ownerSurname:string;
        isOwnerSurnameAbsent:boolean;
        ownerGender:number;
        ownerBirthDate:string;
        ownerBirthPlace:string;
        ownerCountryId:number;
        ownerMailIndex:string;
        ownerCityName:string;
        ownerStreet:string;
        ownerCorpus:string;
        ownerBuilding:string;
        ownerBlock:string;
        ownerFlat:string;
        ownerPhone:string;
        ownerEmail:string;
        ownerCitizenshipId:number;
        ownerPassportTypeId:number;
        ownerPassportReq:string;
        docCountryId:number;
        docTypeId:number;
        docDescription:string;
        docBlankNum:string;
        docRegNum:string;
        docDate:string;
        docDateYear:number;
        docAttachmentsCount:number;
        docFullName:string;
        schoolCountryId:number;
        schoolName:string;
        schoolTypeId:number;
        schoolPostIndex:string;
        schoolCityName:string;
        schoolAddress:string;
        schoolPhone:string;
        schoolFax:string;
        schoolEmail:string;
        baseLearnDateBegin:string;
        baseLearnDateEnd:string;
        specialLearnDateBegin:string;
        specialLearnDateEnd:string;
        fixedLearnSpecialityName:string;
        specialLearnFormId:number;
        aimId:number;
        orgCreator:string;
        other:string;
        entryFormId:number;
        isNovorossia:boolean;
        isRostovFilial:boolean;
        attachments:IApplyAttachmentDto[];
    }

    //  Ron.Ido.BM.Models.Dossier.ApplyAttachmentDto
    export interface IApplyAttachmentDto {
        id:number;
        required:boolean;
        given:boolean;
        description:string;
        error:string;
        attachmentTypeId:number;
        attachmentTypeName:string;
        fileInfo:IFileInfoDto[];
    }

    //  Ron.Ido.BM.Models.Dossier.DuplicateDto
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
