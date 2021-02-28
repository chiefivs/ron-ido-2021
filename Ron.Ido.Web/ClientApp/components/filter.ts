import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-filter', {
        viewModel: {
            createViewModel: function(params:IFilterParams, componentInfo) {
                return new FilterModel(params);
            }
        },
        template: ``
    });
}

export type FilterValueType = 'string'|'number'|'date';

/**
 * eq - equals
 * ne - not equals
 * lt - less than
 */
export type FilterType = 'eq'|'ne'|'lt'|'gt'|'le'|'ge'|'in'|'bn'|'bl'|'br'|'ba'|'st'|'cn';

export interface IFilterParams{
    field: string;
    values: ko.ObservableArray<any>;
    valueType: FilterValueType;
    filterType: FilterType;
}

const ValueTypesDic = {
    FilterValueType: ''
};

class FilterModel {
    //private _templates:{[filterKey:FilterType]:{[valueKey:FilterValueType]:string;};};

    constructor(params:IFilterParams) {

    }
}