import * as ko from 'knockout';
import { DossierPartBase, IDossier } from '../dossier/dossier-part-base';
import { Form, IFormField } from '../../../modules/forms';
import { Utils } from '../../../modules/utils';
import { DuplicatesSearchApi } from '../../../codegen/webapi/duplicatesSearchApi';
import { IODataForm } from '../../../codegen/webapi/odata';
import { Control, IControlParams } from '../../../modules/content';

export interface IDuplicate {
    id: number;
    parts: ko.ObservableArray<DuplicatePartBase>;
}

export abstract class DuplicatePartBase extends Control {
    isVisible = ko.observable(false);
    priority: number = 0;

    private _owner: IDuplicate;

    constructor(params: IDuplicatePartBaseParams) {
        super(params);
        this._owner = params.owner;
    }

    afterRender(nodes:Node[]) {
        // этот метод можно переопределять в наследниках
    }

    close(): boolean {
        this._owner.parts.remove(this);
        return true;
    }

    width(): string {
        return `${this._round(this._getWidth())}%`;
    }

    left(index:number): string {
        return `${this._round(this._getWidth() * index)}%`;
    }

    private _getWidth() {
        return Math.min(50, 100 / this._owner.parts().length);
    }

    private _round(val: number) {
        return Math.round(100 * val) / 100;
    }
}
export interface IDuplicatePartBaseParams extends IControlParams {
    owner: IDuplicate;
}


export class Duplicate extends DuplicatePartBase implements IDuplicateFieldBlockHolder {
    form: DuplicateForm;
    blocks = ko.observableArray<DuplicateFieldsBlock>();

    private _getDuplicatePromise: JQueryPromise<IODataForm<DuplicatesSearchApi.IDuplicateDto>>

    constructor(id: number, owner: IDuplicate) {
        super({
            templatePath: 'pages/main/duplicates/duplicate-main.html',
            owner: owner
        });

        this.priority = 0;
        this._getDuplicatePromise = DuplicatesSearchApi.getDuplicate(id).done(data => {
            this.form = new DuplicateForm(data);
            this._setBlocks();
        });
    }

    afterRender() {
        this._getDuplicatePromise.done(() => {
            setTimeout(() => (this.form.item.fullName as IDuplicateFormField).hasFocus(true));
        });
    }

    private _setBlocks() {
        const path = 'pages/main/dossier/field-templates/';
        const textbox = Utils.getNodesFromFile(`${path}textbox-field.html`);
        const datepicker = Utils.getNodesFromFile(`${path}datepicker-field.html`);
        const checkbox = Utils.getNodesFromFile(`${path}checkbox-field.html`);
        const checkboxRight = Utils.getNodesFromFile(`${path}checkbox-field-right.html`);
        const select = Utils.getNodesFromFile(`${path}select-field.html`);
        const radiobox = Utils.getNodesFromFile(`${path}radiobox-field.html`);
        const multicheckbox = Utils.getNodesFromFile(`${path}multicheckbox-field.html`);

        const blocks: IDuplicateFieldsBlockParams[] = [
            {
                title: 'Дубликат',
                blocks: [
                    {
                        title: 'Вся информация кучей',
                        fields: [
                            this._getDuplicateReadonlyField('Дата создания заявления', this.form.item.createTime, textbox),
                            this._getDuplicateReadonlyField('Дата выдачи', this.form.item.handoutTime, textbox),
                            this._getDuplicateField('Место в накопителе', this.form.item.storage, textbox),

                            this._getDuplicateField('Фамилия, Имя, Отчество подавшего заявку', this.form.item.fullName, textbox),

                            this._getDuplicateField('Страна жительства', this.form.item.creatorCountryId, select),
                            this._getDuplicateField('Почтовый индекс', this.form.item.mailIndex, textbox),

                            this._getDuplicateField('Область, район, населенный пункт', this.form.item.cityName, textbox),
                            this._getDuplicateField('Адрес', this.form.item.address, textbox),
                            this._getDuplicateField('Улица', this.form.item.street, textbox),
                            this._getDuplicateField('Дом', this.form.item.block, textbox),
                            this._getDuplicateField('Квартира', this.form.item.flat, textbox),
                            this._getDuplicateField('Корпус', this.form.item.building, textbox),
                            this._getDuplicateField('Строение', this.form.item.corpus, textbox),

                            this._getDuplicateReadonlyField('Статус', this.form.item.statusId, select),
                            this._getDuplicateField('E-mail адрес', this.form.item.email, textbox),
                            this._getDuplicateField('Телефон', this.form.item.phones, textbox),
                            this._getDuplicateField('Примечание', this.form.item.note, textbox),

                            this._getDuplicateReadonlyField('Фамилия, Имя, Отчество по документу', this.form.item.docFullName, textbox),
                            this._getDuplicateReadonlyField('Дата выдачи документа', this.form.item.documentDate, textbox),
                            this._getDuplicateReadonlyField('Название ИДО', this.form.item.documentTypeId, select),
                            this._getDuplicateReadonlyField('Страна выдачи ИДО', this.form.item.docCountryId, select),
                            this._getDuplicateReadonlyField('Наименование учебного заведения', this.form.item.schoolName, textbox),


                        ],
                    },/*
                    {
                        title: 'Гражданство',
                        fields: [
                            this._getDuplicateField('Гражданство (КНР)', this.form.item.creatorCitizenshipId, select),
                        ]
                    },
                    {
                        title: 'Документ, удостоверяющий личность',
                        fields: [
                            this._getDuplicateField('Тип документа (Код типа документа)', this.form.item.creatorPassportTypeId, select),
                            this._getDuplicateField('Реквизиты документа (ВА123131)', this.form.item.creatorPassportReq, textbox),
                            
                        ],
                    },*/
                                
                ]
            },
            {
                title: 'Форма приема',
                fields: [
                    this._getDuplicateField('Форма приема заявления', this.form.item.returnOriginalsFormId, select),
                ]
            },
            {
                title: 'Документы по делу',
                fields: [],
                blocks: []
            }
        ];

        this.blocks(ko.utils.arrayMap(blocks, b => new DuplicateFieldsBlock(b, this)));

    }

    private _hideField(field:IDuplicateFormField) {
        field.visible(false);
        field.value(null);
    }

    private _getDuplicateField(title: string, field: IFormField, templateNodes: Node[]): IDuplicateFormField {
        const duplicateField = field as IDuplicateFormField;
        if ( !duplicateField ) {
            console.warn(`{title} no field`);
            return duplicateField;
        }
        duplicateField.title = title;
        duplicateField.templateNodes = templateNodes;
        
        return duplicateField;
    }

    private _getDuplicateReadonlyField(title: string, field: IFormField, templateNodes: Node[]): IDuplicateFormField {
        const duplicateField = field as IDuplicateFormField;
        if ( !duplicateField ) {
            console.warn(`{title} no field`);
            return duplicateField;
        }
        duplicateField.title = title;
        
        duplicateField.templateNodes = templateNodes;
        if (duplicateField.readonly)
            duplicateField.readonly(true);
        else
            duplicateField.readonly = ko.observable( true );
        
        return duplicateField;
    }

    private _setCanAbsentField(absent:boolean, field:IDuplicateFormField) {
        field.readonly(absent);
        if(absent)
            field.value('');
    }
}

class DuplicateForm extends Form<DuplicatesSearchApi.IDuplicateDto> {
    constructor(data: IODataForm<DuplicatesSearchApi.IDuplicateDto>) {
        super(data, DuplicatesSearchApi.saveDuplicate, DuplicatesSearchApi.validateDuplicate);
    }
}

interface IDuplicateFormField extends IFormField {
    title: string;
    templateNodes: Node[];
    hasFocus: ko.Observable<boolean>;
    keyDown: (data:any, event:JQuery.Event) => boolean;
    readonly: ko.Observable<boolean>;
    visible: ko.Observable<boolean>;
    block: DuplicateFieldsBlock;
}

interface IDuplicateFieldBlockHolder {
    blocks: ko.ObservableArray<DuplicateFieldsBlock>;
    parent?: IDuplicateFieldBlockHolder;
    openNextBlock?: () => boolean;
    //focusNextField(field: IDuplicateFormField);
}

interface IDuplicateFieldsBlockParams {
    title: string;
    fields?: IDuplicateFormField[];
    blocks?: IDuplicateFieldsBlockParams[];
    containerOnly?: boolean;
}

class DuplicateFieldsBlock implements IDuplicateFieldBlockHolder {
    title: string;
    fields: IDuplicateFormField[];
    blocks = ko.observableArray<DuplicateFieldsBlock>();
    parent: IDuplicateFieldBlockHolder;
    isExpanded = ko.observable(false);
    isVisible: ko.Computed<boolean>;
    afterExpand: () => void;
    containerOnly: boolean;

    constructor(params: IDuplicateFieldsBlockParams, parent: IDuplicateFieldBlockHolder) {
        this.parent = parent;
        this.title = params.title;
        this.containerOnly = params.containerOnly || false;
        this.fields = ko.utils.arrayMap(params.fields || [], f => {
            f.block = this;
            f.keyDown = (data:IDuplicateFormField, event:JQuery.Event) => {
                console.log(data, event.key);
                if(event.key === 'Tab') {
                    this.focusNextField(f);
                    return false;
                }
    
                return true;
            }

            f.readonly = ko.observable(false);
            f.hasFocus = ko.observable(false);
            f.visible = ko.observable(true);
            f.hasFocus.subscribe(has => { if(has) this.isExpanded(true); });
            return f;
        });
        this.blocks(ko.utils.arrayMap(params.blocks || [], b => new DuplicateFieldsBlock(b, this)));

        this.isVisible = ko.computed(() => !!ko.utils.arrayFirst(this.fields, f => f.visible()) || !!ko.utils.arrayFirst(this.blocks(), b => b.isVisible()));

        this.isExpanded.subscribe(expanded => {
            if(expanded) {
                this.closeBlocksExcludingThis();
                if(this.blocks.length)
                    this.blocks[0].isExpanded(true);

                const parent = this.parent as DuplicateFieldsBlock;
                if(parent.isExpanded)
                    parent.isExpanded(true);
            }
        });

        this.afterExpand = () => {
            ko.utils.arrayForEach(this.fields, f => f.hasFocus.valueHasMutated());
        };
    }

    private closeBlocksExcludingThis() {
        const blocks = ko.utils.arrayFilter(this.parent.blocks(), b => b !== this && b.isExpanded());
        ko.utils.arrayForEach(blocks, g => g.isExpanded(false));
    }

    getVisibleFields() {
        return ko.utils.arrayFilter(this.fields, f => f.visible());
    }

    focusNextField(field: IDuplicateFormField = null): boolean {
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