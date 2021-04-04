import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-datepicker', {
        viewModel: {
            createViewModel: function(params:IDatepickerParams, componentInfo) {
                return new DatepickerModel(params);
            }
        },
        template: `<input class="form-control" type="date" data-bind="value: value" />`
    });
}

export class IDatepickerParams {
    value:ko.Observable<string>;
}

class DatepickerModel {
    value: ko.Observable<string>;

    constructor(params:IDatepickerParams) {
        this.value = params.value;
    }
}
