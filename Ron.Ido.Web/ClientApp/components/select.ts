import * as ko from 'knockout';
import { IEditBaseParams, EditBaseModel} from './edit-base';

export function init(){
    ko.components.register('cmp-select', {
        viewModel: {
            createViewModel: function(params:ISelectParams, componentInfo) {
                return new SelectModel(params, componentInfo);
            }
        },
        template: `
            <select class="form-control" data-bind="value: value, options: options, optionsText: optionsText, optionsValue: optionsValue, optionsCaption: optionsCaption, optionsAfterRender: afterRender, hasFocus:hasFocus, css:css, event:{keydown:keyDown}, disable:disable() || readonly(), attr:{readonly:readonly}"></select>`
    });
}

export interface ISelectParams extends IEditBaseParams {
    value:ko.Observable<any>;
    options: any[] | ko.ObservableArray<any>;
    optionsText?: string;
    optionsValue?: string;
    optionsCaption?: string;
}

class SelectModel extends EditBaseModel {
    value: ko.Observable<any>;
    options: ko.ObservableArray<any>;
    optionsText: string;
    optionsValue: string;
    optionsCaption: string;

    constructor(params:ISelectParams, componentInfo:any) {
        super(params);
        this.value = params.value;
        this.options = ko.isObservable(params.options) ? params.options : ko.observableArray(params.options || []);
        this.optionsText = params.optionsText || 'text';
        this.optionsValue = params.optionsValue || '';
        this.optionsCaption = params.optionsCaption || '';
    }

    afterRender(option:Element, item:any) {
        if(!item) {
            $(option).hide();
        }
    }
}
