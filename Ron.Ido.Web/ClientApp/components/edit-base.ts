import * as ko from 'knockout';

export interface IEditBaseParams {
    readonly?: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    disable?: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    hasFocus?: ko.Observable<boolean>;
    css?: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>};
    keyDown?: (data:any, event:JQuery.Event) => boolean;
}

export class EditBaseModel {
    readonly: ko.Observable<boolean> | ko.Computed<boolean>;
    disable: ko.Observable<boolean> | ko.Computed<boolean>;
    hasFocus: ko.Observable<boolean>;
    css: { [key:string]:ko.Observable<boolean>|ko.Computed<boolean>}

    private _keyDown: (data:any, event:JQuery.Event) => boolean;

    constructor(params:IEditBaseParams) {
        this.readonly = ko.isObservable(params.readonly) || ko.isComputed(params.readonly)
            ? params.readonly
            : ko.observable(<boolean>params.readonly || false);
        this.disable = ko.isObservable(params.disable) || ko.isComputed(params.disable)
            ? params.disable
            : ko.observable(<boolean>params.disable || false);
        this.hasFocus = params.hasFocus || ko.observable(false);
        this.css = params.css || {};

        this._keyDown = params.keyDown;
    }

    keyDown(data, evt) {
        if(this._keyDown)
            return this._keyDown(data, evt);

        return true;
    }
}
