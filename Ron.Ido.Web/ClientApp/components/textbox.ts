import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-textbox', {
        viewModel: {
            createViewModel: function(params:ITextBoxParams, componentInfo) {
                return new TextBoxModel(params);
            }
        },
        template: `
            <input type="text" data-bind="textInput: value"></input>`
    });
}

export class ITextBoxParams {
    value:ko.Observable<any>;
}

class TextBoxModel {
    value: ko.Observable<any>;

    constructor(params:ITextBoxParams) {
        this.value = params.value;
    }
}
