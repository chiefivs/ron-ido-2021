import * as ko from 'knockout';
import { IEditBaseParams, EditBaseModel} from './edit-base';

export function init(){
    ko.components.register('cmp-textbox', {
        viewModel: {
            createViewModel: function(params:ITextBoxParams, componentInfo) {
                return new TextBoxModel(params, componentInfo);
            }
        },
        template: `
            <input class="form-control" type="text" data-bind="textInput: value, css:css, event:{keydown:keyDown}, disable:disable, attr:{readonly:readonly, placeholder:placeholder}, hasFocus:hasFocus"></input>`
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

export interface ITextBoxParams extends IEditBaseParams {
    value: ko.Observable<any>;}

class TextBoxModel extends EditBaseModel {
    value: ko.Observable<any>;
    placeholder?:string;

    constructor(params:ITextBoxParams, componentInfo:any) {
        super(params);
        this.value = params.value;
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
