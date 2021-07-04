
import * as ko from 'knockout';
import { IODataOption, IODataForm } from '../codegen/webapi/odata';

export interface IFormField {
    value: ko.Observable<any> | ko.ObservableArray<any>;
    options: ko.ObservableArray<IODataOption>;
    errors: ko.ObservableArray<string>;
    hasChanges: ko.Computed<boolean>;
}

export interface IFormBlockField extends IFormField {
    title: string;
    templateNodes: Node[];
    hasFocus: ko.Observable<boolean>;
    keyDown: (data:any, event:JQuery.Event) => boolean;
    readonly: ko.Observable<boolean>;
    visible: ko.Observable<boolean>;
    block: FormBlock;
}

export interface IFormBlockHolder {
    blocks: ko.ObservableArray<FormBlock>;
    parent?: IFormBlockHolder;
    openPrevBlock?: () => boolean;
    openNextBlock?: () => boolean;
    //focusNextField(field: IApplyFormField);
}

export interface IFormBlockParams {
    title: string;
    fields?: IFormBlockField[];
    blocks?: IFormBlockParams[];
    containerOnly?: boolean;
}

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
                    return true;
                }
            }

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
}

export class FormBlock implements IFormBlockHolder {
    title: ko.Computed<string>;
    fields = ko.observableArray<IFormBlockField>();
    blocks = ko.observableArray<FormBlock>();
    parent: IFormBlockHolder;
    isExpanded = ko.observable(false);
    isVisible: ko.Computed<boolean>;
    hasErrors: ko.Computed<boolean>;
    afterExpand: () => void;
    containerOnly: boolean;

    private _title: string;

    constructor(params: IFormBlockParams, parent: IFormBlockHolder) {
        this.parent = parent;
        this._title = params.title;
        this.containerOnly = params.containerOnly || false;
        this.fields(ko.utils.arrayMap(params.fields || [], f => {
            f.block = this;
            f.keyDown = (data:IFormBlockField, event:JQuery.Event) => {
                if(event.key === 'Tab') {
                    if(event.shiftKey){
                        this.focusPrevField(f);
                    } else {
                        this.focusNextField(f);
                    }

                    return false;
                }
    
                return true;
            }

            f.readonly = ko.observable(false);
            f.visible = ko.observable(true);
            f.hasFocus = ko.observable(false);
            f.hasFocus.subscribe(has => { 
                if(has){
                    this.isExpanded(true);
                } 
             });

            return f;
        }));
        this.blocks(ko.utils.arrayMap(params.blocks || [], b => {
            const block = new FormBlock(b, this);
            return block;
        }));

        this.isExpanded.subscribe(expanded => {
            if(expanded) {
                this.closeBlocksExcludingThis();
                if(this.blocks.length)
                    this.blocks[0].isExpanded(true);

                const parent = this.parent as FormBlock;
                if(parent.isExpanded)
                    parent.isExpanded(true);
            }
        });

        this.afterExpand = () => {
            ko.utils.arrayForEach(this.fields(), f => f.hasFocus.valueHasMutated());
            ko.utils.arrayForEach(this.blocks(), b => b.afterExpand());
        };

        this.hasErrors = ko.computed(() => !!ko.utils.arrayFirst(this.fields(), f => !!f.errors().length) || !!ko.utils.arrayFirst(this.blocks(), b => b.hasErrors()));

        this.title = ko.computed(() => {
            if(!this.hasErrors())
                return this._title;

            return `<span class="has-errors">${this._title}</span>`;
        });

        this.isVisible = ko.computed(() => {
            const visible = !!ko.utils.arrayFirst(this.fields(), f => f.visible()) || !!ko.utils.arrayFirst(this.blocks(), b => b.isVisible());
            return visible;
        });
    }

    private closeBlocksExcludingThis() {
        const blocks = ko.utils.arrayFilter(this.parent.blocks(), b => b !== this && b.isExpanded());
        ko.utils.arrayForEach(blocks, g => g.isExpanded(false));
    }

    getVisibleFields() {
        return ko.utils.arrayFilter(this.fields(), f => f.visible());
    }

    focusPrevField(field: IFormBlockField = null): boolean {
        if(field)
            field.hasFocus(false);
        
        const visibleFields = this.getVisibleFields();
        const fieldIndex = ko.utils.arrayIndexOf(visibleFields, field);
        if(fieldIndex > 0) {
            //  есть предыдущее поле
            this.isExpanded(true);
            visibleFields[fieldIndex - 1].hasFocus(true);
            return true;
        }

        if(this.openPrevBlock())
            return true;

        return false;
    }

    focusNextField(field: IFormBlockField = null): boolean {
        if(field)
            field.hasFocus(false);
        
        const visibleFields = this.getVisibleFields();
        const fieldIndex = ko.utils.arrayIndexOf(visibleFields, field);
        if(fieldIndex < visibleFields.length - 1) {
            //  есть следующее поле
            this.isExpanded(true);
            visibleFields[fieldIndex + 1].hasFocus(true);
            return true;
        }

        const visibleChildBlocks = ko.utils.arrayFilter(this.blocks(), b => b.isVisible());
        if(visibleChildBlocks.length && visibleChildBlocks[0].focusNextField())
            return true;

        if(this.openNextBlock())
            return true;

        return false;
    }

    focusLastField(): boolean {
        const visibleChildBlocks = ko.utils.arrayFilter(this.blocks(), b => b.isVisible());
        if(visibleChildBlocks.length && visibleChildBlocks[visibleChildBlocks.length - 1].focusLastField()) {
            this.isExpanded(true);
            return true;
        }

        const visibleFields = this.getVisibleFields();
        if(visibleFields.length){
            this.isExpanded(true);
            visibleFields[visibleFields.length - 1].hasFocus(true);
            return true;
        }

        if(this.openPrevBlock())
            return true;

        return false;
    }

    openPrevBlock() {
        if(!this.parent)
            return false;

        const parentBlock = this.parent;
        const visibleSiblingBlocks = ko.utils.arrayFilter(parentBlock.blocks(), b => b.isVisible());
        const blockIndex = ko.utils.arrayIndexOf(visibleSiblingBlocks, this);

        if(blockIndex > 0 && visibleSiblingBlocks[blockIndex - 1].focusLastField())
            return true;

        if(blockIndex === 0) {
            const parent = this.parent as FormBlock;
            const visibleParentFields = parent.getVisibleFields ? parent.getVisibleFields() : [];
            if(visibleParentFields.length) {
                parent.isExpanded(true);
                visibleParentFields[visibleParentFields.length - 1].visible(true);
                return true;
            }
        }

        if(this.parent.openPrevBlock && this.parent.openPrevBlock())
            return true;
    
        return false;
    }

    openNextBlock() {
        if(!this.parent)
            return false;

        const parentBlock = this.parent;
        const visibleSiblingBlocks = ko.utils.arrayFilter(parentBlock.blocks(), b => b.isVisible());
        const blockIndex = ko.utils.arrayIndexOf(visibleSiblingBlocks, this);
        if(blockIndex < visibleSiblingBlocks.length - 1 && visibleSiblingBlocks[blockIndex + 1].focusNextField())
            return true;

        if(this.parent.openNextBlock && this.parent.openNextBlock())
            return true;
    
        return false;
    }
}