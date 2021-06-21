import * as ko from 'knockout';
import { Utils } from '../modules/utils';
import { IEditBaseParams, EditBaseModel} from './edit-base';

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
    uploadUrl: string;
    errorMessage: ko.Observable<string>;
    files: ko.ObservableArray<IFileData>;
    start: ko.Observable<() => JQueryPromise<any>>;
    clear: ko.Observable<() => void>;
    validate: (file: IFileSelect) => boolean;
    progress: (data: IProgressData) => void;
    multiple?: boolean;
    accept?: string[];
}

class FileUploadModel extends EditBaseModel {
    errorMessage: ko.Observable<string>;
    files: ko.ObservableArray<IFileData>;
    hasFocus = ko.observable(false);
    elementData: IElementData;
    multiple: boolean;
    accept: string[];

    private input: JQuery;
    private link: JQuery;
    private validate: (file: IFileSelect) => boolean;
    private progress: (data: IProgressData) => void;

    constructor(params: IFileUploadParams, node: Node) {
        super(params);
        this.link = $('a.img-floppy', node);
        this.input = $('input[type=file]', node);
        this.input.attr('data-url', params.uploadUrl).hide();
        if(params.multiple)
            this.input.attr('multiple', 'multiple');
        this.link.on('click', evt => {
            this.input.trigger('click');
            evt.preventDefault();
        });

        if (params.accept) {
            this.input.attr('accept', params.accept.join(','));
        }

        this.files = params.files || ko.observableArray([]);
        this.validate = params.validate;
        this.progress = params.progress;
        this.multiple = !!params.multiple;
        this.accept = params.accept || null;

        this.errorMessage = ko.observable('');

        if (ko.isObservable(params.start)) {
            params.start(this.start.bind(this));
        }
        if (ko.isObservable(params.clear)) {
            params.clear(this.clear.bind(this));
        }

        this.input.on('change', (evt:any) => {
            console.log(evt.target.files);
        });

        // (this.input as any).fileupload({
        //     dataType: 'json',
        //     add: (e:any, data:IElementData) => {
        //         var file = data.files[0];
        //         if (!this.onvalidate(file))
        //             return;

        //         if (!this.elementData) {
        //             this.elementData = data;
        //             this.elementData.files = [];
        //         }

        //         this.onselect(file);
        //     },
        //     beforeSend: () => {
        //     },
        //     progressall: (e:any, data:IProgressData) => {
        //         if (this.progress)
        //             this.progress(data);
        //     }
        // });
    }

    delete(file: File) {
        this.files.remove(file);
    }

    onselect(select: IFileSelect): void {
        var file = new File(select);
        file.owner = this;
        if (!this.multiple) {
            this.clear();
        } 
        if (this.accept) {
            const ext = file.name.split('.').pop();
            const allowed = !!ko.utils.arrayFirst(this.accept, i => i === ext);

             if (!allowed) {
                this.errorMessage(this.accept + ' only files are allowed');
                return ;
            }
            this.errorMessage('');
        }
        this.files.push(file);
    }

    onvalidate(file: IFileSelect): boolean {
        if (ko.utils.arrayFilter(this.files(), f => !f.uid && f.name === file.name).length)
            return false;

        if (this.validate)
            return this.validate(file);

        return true;
    }

    ondone(result: IFileData[], status: string, promise: JQueryPromise<IFileData[]>): JQueryPromise<IFileData[]> {
        ko.utils.arrayForEach(result, r => {
            var file = ko.utils.arrayFirst(this.files(), f => !f.uid && f.name === r.name);
            if (!file)
                return;

            file.uid = r.uid;
        });

        this.files.valueHasMutated();
        return promise;
    }

    clear(): void {
        this.files.removeAll();
    }

    start(): JQueryPromise<IFileData[]> {
        var selected = ko.utils.arrayFilter(this.files(), f => (<File>f).fileSelect !== null);
        this.elementData.files = ko.utils.arrayMap(selected, f => (<File>f).fileSelect);
        return this.elementData.submit().then(this.ondone.bind(this));
    }
}



class File implements IFileData {
    uid: string;
    size: number;
    sizeString: string;
    name: string;
    fileSelect: IFileSelect;

    owner: FileUploadModel;

    constructor(data: IFileData | IFileSelect) {
        if ((<IFileData>data).uid) {
            var fdata = <IFileData>data;
            this.uid = fdata.uid;
            this.name = fdata.name;
            this.size = fdata.size;
            this.sizeString = this.getSizeString(fdata.size);
            this.fileSelect = null;
        } else {
            var fsel = <IFileSelect>data;
            this.uid = null;
            this.name = fsel.name;
            this.size = fsel.size;
            this.sizeString = this.getSizeString(fsel.size);
            this.fileSelect = fsel;
        }
    }

    delete(): void {
        if (this.owner) {
            this.owner.delete(this);
        }
    }

    private getSizeString(size: number): string {
        return size <= 1024 ? size.toString() + ' b'
            : size <= 1024 * 1024 ? Math.round(size / 1024).toString() + 'kb'
            : Math.round(size / 1024 / 1024).toString() + 'Mb';
    }
}

export interface IFileData {
    uid: string;
    size: number;
    sizeString: string;
    name: string;
    delete(): void;
}

export interface IFileSelect {
    name: string;
    size: number;
    type: string;
    content?: string;
}

export interface IProgressData {
    loaded: number;
    total: number;
}

export interface IElementData {
    files: IFileSelect[];
    loaded: number;
    total: number;
    result: any;
    submit: () => JQueryPromise<IFileData[]>;
    abort: () => void;
}
