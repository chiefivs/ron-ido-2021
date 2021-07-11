import * as ko from 'knockout';
import { IEditBaseParams, EditBaseModel} from './edit-base';

export function init(){
    ko.components.register('cmp-checkbox', {
        viewModel: {
            createViewModel: function(params:ICheckBoxParams, componentInfo) {
                return new CheckBoxModel(params, componentInfo);
            }
        },
        template: `
            <input type="checkbox" data-bind="value:value, checked:checked, css:css, event:{keydown:keyDown}, disable:disable() || readonly(), attr:{readonly:readonly}, hasFocus:hasFocus"></input>`
    });
}

export interface ICheckBoxParams extends IEditBaseParams {
    value: any;
    checked: ko.Observable<any>;
}

class CheckBoxModel extends EditBaseModel {
    value: ko.Observable<any>;
    checked: ko.Observable<any>;

    constructor(params:ICheckBoxParams, componentInfo:any) {
        super(params);
        this.value = params.value;
        this.checked = params.checked;
    }
}