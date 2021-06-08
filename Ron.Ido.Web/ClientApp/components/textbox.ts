import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-textbox', {
        viewModel: {
            createViewModel: function(params:ITextBoxParams, componentInfo) {
                return new TextBoxModel(params);
            }
        },
        template: `
            <input class="form-control" type="text" data-bind="textInput: value, css:css, attr:{placeholder:placeholder}"></input>`
    });
    ko.components.register('cmp-richtext', {
        viewModel: {
            createViewModel: function(params:ITextBoxParams, componentInfo) {
                return new RichTextModel(params);
            }
        },
        template: `
            <textarea class="form-control" type="text" data-bind="textInput: value, css:css, attr:{placeholder:placeholder}" rows="5"></textarea>`
    });
}

export class ITextBoxParams {
    value:ko.Observable<any>;
    css?: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}
    placeholder?:string;
}

class TextBoxModel {
    value: ko.Observable<any>;
    css: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}
    placeholder?:string;

    constructor(params:ITextBoxParams) {
        this.value = params.value;
        this.css = params.css || {};
        this.placeholder = params.placeholder || null;
    }
}

class RichTextModel {
    value: ko.Observable<any>;
    css: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}
    placeholder?:string;

    constructor(params:ITextBoxParams) {
        this.value = params.value;
        this.css = params.css || {};
        this.placeholder = params.placeholder || null;
    }
}
