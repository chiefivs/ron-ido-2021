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
            <input class="form-control" type="text" data-bind="textInput: value, css:css, event:{keydown:keyDown}, disable:disable, attr:{readonly:readonly}, hasFocus:hasFocus"></input>`
    });
}

export interface ITextBoxParams extends IEditBaseParams {
    value: ko.Observable<any>;}

class TextBoxModel extends EditBaseModel {
    value: ko.Observable<any>;

    constructor(params:ITextBoxParams, componentInfo:any) {
        super(params);
        this.value = params.value;
    }
}

