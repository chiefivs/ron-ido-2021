import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-textbox', {
        viewModel: {
            createViewModel: function(params:ITextBoxParams, componentInfo) {
                return new TextBoxModel(params);
            }
        },
        template: `
            <input class="form-control" type="text" data-bind="textInput: value, css:css"></input>`
    });
}

export class ITextBoxParams {
    value:ko.Observable<any>;
    css?: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}
}

class TextBoxModel {
    value: ko.Observable<any>;
    css: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}

    constructor(params:ITextBoxParams) {
        this.value = params.value;
        this.css = params.css || {};
    }
}
