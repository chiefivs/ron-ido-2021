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

const ValueTypesDic = {
    FilterValueType: ''
};

class FilterModel {
    templateNodes: Node[];
    values = ko.observableArray([]);
    value1 = ko.observable(null);
    value2 = ko.observable(null);
    title: ko.Observable<string>;
    options: ko.ObservableArray<IFilterOption>;
    state: ko.Observable<IODataFilter>;

    private _templates:object = {};
    private _name:string;

    constructor(params:IFilterParams) {
        this._name = Utils.randomString(20);
        this._defineAllTemplates();
        this.templateNodes = this._getTemplate(params.filterType, params.valueType);

        this.state = params.state || ko.observable(null);
        if(params.initialValues)
            this.state({ field: params.field, aliases: params.aliases || [], type: params.filterType, values:this._getStateValues(params.initialValues || [])});

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
    }

    private _defineAllTemplates() {
        this._setTemplate(ODataFilterTypeEnum.Contains, 'string',
            '<cmp-textbox params="value:value1"></cmp-textbox>');
        this._setTemplate(ODataFilterTypeEnum.In, 'number',
            '<cmp-multiselect params="values:values, options:options, title:title"></cmp-multiselect>');
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
        this._setTemplate(ODataFilterTypeEnum.Equals, 'boolean',
            `<div>
                <input type="radio" name="${this._name}" value="true" data-bind="checked:value1" /><span>да</span>
                <input type="radio" name="${this._name}" value="false" data-bind="checked:value1" /><span>нет</span>
                <input type="radio" name="${this._name}" value="" data-bind="checked:value1" /><span>не определен</span>
            </div>`);

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

    private _getStateValues(values:any[]){
        const res = [];
        ko.utils.arrayForEach(values, v => res.push(v.toString()));
        return res;
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