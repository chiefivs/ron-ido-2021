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

export type FilterValueType = 'string'|'number'|'date'|'boolean';

export interface IFilterOption {
    value: string;
    text: string;
}

export interface IFilterParams{
    title: string | ko.Observable<string>;
    field: string;
    aliases?: string[];
    valueType: FilterValueType;
    filterType: ODataFilterTypeEnum;
    options?: IFilterOption[]|ko.ObservableArray<IFilterOption>;
    initialValues?: any[];
    state?: ko.Observable<IODataFilter>;
}

export function getFilterStateValues(values:any[]){
    const res = [];
    ko.utils.arrayForEach(values, v => res.push(v !== null ? v.toString() : null));
    return res;
}

const ValueTypesDic = {
    FilterValueType: ''
};

class FilterModel {
    templateNodes: Node[];
    values: ko.ObservableArray<any>;
    value1 = ko.observable(null);
    value2 = ko.observable(null);
    title: ko.Observable<string>;
    options: ko.ObservableArray<IFilterOption>;
    state: ko.Observable<IODataFilter>;

    private _templates:object = {};
    private _name:string;
    private _initialValues:any[];
    private _filterType: ODataFilterTypeEnum;

    constructor(params:IFilterParams) {
        this._name = Utils.randomString(20);
        this._initialValues = params.initialValues || [];
        this._filterType = params.filterType;
        this._defineAllTemplates();
        this.templateNodes = this._getTemplate(params.filterType, params.valueType);

        this.values = /*params.values ||*/ ko.observableArray([]);

        this.state = params.state || ko.observable(null);
        if(this._initialValues.length)
            this.state({ field: params.field, aliases: params.aliases || [], type: params.filterType, values:getFilterStateValues(params.initialValues)});

        this.title = ko.isObservable(params.title) ? params.title : ko.observable(params.title || '');
        this.options = ko.isObservable(params.options) ? params.options : ko.observableArray(params.options || []);

        let values:any[] = this.state() ? this.state().values : [];
        if(params.filterType === ODataFilterTypeEnum.In) {
            values = ko.utils.arrayMap(values, v => ko.utils.arrayFirst(this.options(), opt => opt.value === v));
            this.values(values);
        } else {
            this.values(values);
            this.value1(this.values().length ? this.values()[0] : null);
            this.value2(this.values().length > 1 ? this.values()[1] : null);
        }

        this.values.subscribe(values => {
            if(isStateChanging)
                return;

            if(!values.length) {
                this.state(null);
            } else {
                if(params.filterType === ODataFilterTypeEnum.In) {
                    this.state({
                        field:params.field,
                        aliases:params.aliases || [],
                        type: params.filterType,
                        values: ko.utils.arrayMap(values, (opt:IFilterOption) => opt.value)
                    });
                } else {
                    this.state({
                        field:params.field,
                        aliases:params.aliases || [],
                        type: params.filterType,
                        values: values
                    });
                }
            }
        });

        let isStateChanging = false;
        this.state.subscribe(state => {
            isStateChanging = true;
            console.log('state', state);
            let values:any[] = state ? state.values : [];
            if(this._filterType === ODataFilterTypeEnum.In) {
                values = ko.utils.arrayMap(values, v => ko.utils.arrayFirst(this.options(), opt => opt.value === v));
                this.values(values);
            } else {
                this.values(values);
                this.value1(this.values().length ? this.values()[0] : null);
                this.value2(this.values().length > 1 ? this.values()[1] : null);
            }
            isStateChanging = false;
        });
    }

    private _defineAllTemplates() {
        this._setTemplate(ODataFilterTypeEnum.Contains, 'string',
            '<cmp-textbox params="value:value1"></cmp-textbox>');
        this._setTemplate(ODataFilterTypeEnum.In, 'number',
            '<cmp-multiselect params="values:values, options:options, title:title"></cmp-multiselect>');
        this._setTemplate(ODataFilterTypeEnum.BetweenNone, 'date', this._dateBetweenTemplate);
        this._setTemplate(ODataFilterTypeEnum.BetweenLeft, 'date', this._dateBetweenTemplate);
        this._setTemplate(ODataFilterTypeEnum.BetweenRight, 'date', this._dateBetweenTemplate);
        this._setTemplate(ODataFilterTypeEnum.BetweenAll, 'date', this._dateBetweenTemplate);
        this._setTemplate(ODataFilterTypeEnum.Equals, 'boolean',
            `<div>
                <input type="radio" name="${this._name}" value="true" data-bind="checked:value1" /><span>да</span>
                <input type="radio" name="${this._name}" value="false" data-bind="checked:value1" /><span>нет</span>
                <input type="radio" name="${this._name}" data-bind="checked:value1, value:null" /><span>не определен</span>
            </div>`);

        this.value1.subscribe(this._updateValues.bind(this));
        this.value2.subscribe(this._updateValues.bind(this));
    }

    _dateBetweenTemplate =
        `<div class="date-between">
            <div>
                <div class="date-left"><cmp-datepicker params="value:value1"></cmp-datepicker></div>
                <div class="date-right"><cmp-datepicker params="value:value2"></cmp-datepicker></div>
            </div>
        </div>`;

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