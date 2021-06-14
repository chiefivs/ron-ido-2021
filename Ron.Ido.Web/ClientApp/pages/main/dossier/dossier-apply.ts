import * as ko from 'knockout';
import { DossierPartBase, IDossier } from './dossier-part-base';
import { Form, IFormField } from '../../../modules/forms';
import { Utils } from '../../../modules/utils';
import { DossierApi } from '../../../codegen/webapi/dossierApi';
import { IODataForm } from '../../../codegen/webapi/odata';

export class Apply extends DossierPartBase implements IApplyFieldBlockHolder {
    form: ApplyForm;
    blocks = ko.observableArray<ApplyFieldsBlock>();

    private _getApplyPromise: JQueryPromise<IODataForm<DossierApi.IApplyDto>>

    constructor(id: number, owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html',
            owner: owner
        });

        this.priority = 0;
        this._getApplyPromise = DossierApi.getApply(id).done(data => {
            this.form = new ApplyForm(data);
            this._setBlocks();
        });
    }

    afterRender() {
        this._getApplyPromise.done(() => {
            setTimeout(() => (this.form.item.creatorSurname as IApplyFormField).hasFocus(true));
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

        const blocks: IApplyFieldsBlockParams[] = [
            {
                title: 'Заявитель (лицо, представляющее интересы обладателя документа об иностранном образовании)',
                blocks: [
                    {
                        title: 'Ф.И.О. на русском языке по паспорту РФ, по переводу документа, удостоверяющего личность, по въездной визе или по карточке регистрации',
                        fields: [
                            this._getApplyField('Фамилия (Лулу)', this.form.item.creatorSurname, textbox),
                            this._getApplyField('фамилия отсутствует', this.form.item.isCreatorSurnameAbsent, checkboxRight),
                            this._getApplyField('Имя (Ван)', this.form.item.creatorFirstName, textbox),
                            this._getApplyField('имя отсутствует', this.form.item.isCreatorFirstNameAbsent, checkboxRight),
                            this._getApplyField('Отчество (-)', this.form.item.creatorLastName, textbox),
                            this._getApplyField('отчество отсутствует', this.form.item.isCreatorLastNameAbsent, checkboxRight),
                            this._getApplyField('Дата рождения (11.12.1987)', this.form.item.creatorBirthDate, datepicker),
                            this._getApplyField('Место рождения', this.form.item.creatorBirthPlace, textbox),
                        ],
                    },
                    {
                        title: 'Гражданство',
                        fields: [
                            this._getApplyField('Гражданство (КНР)', this.form.item.creatorCitizenshipId, select),
                        ]
                    },
                    {
                        title: 'Документ, удостоверяющий личность',
                        fields: [
                            this._getApplyField('Тип документа (Код типа документа)', this.form.item.creatorPassportTypeId, select),
                            this._getApplyField('Реквизиты документа (ВА123131)', this.form.item.creatorPassportReq, textbox),
                            
                        ],
                    },
                    {
                        title: 'По доверенности',
                        fields: [
                            this._getApplyField('(Включить, если заявитель и обладатель документа - разные лица)', this.form.item.byWarrant, checkbox),
                            this._getApplyField('номер доверенности (12-fs452658 US)', this.form.item.warrantReq, textbox),
                            this._getApplyField('Дата доверенности', this.form.item.warrantDate, datepicker),
                            this._getApplyField('Действительна до', this.form.item.warrantTerm, datepicker),
                        ]
                    },
                                
                ]
            },
            {
                title: 'Контактная информация (Адрес доставки) для обратной связи с заявителем',
                fields: [
                    this._getApplyField('Страна жительства (Россия)', this.form.item.creatorCountryId, select),
                    this._getApplyField('Почтовый индекс (603000)', this.form.item.creatorMailIndex, textbox),
                    this._getApplyField('Область, район, населенный пункт', this.form.item.creatorCityName, textbox),
                    this._getApplyField('Улица (пр.Ленина)', this.form.item.creatorStreet, textbox),
                    this._getApplyField('Дом (113-А)', this.form.item.creatorBlock, textbox),
                    this._getApplyField('Квартира (111)', this.form.item.creatorFlat, textbox),
                    this._getApplyField('Корпус (1-А)', this.form.item.creatorCorpus, textbox),
                    this._getApplyField('Строение (2)', this.form.item.creatorBuilding, textbox),
                    this._getApplyField('Телефоны * (89200000000)', this.form.item.creatorPhone, textbox),
                    this._getApplyField('Электронная почта (van_1985@gmail.com)', this.form.item.creatorEmail, textbox),
                    this._getApplyField('Форма получения результата', this.form.item.deliveryFormId, radiobox),

                ],
                blocks: [
                    {
                        title: 'Форма получения электронного свидетельства',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Форма получения оригиналов документов',
                        fields: [],
                        blocks: []
                    }
                ]
            },
            {
                title: 'Обладатель документа',
                fields: [],
                blocks: []
            },
            {
                title: 'Документ представленный к признанию',
                fields: [],
                blocks: [
                    {
                        title: 'Учебное заведение, выдавшее документ',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Адрес учебного заведения',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Контактные данные учебного заведения',
                        fields: [],
                        blocks: []
                    },
                ]
            },
            {
                title: 'Сведения о полученном образовании',
                fields: [],
                blocks: [
                    {
                        title: 'Период обучения по общеобразовательной программе (аттестат)',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Период обучения по программе(ам) профессионального образования',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Сведения',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Цель признания',
                        fields: [],
                        blocks: []
                    }
                ]
            },
            {
                title: 'Форма приема',
                fields: [],
                blocks: []
            },
            {
                title: 'Документы по делу',
                fields: [],
                blocks: []
            }
        ];

        this.blocks(ko.utils.arrayMap(blocks, b => new ApplyFieldsBlock(b, this)));

        const formItem = this.form.item as {[key:string]:IApplyFormField};
        (formItem.isCreatorSurnameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorSurname));
        (formItem.isCreatorFirstNameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorFirstName));
        (formItem.isCreatorLastNameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorLastName));
    
        (formItem.byWarrant.value as ko.Observable<boolean>).subscribe(val => {
            formItem.warrantReq.visible(val);
            formItem.warrantDate.visible(val);
            formItem.warrantTerm.visible(val);
        });
        formItem.byWarrant.value.valueHasMutated();
    }

    private _getApplyField(title: string, field: IFormField, templateNodes: Node[]): IApplyFormField {
        const applyField = field as IApplyFormField;
        applyField.title = title;
        applyField.templateNodes = templateNodes;
        
        return applyField;
    }

    private _setCanAbsentField(absent:boolean, field:IApplyFormField) {
        field.readonly(absent);
        if(absent)
            field.value('');
    }
}

class ApplyForm extends Form<DossierApi.IApplyDto> {
    constructor(data: IODataForm<DossierApi.IApplyDto>) {
        super(data, DossierApi.saveApply, DossierApi.validateApply);
    }
}

interface IApplyFormField extends IFormField {
    title: string;
    templateNodes: Node[];
    hasFocus: ko.Observable<boolean>;
    keyDown: (data:any, event:JQuery.Event) => boolean;
    readonly: ko.Observable<boolean>;
    visible: ko.Observable<boolean>;
    block: ApplyFieldsBlock;
}

interface IApplyFieldBlockHolder {
    blocks: ko.ObservableArray<ApplyFieldsBlock>;
}

interface IApplyFieldsBlockParams {
    title: string;
    fields?: IApplyFormField[];
    blocks?: IApplyFieldsBlockParams[];
}

class ApplyFieldsBlock implements IApplyFieldBlockHolder {
    title: string;
    fields: IApplyFormField[];
    blocks = ko.observableArray<ApplyFieldsBlock>();
    parent: IApplyFieldBlockHolder;
    isExpanded = ko.observable(false);
    isVisible: ko.Computed<boolean>;
    afterExpand: () => void;

    constructor(params: IApplyFieldsBlockParams, parent: IApplyFieldBlockHolder) {
        this.parent = parent;
        this.title = params.title;
        this.fields = ko.utils.arrayMap(params.fields || [], f => {
            f.block = this;
            f.keyDown = (data:IApplyFormField, event:JQuery.Event) => {
                if(event.key === 'Tab') {
                    this._focusNextField(f);
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
        this.blocks(ko.utils.arrayMap(params.blocks || [], b => new ApplyFieldsBlock(b, this)));

        this.isVisible = ko.computed(() => !!ko.utils.arrayFirst(this.fields, f => f.visible()) || !!ko.utils.arrayFirst(this.blocks(), b => b.isVisible()));

        this.isExpanded.subscribe(expanded => {
            if(expanded) {
                this.closeBlocksExcludingThis();
                if(this.blocks.length)
                    this.blocks[0].isExpanded(true);

                const parent = this.parent as ApplyFieldsBlock;
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

    private _focusNextField(field: IApplyFormField) {
        field.hasFocus(false);
        const fieldIndex = ko.utils.arrayIndexOf(this.fields, field);
        if(fieldIndex < this.fields.length - 1) {
            this.fields[fieldIndex + 1].hasFocus(true);
            return;
        }

        const blockIndex = ko.utils.arrayIndexOf(this.parent.blocks(), this);
        if(blockIndex < this.parent.blocks().length - 1) {
            const block = this.parent.blocks()[blockIndex + 1];
            block.isExpanded(true);
            if(block.fields.length)
                block.fields[0].hasFocus(true);

            return;
        }

        return;
    }
}