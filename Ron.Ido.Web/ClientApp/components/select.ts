import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-select', {
        viewModel: {
            createViewModel: function(params:ISelectParams, componentInfo) {
                return new SelectModel(params);
            }
        },
        template: `
            <select class="form-control" data-bind="options: options, optionsText: optionsText, value: value, optionsCaption: optionsCaption, optionsAfterRender: afterRender"></select>`
    });
}

export class ISelectParams {
    value:ko.Observable<any>;
    options: any[] | ko.ObservableArray<any>;
    optionsText?: string;
    optionsCaption?: string;
}

class SelectModel {
    value: ko.Observable<any>;
    options: ko.ObservableArray<any>;
    optionsText: string;
    optionsCaption: string;

    constructor(params:ISelectParams) {
        this.value = params.value;
        this.options = ko.isObservable(params.options) ? params.options : ko.observableArray(params.options || []);
        this.optionsText = params.optionsText || 'text';
        this.optionsCaption = params.optionsCaption || '';
    }

    afterRender(option:Element, item:any) {
        if(!item) {
            $(option).hide();
        }
    }
}
