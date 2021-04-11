
import * as ko from 'knockout';
import { IODataOption, IODataForm } from '../codegen/webapi/odata';

export class Form<T> {
    item: {[key:string]:IFormField};
    private original: T;
    private errors: {[key:string]:ko.ObservableArray<string>};
    private options: {[key:string]:IODataOption[]};
    private saveApi:(item:T) => JQueryPromise<any>;
    private validateApi:(item:T) => JQueryPromise<{[key:string]:string[]}>

    private _validateTimeout = null;

    constructor(data: IODataForm<T>, saveApi?:(item:T) => JQueryPromise<any>, validateApi?:(item:T) => JQueryPromise<{[key:string]:string[]}>) {
        this.options = data.options || {};
        this.errors = {};
        this.item = {};
        this.original = data.item;
        this.saveApi = saveApi;
        this.validateApi = validateApi;

        for(const key in data.item) {
            this.errors[key] = ko.observableArray([]);

            const val = Array.isArray(data.item[key])
                ? ko.observableArray(<any>data.item[key])
                : ko.observable(data.item[key]);
            this.item[key] = {
                value: val,
                options: this.options[key],
                errors: this.errors[key]
            };

            if(this.validateApi) {
                (val as any).subscribe(v => {
                    if(this._validateTimeout)
                        clearTimeout(this._validateTimeout);
                        this._validateTimeout = setTimeout(this._validate.bind(this), 500);
                });
            }
        }
    }

    get(): T {
        const res = {};
        for(const key in this.item) {
            res[key] = this.item[key].value();
        }

        return res as T;
    }

    update(data: T) {
        for(const key in data) {
            this.item[key].value(data[key]);
        }
    }

    getOptions(key:string) {
        return this.options[key] || null;
    }

    save() {
        if(!this.saveApi)
            return null;
    
        return this.saveApi(this.get()).fail(res => {
            if(res.status === 400) {
                console.log('error', res.responseJSON);

            }
        });
    }

    private _validate() {
        this._validateTimeout = null;
        this.validateApi(this.get())
            .done(errors => {
                for(const key in this.errors) {
                    const list = this.errors[key];
                    list(errors[key] || []);
                }
            });
    }
}

interface IFormField {
    value: ko.Observable<any> | ko.ObservableArray<any>;
    options: IODataOption[];
    errors: ko.ObservableArray<string>;
}