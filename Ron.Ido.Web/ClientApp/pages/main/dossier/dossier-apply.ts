import * as ko from 'knockout';
import { DossierPartBase, IDossier } from './dossier-part-base';
import { Form, IFormField } from '../../../modules/forms';
import { DossierApi } from '../../../codegen/webapi/dossierApi';
import { IODataForm } from '../../../codegen/webapi/odata';

export class Apply extends DossierPartBase implements IApplyFieldBlockHolder {
    form: ApplyForm;
    blocks = ko.observableArray<ApplyFieldsBlock>();

    constructor(id: number, owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html',
            owner: owner
        });

        this.priority = 0;

        DossierApi.getApply(id).done(data => {
            this.form = new ApplyForm(data);
            this._setBlocks();
        });
    }

    private _setBlocks() {
        const blocks: IApplyFieldsBlockParams[] = [
            {
                title: 'Заявитель (лицо, представляющее интересы обладателя документа об иностранном образовании)',
                fields: [
                    this._getApplyField('Фамилия (Лулу)', this.form.item.creatorSurname),
                    this._getApplyField('Имя (Ван)', this.form.item.creatorFirstName),
                    this._getApplyField('Отчество (-)', this.form.item.creatorLastName),
                ],
                blocks: [
                    {
                        title: 'Ф.И.О. на русском языке по паспорту РФ, по переводу документа, удостоверяющего личность, по въездной визе или по карточке регистрации',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Гражданство',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'Документ, удостоверяющий личность',
                        fields: [],
                        blocks: []
                    },
                    {
                        title: 'По доверенности',
                        fields: [],
                        blocks: []
                    },
                                
                ]
            },
            {
                title: 'Контактная информация (Адрес доставки) для обратной связи с заявителем',
                fields: [],
                blocks: [
                    {
                        title: 'Форма получения результата',
                        fields: [],
                        blocks: []
                    },
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
    }

    private _getApplyField(title: string, field: IFormField): IApplyFormField {
        const applyField = field as IApplyFormField;
        applyField.title = title;
        applyField.hasFocus = ko.observable(false);
        //applyField.hasFocus.subscribe(focused => console.log(focused, applyField));
        
        return applyField;
    }
}

class ApplyForm extends Form<DossierApi.IApplyDto> {
    constructor(data: IODataForm<DossierApi.IApplyDto>) {
        super(data, DossierApi.validateApply, DossierApi.saveApply);
    }
}

interface IApplyFormField extends IFormField {
    title: string;
    hasFocus: ko.Observable<boolean>;
    keyDown: (data:any, event:JQuery.Event) => boolean;
    readonly: boolean | ko.Observable<boolean> | ko.Computed<boolean>;
    block: ApplyFieldsBlock;
}

interface IApplyFieldBlockHolder {
    blocks: ko.ObservableArray<ApplyFieldsBlock>;
}

interface IApplyFieldsBlockParams {
    title: string;
    fields: IApplyFormField[];
    blocks: IApplyFieldsBlockParams[];
}

class ApplyFieldsBlock implements IApplyFieldBlockHolder {
    title: string;
    fields: IApplyFormField[];
    blocks = ko.observableArray<ApplyFieldsBlock>();
    parent: IApplyFieldBlockHolder;
    isExpanded = ko.observable(false);

    constructor(params: IApplyFieldsBlockParams, parent: IApplyFieldBlockHolder) {
        this.parent = parent;
        this.title = params.title;
        this.fields = ko.utils.arrayMap(params.fields, f => {
            f.block = this;
            f.keyDown = (data:IApplyFormField, event:JQuery.Event) => {
                if(event.key === 'Tab')
                    return false;
    
                if(event.key === 'Enter' && !event.ctrlKey)
                    this._focusNextField(f);
    
                return true;
            }

            return f;
        });
        this.blocks(ko.utils.arrayMap(params.blocks, b => new ApplyFieldsBlock(b, this)));

        this.isExpanded.subscribe(expanded => {
            if(expanded) {
                this.closeBlocksExcludingThis();
                if(this.blocks.length)
                    this.blocks[0].isExpanded(true);
            }
        });

    }

    private closeBlocksExcludingThis() {
        const blocks = ko.utils.arrayFilter(this.parent.blocks(), b => b !== this && b.isExpanded());
        ko.utils.arrayForEach(blocks, g => g.isExpanded(false));
    }

    private _focusNextField(field: IApplyFormField) {
        console.log(field);
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