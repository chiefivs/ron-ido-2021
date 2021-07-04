import * as ko from 'knockout';
import { IFilterParams, getFilterStateValues } from './filter';
import { IODataFilter } from '../codegen/webapi/odata';

export function init(){
    ko.components.register('cmp-filters-panel', {
        viewModel: {
            createViewModel: function(params:IFiltersPanelParams, componentInfo) {
                return new FiltersPanelModel(params);
            }
        },
        template: `
            <form class="filters-panel" data-bind="submit:search">
                <div data-bind="foreach:filters">
                    <div><cmp-filter params="title:title, field:field, aliases:aliases, options:options, initialValues:initialValues, state:state, filterType:filterType, valueType:valueType"></cmp-filter></div>
                </div>
                <div>
                    <button class="btn btn-primary pull-right">ПОИСК</div>
                    <a class="btn btn-secondary pull-right" data-bind="click:reset">СБРОС</a>
                </div>
            </form>`
    });
}

export interface IFiltersPanelParams {
    filters: IFilterParams[] | ko.ObservableArray<IFilterParams>;
    states: ko.ObservableArray<IODataFilter>;
}

class FiltersPanelModel {
    filters: ko.ObservableArray<IFilterParams>;
    states: ko.ObservableArray<IODataFilter>;

    constructor(params: IFiltersPanelParams) {
        this.states = params.states;

        this.filters = ko.isObservableArray(params.filters)
            ? this.filters
            : ko.observableArray<IFilterParams>(<IFilterParams[]>params.filters || []);


        this.filters.subscribe(filters => {
            ko.utils.arrayForEach(filters, f => {
                if(!f.options)
                    f.options = [];

                if(!f.initialValues)
                    f.initialValues = null;

                if(!f.state)
                    f.state = ko.observable<IODataFilter>(null);
                
                if(!f.aliases)
                    f.aliases = [];
            })
        });
        this.filters.valueHasMutated();
    }

    search() {
        const filters = ko.utils.arrayFilter(this.filters(), f => !!f.state());
        this.states(ko.utils.arrayMap(filters, f => f.state()));
    }

    reset() {
        ko.utils.arrayForEach(this.filters(), f => {
            f.state(f.initialValues 
                ? { field: f.field, aliases: f.aliases || [], type: f.filterType, values:getFilterStateValues(f.initialValues)}
                : null);
        });

        this.search();
    }
}