//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';

export namespace FileStorageApi {
    export function getBytes(uid:string): JQueryPromise<string> {
        const segments = ['api', 'files', 'download', uid];
        return WebApi.get(segments.join('/'));
    }


}
