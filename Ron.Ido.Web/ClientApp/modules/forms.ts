
import * as ko from 'knockout';
import { IODataOption, IODataForm } from '../codegen/webapi/odata';

export class Form<T> {
    item: {[key:string]:IFormField};
    errors = ko.observableArray<string>();
    hasChanges: ko.Computed<boolean>;
    hasErrors: ko.Computed<boolean>;

    private _original: T;
    private _errors: {[key:string]:ko.ObservableArray<string>};
    private _options: {[key:string]:ko.ObservableArray<IODataOption>};
    private _saveApi:(item:T) => JQueryPromise<any>;
    private _validateApi:(item:T) => JQueryPromise<{[key:string]:string[]}>

    private _validateTimeout = null;

    constructor(data: IODataForm<T>, saveApi?:(item:T) => JQueryPromise<any>, validateApi?:(item:T) => JQueryPromise<{[key:string]:string[]}>) {
        this._options = {};
        if(data.options) {
            for(const key in data.options) {
                this._options[key] = ko.observableArray(data.options[key]);
            }
        }

        this._errors = {};
        this._original = data.item;

        this.item = {};
        this._saveApi = saveApi;
        this._validateApi = validateApi;

        for(const key in data.item) {
            this._errors[key] = ko.observableArray([]);

            const value = Array.isArray(data.item[key])
                ? ko.observableArray(ko.utils.arrayMap(<any>data.item[key], i => i))
                : ko.observable(data.item[key]);
            
            const original = data.item[key];
            this.item[key] = {
                value: value,
                options: this._options[key],
                errors: this._errors[key],
                hasChanges: ko.computed(() => {
                    if(!Array.isArray(original)) {
                        return value() !== original;
                    } else {
                        const diff = ko.utils.compareArrays((original as any[]).sort(), (value() as any[]).sort());
                        return !!ko.utils.arrayFirst(diff, d => d.status === 'added' || d.status === 'deleted');
                    }
                })
            };

            if(this._validateApi) {
                (value as any).subscribe(v => {
                    if(this._validateTimeout)
                        clearTimeout(this._validateTimeout);
                        this._validateTimeout = setTimeout(this._validate.bind(this), 500);
                });
            }
        }

        this.hasChanges = ko.computed(() => {
            for(var key in this.item) {
                if(this.item[key].hasChanges())
                    return true;
            }

            return false;
        });

        this.hasErrors = ko.computed(() => {
            if(this.errors().length)
                return true;

            for(const key in this._errors) {
                if(this._errors[key]().length)
                    return true;
            }

            return false;
        });
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
        return this._options[key] || null;
    }

    setOptions(key:string, options:IODataOption[]) {
        this._options[key](options);
    }

    reset() {
        for(const key in this.item) {
            const orig = this._original[key];
            this.item[key].value(Array.isArray(orig)
                ? ko.utils.arrayMap(orig, i => i)
                : orig);
        }
    }

    save() {
        if(!this._saveApi || !this.hasChanges())
            return jQuery.Deferred();
    
        return this._saveApi(this.get()).fail(res => {
            if(res.status === 400) {
                console.log('error', res.responseJSON);

            }
        });
    }

    private _validate() {
        this._validateTimeout = null;
        this._validateApi(this.get())
            .done(errors => {
                this.errors(errors[''] || []);
                for(const key in this._errors) {
                    const list = this._errors[key];
                    list(errors[key] || []);
                }
            });
    }
    
    private _fieldHasChanges(key:string) {
        console.log('_fieldHasChanges', this);
        const val = this.item[key].value();
        const org = this._original[key];
        if(!Array.isArray(org)) {
            return val !== org;
        } else {
            const diff = ko.utils.compareArrays((org as any[]).sort(), (val as any[]).sort());
            return !!ko.utils.arrayFirst(diff, d => d.status === 'added' || d.status === 'deleted');
        }
    }
}

export interface IFormField {
    value: ko.Observable<any> | ko.ObservableArray<any>;
    options: ko.ObservableArray<IODataOption>;
    errors: ko.ObservableArray<string>;
    hasChanges: ko.Computed<boolean>;
}