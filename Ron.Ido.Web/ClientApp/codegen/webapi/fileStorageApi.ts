//  Сгенерировано на основе серверного кода. Не изменять!!!
import { WebApi } from '../../modules/webapi';
import { IFileInfoDto } from './odata';

export namespace FileStorageApi {
    export function upload(): JQueryPromise<IFileInfoDto[]> {
        const segments = ['api', 'storage', 'upload'];
        return WebApi.post(segments.join('/'));
    }


}
