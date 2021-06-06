//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';

export namespace DossierApi {
    export function getDossier(id:number): JQueryPromise<IDossierDataDto> {
        const segments = ['api', 'dossier', 'get'];
        const urlParams = [`id=${id}`];
        return WebApi.get(segments.join('/')+'?'+urlParams.join('&'));
    }

    //  Ron.Ido.BM.Models.Dossier.DossierDataDto
    export interface IDossierDataDto {
        apply:IApplyData;
    }

    //  Ron.Ido.BM.Models.Dossier.ApplyData
    export interface IApplyData {
        id:number;
        barCode:string;
        createTime:string;
    }

}
