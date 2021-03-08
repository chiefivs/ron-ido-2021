import * as ko from 'knockout';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../codegen/webapi/odata';
import { Utils } from '../modules/utils';

export function init(){
    ko.components.register('cmp-filter', {
        viewModel: {
            createViewModel: function(params:IFilterParams, componentInfo) {
                return new FilterModel(params);
            }
        },
        template: `
            <!-- ko if:templateNodes && templateNodes.length -->
            <div class="filter-title" data-bind="text:title"></div>
            <div data-bind="template:{nodes:templateNodes, data:$data}"></div>
            <!-- /ko -->`
    });
}

export type FilterValueType = 'string'|'number'|'date';

export interface IFilterParams{
    title: string | ko.Observable<string>;
    field: string;
    aliases?: string[];
    state: ko.Observable<IODataFilter>;
    valueType: FilterValueType;
    filterType: ODataFilterTypeEnum;
}

const ValueTypesDic = {
    FilterValueType: ''
};

class FilterModel {
    templateNodes: Node[];
    values = ko.observableArray([]);
    value1 = ko.observable(null);
    value2 = ko.observable(null);
    title: ko.Observable<string>;
    state: ko.Observable<IODataFilter>;

    private _templates:object = {};

    constructor(params:IFilterParams) {
        console.log(params);
        this._defineAllTemplates();
        this.templateNodes = this._getTemplate(params.filterType, params.valueType);
        this.state = params.state;

        this.title = ko.isObservable(params.title) ? params.title : ko.observable(params.title || '');

        this.values.subscribe(values => this.state(values.length 
            ? { field:params.field, aliases:params.aliases || null, type: params.filterType, values: values }
            : null));
    }

    private _defineAllTemplates() {
        this._setTemplate(ODataFilterTypeEnum.Contains, 'string',
            '<cmp-textbox params="value:value1"></cmp-textbox>');
        this._setTemplate(ODataFilterTypeEnum.In, 'number', '');
        this._setTemplate(ODataFilterTypeEnum.BetweenNone, 'date',
            `<div class="date-between">
                <div>
                    <cmp-datepicker params="value:value1"></cmp-datepicker>
                    <cmp-datepicker params="value:value2"></cmp-datepicker>
                </div>
             </div>`);
        this._setTemplate(ODataFilterTypeEnum.BetweenLeft, 'date', '');
        this._setTemplate(ODataFilterTypeEnum.BetweenRight, 'date', '');
        this._setTemplate(ODataFilterTypeEnum.BetweenAll, 'date', '');

        this.value1.subscribe(this._updateValues.bind(this));
        this.value2.subscribe(this._updateValues.bind(this));
    }

    private _setTemplate(filterType:ODataFilterTypeEnum, valueType:FilterValueType, template:string):void {
        if(!this._templates[filterType])
            this._templates[filterType] = {};

        const filterTypesObj = this._templates[filterType];
        if(!filterTypesObj[valueType])
            filterTypesObj[valueType] = '';

            filterTypesObj[valueType] = template;
    }

    private _getTemplate(filterType:ODataFilterTypeEnum, valueType:FilterValueType):Node[] {
        if(!this._templates[filterType])
            return null;

        const html = this._templates[filterType][valueType] || null;

        if(!html)
            return null;

        return Utils.getNodesFromHtml(html);
    }

    _updateValues() {
        const values = [];
        if(this.value1() || this.value2())
            values.push(this.value1());

        if(this.value2())
            values.push(this.value2());

        this.values(values);
    }
}