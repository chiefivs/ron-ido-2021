import * as ko from 'knockout';
import { Utils } from '../modules/utils';

export function init(){
    ko.components.register('cmp-multicheckbox', {
        viewModel: {
            createViewModel: function(params:IMultiCheckBoxParams, componentInfo) {
                return new MultiCheckBoxModel(params, componentInfo);
            }
        },
        template: `
            <div class="form-control" style="height:unset;">
                <table style="width:100%" data-bind="foreach:options">
                    <tr>
                        <td><span data-bind="html:text"></span></td>
                        <td style="text-align:right;">
                            <input type="checkbox" data-bind="value:value, checked:$parent.value, hasFocus:hasFocus, event:{keydown:keyDown}" />
                        </td>
                    </tr>
                </table>
            </div>`
    });
}

export interface IMultiCheckBoxParams {
    value:ko.ObservableArray<any>;
    options: any[] | ko.ObservableArray<any>;
    disabledOptions?: any[] | ko.ObservableArray<any>;
    optionsText?: string;
    optionsValue?: string;
    readonly?: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    disable?: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    hasFocus?: ko.Observable<boolean>;
    keyDown?: (data:any, event:JQuery.Event) => boolean;
}

class MultiCheckBoxModel {
    value: ko.Observable<any>;
    options: ko.Computed<MultiCheckBoxOption[]>;
    disabledOptions: ko.ObservableArray<any>;
    readonly: ko.Observable<boolean> | ko.Computed<boolean>;
    disable: ko.Observable<boolean> | ko.Computed<boolean>;
    hasFocus: ko.Observable<boolean>;

    private _keyDown: (data:any, event:JQuery.Event) => boolean;

    constructor(params:IMultiCheckBoxParams, componentInfo:any) {
        this.value = params.value;
        this.disabledOptions = ko.isObservable(params.disabledOptions) ? params.disabledOptions : ko.observableArray(params.disabledOptions || []);
        this.options = ko.computed(() => {
            const options = ko.unwrap(params.options);
            return ko.utils.arrayMap(options, o => new MultiCheckBoxOption(o, params, this));
        });

        this.readonly = ko.isObservable(params.readonly) || ko.isComputed(params.readonly)
            ? params.readonly
            : ko.observable(<boolean>params.readonly || false);
        this.disable = ko.isObservable(params.disable) || ko.isComputed(params.disable)
            ? params.disable
            : ko.observable(<boolean>params.disable || false);
        this.hasFocus = params.hasFocus || ko.observable(false);
        this.hasFocus.subscribe(focused => {
            const options = ko.utils.arrayFilter(this.options(), o => !o.disabled());
            if(!options.length)
                return;

            if(focused) {
                const checkedOption = ko.utils.arrayFirst(options, o => o.value === this.value()) || options[0];
                
                if(!checkedOption.hasFocus())
                    checkedOption.hasFocus(true);
                else
                    checkedOption.hasFocus.valueHasMutated();
            } else {
                ko.utils.arrayForEach(options, o => o.hasFocus(false));
            }
        });

        this._keyDown = params.keyDown;
    }

    isLastOption(option: MultiCheckBoxOption):boolean {
        const options = ko.utils.arrayFilter(this.options(), o => !o.disabled());
        const index = ko.utils.arrayIndexOf(options, option);
        return index >= options.length - 1;
    }
}

class MultiCheckBoxOption {
    value: any;
    text: string;
    hasFocus: ko.Observable<boolean>;
    disabled: ko.Computed<boolean>;

    private _keyDown: (data:any, event:JQuery.Event) => boolean;
    private _parent: MultiCheckBoxModel;

    constructor(option:any, params:IMultiCheckBoxParams, parent:MultiCheckBoxModel) {
        this._parent = parent;
        this.value = params.optionsValue ? option[params.optionsValue] : option;
        this.text = option[params.optionsText || 'text'];
        this.hasFocus = ko.observable(false);

        this._keyDown = params.keyDown;
        
        this.disabled = ko.computed(() => {
            if(!params.disabledOptions)
                return false;

            const disabledOptions = ko.unwrap(params.disabledOptions);
            return !!ko.utils.arrayFirst(disabledOptions, this.value);
        });
    }
        
    keyDown(data:any, event:JQuery.Event) {
        if(this._keyDown && this._parent.isLastOption(this))
            return this._keyDown(data, event);

        return true;
    }
}
