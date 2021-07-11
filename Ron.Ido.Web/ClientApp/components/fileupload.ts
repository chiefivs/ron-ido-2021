//  https://developer.mozilla.org/ru/docs/Web/API/File/Using_files_from_web_applications
//  https://habr.com/ru/post/321250/
//  https://developer.mozilla.org/ru/docs/Web/API/FormData/Using_FormData_Objects
import * as ko from 'knockout';
import { IEditBaseParams, EditBaseModel} from './edit-base';
import { IFileInfoDto } from '../codegen/webapi/odata';
//import { WebApi } from '../modules/webapi';

export function init(){
    ko.components.register('cmp-fileupload', {
        viewModel: {
            createViewModel(params: IFileUploadParams, componentInfo) {

                return new FileUploadModel(params, componentInfo.element);
            }
        },
        template: `
            <a href="#" class="btn img img-floppy img-size-20" data-bind="hasFocus:hasFocus"></a>
            <input type="file" />`
    });
}

export interface IFileUploadParams extends IEditBaseParams {
    files: ko.ObservableArray<FileData>;
    multiple?: boolean;
    accept?: string[];
    validate?: (files: FileData[]) => boolean;
}

class FileUploadModel extends EditBaseModel {
    files: ko.ObservableArray<FileData>;
    multiple: boolean;
    accept: string[];

    private input: JQuery;
    private link: JQuery;
    private validate: (files: FileData[]) => boolean;

    constructor(params: IFileUploadParams, node: Node) {
        super(params);
        this.link = $('a.img-floppy', node);
        this.input = $('input[type=file]', node);
        this.input.hide();
        if(params.multiple)
            this.input.attr('multiple', 'multiple');
        this.link.on('click', evt => {
            this.input.trigger('click');
            evt.preventDefault();
        });

        if (params.accept) {
            this.input.attr('accept', params.accept.join(','));
        }

        this.validate = params.validate || null;

        this.files = params.files || ko.observableArray([]);
        this.multiple = !!params.multiple;
        this.accept = params.accept || null;

        this.input.on('change', (evt:any) => {
            const selected = ko.utils.arrayMap(evt.target.files as File[], f => new FileData(f));
            if(this.validate && ! this.validate(selected))
                return;

            const files = this.multiple ? this.files() : [];
            ko.utils.arrayForEach(selected, file => files.push(file));

            this.files(files);
        });
    }
}



export class FileData implements IFileInfoDto {
    uid: string;
    name: string;
    size: number;
    contentType: string;
    sizeString: string;
    selection: File;
    bytesBase64: string = null;

    constructor(data: IFileInfoDto | File) {
        if(data instanceof File){
            this.uid = null;
            this.name = data.name;
            this.size = data.size;
            this.contentType = data.type;
            this.sizeString = this.getSizeString(data.size);
            this.selection = data;
        } else {
            this.uid = data.uid;
            this.name = data.name;
            this.size = data.size;
            this.contentType = data.contentType;
            this.sizeString = this.getSizeString(data.size);
        }
    }

    getFileInfo(): IFileInfoDto {
        return {
            name: this.name,
            size: this.size,
            uid: this.uid,
            contentType: this.contentType,
            bytesBase64: this.bytesBase64
        };
    }

    upload(url:string):JQueryPromise<IFileInfoDto[]> {    //  'api/storage/upload'
        if(!this.selection)
            return;

        const webapi = require('../modules/webapi');
        return webapi.WebApi.upload(url, this.selection);
    }
    
    fillBytesBase64() {
        const deferred = $.Deferred();
        if(!this.selection)
            deferred.reject();

        const reader = new FileReader();
        reader.readAsDataURL(this.selection);
        reader.onload = () => {
            const result = reader.result as string;
            const n = result.indexOf(',');
            this.bytesBase64 = result.substr(n+1);
            deferred.resolve();
        };
        reader.onerror = () => deferred.reject();

        return deferred.promise();
    }

    private getSizeString(size: number): string {
        return size <= 1024 ? size.toString() + ' b'
            : size <= 1024 * 1024 ? Math.round(size / 1024).toString() + 'kb'
            : Math.round(size / 1024 / 1024).toString() + 'Mb';
    }
}
