import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-checkbox', {
        viewModel: {
            createViewModel: function(params:ICheckBoxParams, componentInfo) {
                return new CheckboxModel(params);
            }
        },
        template: `
        <label><input type="checkbox" class="form-control" data-bind="checked:value"></input></label>`
    });
}

export class ICheckBoxParams {
    value:ko.Observable<any>;
}

class CheckboxModel {
    value: ko.Observable<any>;

    constructor(params:ICheckBoxParams) {
        this.value = params.value;
    }

}
