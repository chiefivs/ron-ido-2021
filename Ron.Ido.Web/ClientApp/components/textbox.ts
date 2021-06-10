import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-textbox', {
        viewModel: {
            createViewModel: function(params:ITextBoxParams, componentInfo) {
                return new TextBoxModel(params, componentInfo);
            }
        },
        template: `
            <input class="form-control" type="text" data-bind="textInput: value, event:{keydown:keyDown}, css:css"></input>`
    });
}

export class ITextBoxParams {
    value: ko.Observable<any>;
    hasFocus?: ko.Observable<boolean>;
    css?: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>};
    keyDown?: (data:any, event:JQuery.Event) => boolean;
}

class TextBoxModel {
    value: ko.Observable<any>;
    hasFocus: ko.Observable<boolean>;
    css: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}

    private _input: JQuery;
    private _keyDown: (data:any, event:JQuery.Event) => boolean;

    constructor(params:ITextBoxParams, componentInfo:any) {
        this.value = params.value;
        this.hasFocus = params.hasFocus || ko.observable(false);
        this.css = params.css || {};

        this._input = $('input', componentInfo.element);
        this._keyDown = params.keyDown;
        this._input.on('focus', () => {
            this.hasFocus(true);
        });
        this._input.on('blur', () => {
            this.hasFocus(false);
        });

        this.hasFocus.subscribe(focused => {
            if(focused && !this._input.is(':focus'))
                this._input.trigger('focus');
            else if(!focused && this._input.is(':focus'))
                this._input.trigger('blur');
        });
    }

    keyDown(data, evt) {
        if(this._keyDown)
            return this._keyDown(data, evt);

        return true;
    }
}

