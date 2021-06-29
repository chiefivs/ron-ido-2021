
import * as ko from 'knockout';
import { IODataOption, IODataForm } from '../codegen/webapi/odata';

export class Form<T> {
    item: {[key:string]:IFormField};
    errors = ko.observableArray<string>();
    hasChanges: ko.Computed<boolean>;
    hasErrors: ko.Computed<boolean>;

    original: ko.Observable<T>;
    errorsDic: {[key:string]:ko.ObservableArray<string>};
    optionsDic: {[key:string]:ko.ObservableArray<IODataOption>};
    
    private _saveApi:(item:T) => JQueryPromise<any>;
    private _validateApi:(item:T) => JQueryPromise<{[key:string]:string[]}>
    private _fieldsFactory:{[key:string]:(item:T, form:Form<T>) => IFormField};

    private _validateTimeout = null;

    constructor(
        data: IODataForm<T>,
        saveApi?:(item:T) => JQueryPromise<any>,
        validateApi?:(item:T) => JQueryPromise<{[key:string]:string[]}>,
        fieldsFactory?:{[key:string]:(item:T, form:Form<T>) => IFormField}) {
        this._fieldsFactory = fieldsFactory;
        this.optionsDic = {};
        if(data.options) {
            for(const key in data.options) {
                this.optionsDic[key] = ko.observableArray(data.options[key]);
            }
        }

        this.errorsDic = {};
        this.original = ko.observable( data.item);

        this.item = {};
        this._saveApi = saveApi;
        this._validateApi = validateApi;

        for(const key in data.item) {
            this.errorsDic[key] = ko.observableArray([]);

            this.item[key] = this._fieldsFactory && this._fieldsFactory[key]
            ? this._fieldsFactory[key](data.item, this)
            : this._createFieldDefault(data.item, key)

            if(this._validateApi) {
                (this.item[key].value as any).subscribe(v => {
                    if(this._validateTimeout)
                        clearTimeout(this._validateTimeout);
                        this._validateTimeout = setTimeout(this._validate.bind(this), 500);
                });
            }
        }

        this.hasChanges = ko.computed(() => {
            for(var key in this.item) {
                if(this.item[key].hasChanges()){
                    console.log('form has changes', key);
                    return true;
                }
            }

            console.log('form has no changes');
            return false;
        });

        this.hasErrors = ko.computed(() => {
            if(this.errors().length)
                return true;

            for(const key in this.errorsDic) {
                if(this.errorsDic[key]().length)
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
            const newField = this._fieldsFactory && this._fieldsFactory[key]
            ? this._fieldsFactory[key](data, this)
            : this._createFieldDefault(data, key)

            this.item[key].value(newField.value());
        }
    }

    getOptions(key:string) {
        return this.optionsDic[key] || null;
    }

    setOptions(key:string, options:IODataOption[]) {
        this.optionsDic[key](options);
    }

    commit() {
        this.original(this.get());
    }

    reset() {
        const orig = this.original();
        for(const key in this.item) {
            const newField = this._fieldsFactory && this._fieldsFactory[key]
            ? this._fieldsFactory[key](orig, this)
            : this._createFieldDefault(orig, key)

            this.item[key].value(newField.value());
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
    
    private _createFieldDefault(data:T, key:string): IFormField {
        const value = Array.isArray(data[key])
        ? ko.observableArray(ko.utils.arrayMap(<any>data[key], i => i))
        : ko.observable(data[key]);

        var field: IFormField = {
            value: value,
            options: this.optionsDic[key],
            errors: this.errorsDic[key],
            hasChanges: ko.computed(() => {
                const original = this.original()[key];
                if(!Array.isArray(original)) {
                    const v = value() !== undefined ? value() : null;
                    if(v !== original)
                        console.log('item has changes', key, v, original);

                    return v !== original;
                } else {
                    const diff = ko.utils.compareArrays((original as any[]).sort(), (value() as any[]).sort());
                    return !!ko.utils.arrayFirst(diff, d => d.status === 'added' || d.status === 'deleted');
                }
            })
        };

        return field;
    }

    private _validate() {
        this._validateTimeout = null;
        const data = this.get();
        this._validateApi(data)
            .done(errors => {
                console.log('form validate', data, errors);
                this.errors(errors[''] || []);
                for(const key in this.errorsDic) {
                    const list = this.errorsDic[key];
                    list(errors[key] || []);
                }
            });
    }
    
    private _fieldHasChanges(key:string) {
        console.log('_fieldHasChanges', this);
        const val = this.item[key].value();
        const org = this.original()[key];
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