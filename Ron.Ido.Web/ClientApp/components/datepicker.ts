import * as ko from 'knockout';
import { IEditBaseParams, EditBaseModel} from './edit-base';

export function init(){
    ko.components.register('cmp-datepicker', {
        viewModel: {
            createViewModel: function(params:IDatepickerParams, componentInfo) {
                return new DatepickerModel(params, componentInfo);
            }
        },
        template: `<input class="form-control" type="date" data-bind="value:value, css:css, event:{keydown:keyDown}, disable:disable, attr:{readonly:readonly}, hasFocus:hasFocus" />`
    });
}


export interface IDatepickerParams extends IEditBaseParams {
    value:ko.Observable<string>;
}

class DatepickerModel extends EditBaseModel {
    value: ko.Observable<string>;

    constructor(params:IDatepickerParams, componentInfo:any) {
        super(params);
        this.value = params.value;
    }
}
