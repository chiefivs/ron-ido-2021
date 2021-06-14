import * as ko from 'knockout';
import { Utils } from '../modules/utils';

export function init(){
    ko.components.register('cmp-radiobox', {
        viewModel: {
            createViewModel: function(params:IRadioBoxParams, componentInfo) {
                return new RadioBoxModel(params, componentInfo);
            }
        },
        template: `
            <div class="form-control" style="height:unset;">
                <table style="width:100%" data-bind="foreach:options">
                    <tr>
                        <td><span data-bind="html:text"></span></td>
                        <td style="text-align:right;">
                            <input type="radio" data-bind="value:value, checked:$parent.value, attr:{name:$parent.name}" />
                        </td>
                    </tr>
                </table>
            </div>`
    });
}

export interface IRadioBoxParams {
    value:ko.Observable<any>;
    options: any[] | ko.ObservableArray<any>;
    disabledOptions?: any[] | ko.ObservableArray<any>;
    optionsText?: string;
    optionsValue?: string;
    readonly?: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    disable?: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    hasFocus?: ko.Observable<boolean>;
    keyDown?: (data:any, event:JQuery.Event) => boolean;
}

class RadioBoxModel {
    value: ko.Observable<any>;
    options: ko.Computed<RadioBoxOption[]>;
    disabledOptions: ko.ObservableArray<any>;
    // optionsText: string;
    // optionsValue: string;
    // optionsCaption: string;
    readonly: ko.Observable<boolean> | ko.Computed<boolean>;
    disable: ko.Observable<boolean> | ko.Computed<boolean>;
    hasFocus: ko.Observable<boolean>;
    name: string;



    private _keyDown: (data:any, event:JQuery.Event) => boolean;

    constructor(params:IRadioBoxParams, componentInfo:any) {
        this.value = params.value;
        this.disabledOptions = ko.isObservable(params.disabledOptions) ? params.disabledOptions : ko.observableArray(params.disabledOptions || []);
        // this.options = ko.isObservable(params.options) ? params.options : ko.observableArray(params.options || []);
        // this.optionsText = params.optionsText || 'text';
        // this.optionsValue = params.optionsValue || '';
        this.options = ko.computed(() => {
            const options = ko.unwrap(params.options);
            return ko.utils.arrayMap(options, o => new RadioBoxOption(o, params));
        });

        this.readonly = ko.isObservable(params.readonly) || ko.isComputed(params.readonly)
            ? params.readonly
            : ko.observable(<boolean>params.readonly || false);
        this.disable = ko.isObservable(params.disable) || ko.isComputed(params.disable)
            ? params.disable
            : ko.observable(<boolean>params.disable || false);
        this.hasFocus = params.hasFocus || ko.observable(false);
        this._keyDown = params.keyDown;

        this.name = Utils.randomString(12);
    }

    // getOptionValue(option:any) {
    //     return this.optionsValue ? option[this.optionsValue] : option;
    // }

    // getOptionText(option:any) {
    //     return option[this.optionsText];
    // }

    // afterRender(option:Element, item:any) {
    //     if(!item) {
    //         $(option).hide();
    //     }
    // }
}

class RadioBoxOption {
    value: any;
    text: string;
    hasFocus: ko.Observable<boolean>;
    disabled: ko.Computed<boolean>;

    constructor(option:any, params:IRadioBoxParams) {
        this.value = params.optionsValue ? option[params.optionsValue] : option,
        this.text = option[params.optionsText || 'text'],
        this.hasFocus = ko.observable(false),
        
        this.disabled = ko.computed(() => {
            return false;
        });
    }
        
    keyDown(data:any, event:JQuery.Event) {
        return true;
    }
}
