import * as ko from 'knockout';
import { DossierPartBase, IDossier } from './dossier-part-base';
import { Form, IFormField } from '../../../modules/forms';
import { Utils } from '../../../modules/utils';
import { FileData } from '../../../components/index';
import { DossierApi } from '../../../codegen/webapi/dossierApi';
import { IODataForm } from '../../../codegen/webapi/odata';
import { Popups } from '../../../modules/content';

export class Apply extends DossierPartBase implements IApplyFieldBlockHolder {
    form = ko.observable<ApplyForm>();;
    blocks = ko.observableArray<ApplyFieldsBlock>();

    private _getApplyPromise: JQueryPromise<IODataForm<DossierApi.IApplyDto>>

    constructor(id: number, owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html',
            owner: owner
        });

        this.priority = 0;
        this._getApplyPromise = this._loadApply(id);
    }

    afterRender() {
        this._getApplyPromise.done(() => {
            setTimeout(() => (this.form().item.creatorSurname as IApplyFormField).hasFocus(true));
        });
    }

    save() {
        const attachs = ko.utils.arrayFilter(this.form().item.attachments.value() as AttachmentForm[], att => {
            const files:FileData[] = att.item.fileInfo.value();

            return files.length && !!files[0].selection;
        });

        //const files:FileData[] = ko.utils.arrayMap(attachs, att => att.item.fileInfo.value()[0]);

        const saveApply = () => {
            const data = this.form().get();
            console.log('save apply', data);
            DossierApi.saveApply(data)
                .done(id => {
                    Popups.Alert.open('сохранение заявки', 'Заявка успешно сохранена');
                    this._loadApply(id);
                })
                .fail(() => Popups.Alert.open('ошибка сохранения заявки', 'Не удалось сохранить заявку'))
        };

        const saveFile = () => {
            if(!attachs.length) {
                saveApply();
                return;
            }

            const attach = attachs.shift();
            const file:FileData = attach.item.fileInfo.value()[0];
            file.upload('api/storage/upload')
                .done(res => {
                    attach.item.fileInfo.value([new FileData(res[0])]);
                    saveFile();
                });
        };

        saveFile();
    }

    private _loadApply(id:number) {
        return DossierApi.getApply(id).done(data => {
            this.form(new ApplyForm(data));
            this._setBlocks();
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
        const attachments = Utils.getNodesFromFile(`${path}attachments-field.html`);

        const blocks: IApplyFieldsBlockParams[] = [
            {
                title: 'Заявитель (лицо, представляющее интересы обладателя документа об иностранном образовании)',
                blocks: [
                    {
                        title: 'Ф.И.О. на русском языке по паспорту РФ, по переводу документа, удостоверяющего личность, по въездной визе или по карточке регистрации',
                        fields: [
                            this._getApplyField('Фамилия (Лулу)', this.form().item.creatorSurname, textbox),
                            this._getApplyField('фамилия отсутствует', this.form().item.isCreatorSurnameAbsent, checkboxRight),
                            this._getApplyField('Имя (Ван)', this.form().item.creatorFirstName, textbox),
                            this._getApplyField('имя отсутствует', this.form().item.isCreatorFirstNameAbsent, checkboxRight),
                            this._getApplyField('Отчество (-)', this.form().item.creatorLastName, textbox),
                            this._getApplyField('отчество отсутствует', this.form().item.isCreatorLastNameAbsent, checkboxRight),
                            this._getApplyField('Дата рождения (11.12.1987)', this.form().item.creatorBirthDate, datepicker),
                            this._getApplyField('Место рождения', this.form().item.creatorBirthPlace, textbox),
                        ],
                    },
                    {
                        title: 'Гражданство',
                        fields: [
                            this._getApplyField('Гражданство (КНР)', this.form().item.creatorCitizenshipId, select),
                        ]
                    },
                    {
                        title: 'Документ, удостоверяющий личность',
                        fields: [
                            this._getApplyField('Тип документа (Код типа документа)', this.form().item.creatorPassportTypeId, select),
                            this._getApplyField('Реквизиты документа (ВА123131)', this.form().item.creatorPassportReq, textbox),
                            
                        ],
                    },
                    {
                        title: 'По доверенности',
                        fields: [
                            this._getApplyField('(Включить, если заявитель и обладатель документа - разные лица)', this.form().item.byWarrant, checkbox),
                            this._getApplyField('номер доверенности (12-fs452658 US)', this.form().item.warrantReq, textbox),
                            this._getApplyField('Дата доверенности', this.form().item.warrantDate, datepicker),
                            this._getApplyField('Действительна до', this.form().item.warrantTerm, datepicker),
                        ]
                    },
                                
                ]
            },
            {
                title: 'Контактная информация (Адрес доставки) для обратной связи с заявителем',
                fields: [
                    this._getApplyField('Страна жительства (Россия)', this.form().item.creatorCountryId, select),
                    this._getApplyField('Почтовый индекс (603000)', this.form().item.creatorMailIndex, textbox),
                    this._getApplyField('Область, район, населенный пункт', this.form().item.creatorCityName, textbox),
                    this._getApplyField('Улица (пр.Ленина)', this.form().item.creatorStreet, textbox),
                    this._getApplyField('Дом (113-А)', this.form().item.creatorBlock, textbox),
                    this._getApplyField('Квартира (111)', this.form().item.creatorFlat, textbox),
                    this._getApplyField('Корпус (1-А)', this.form().item.creatorCorpus, textbox),
                    this._getApplyField('Строение (2)', this.form().item.creatorBuilding, textbox),
                    this._getApplyField('Телефоны (89200000000)', this.form().item.creatorPhone, textbox),
                    this._getApplyField('Электронная почта (van_1985@gmail.com)', this.form().item.creatorEmail, textbox),
                ],
                blocks: [
                    {
                        title: 'Форма получения результата',
                        fields: [
                            this._getApplyField('', this.form().item.deliveryFormId, radiobox)
                        ]
                    },
                    {
                        title: 'Форма получения электронного свидетельства',
                        fields: [
                            this._getApplyField('', this.form().item.certificateDeliveryForms, multicheckbox)
                        ]
                    },
                    {
                        title: 'Форма получения оригиналов документов',
                        fields: [
                            this._getApplyField('', this.form().item.returnOriginalsFormId, radiobox)
                        ]
                    }
                ]
            },
            {
                title: 'Обладатель документа',
                fields: [
                    this._getApplyField('Фамилия (Хоу)', this.form().item.ownerSurname, textbox),
                    this._getApplyField('фамилия отсутствует', this.form().item.isOwnerSurnameAbsent, checkboxRight),
                    this._getApplyField('Имя (Юйсин)', this.form().item.ownerFirstName, textbox),
                    this._getApplyField('имя отсутствует', this.form().item.isOwnerFirstNameAbsent, checkboxRight),
                    this._getApplyField('Отчество (-)', this.form().item.ownerLastName, textbox),
                    this._getApplyField('отчество отсутствует', this.form().item.isOwnerLastNameAbsent, checkboxRight),
                    this._getApplyField('Дата рождения (11.12.1987)', this.form().item.ownerBirthDate, datepicker),
                    this._getApplyField('Место рождения', this.form().item.ownerBirthPlace, textbox),
                    this._getApplyField('Страна жительства (КНР)', this.form().item.ownerCountryId, select),
                    this._getApplyField('Индекс (131231)', this.form().item.ownerMailIndex, textbox),
                    this._getApplyField('Область, район, населенный пункт', this.form().item.ownerCityName, textbox),
                    this._getApplyField('Улица (ул.Маркстская)', this.form().item.ownerStreet, textbox),
                    this._getApplyField('Дом (113-А)', this.form().item.ownerBlock, textbox),
                    this._getApplyField('Квартира (111)', this.form().item.ownerFlat, textbox),
                    this._getApplyField('Корпус (1-А)', this.form().item.ownerCorpus, textbox),
                    this._getApplyField('Строение (2)', this.form().item.ownerBuilding, textbox),
                    this._getApplyField('Телефоны (89200000000)', this.form().item.ownerPhone, textbox),
                    this._getApplyField('Электронная почта (van_1985@gmail.com)', this.form().item.ownerEmail, textbox),
                    this._getApplyField('Гражданство (КНР)', this.form().item.ownerCitizenshipId, select),
                    this._getApplyField('Тип документа (Код типа документа)', this.form().item.ownerPassportTypeId, select),
                    this._getApplyField('Реквизиты документа (ВА123131)', this.form().item.ownerPassportReq, textbox),
                ]
            },
            {
                title: 'Документ представленный к признанию',
                fields: [
                    this._getApplyField('Страна выдачи (КНР)', this.form().item.docCountryId, select),
                    this._getApplyField('Документ (Документ об основном общем образовании)', this.form().item.docTypeId, select),
                    this._getApplyField('Описание документа', this.form().item.docDescription, textbox),
                    this._getApplyField('Бланк: серия, номер (АВС 123123)', this.form().item.docBlankNum, textbox),
                    this._getApplyField('Рег. номер (-)', this.form().item.docRegNum, textbox),
                    this._getApplyField('Дата выдачи (28.06.2005)', this.form().item.docDate, textbox),
                    this._getApplyField('Приложение, на листах', this.form().item.docAttachmentsCount, textbox),
                    this._getApplyField('ФИО по документу (Козлов Владимир Владимирович )', this.form().item.docFullName, textbox),
                ],
                blocks: [
                    {
                        title: 'Учебное заведение, выдавшее документ',
                        fields: [
                            this._getApplyField('Страна учебного заведения (КНР)', this.form().item.schoolCountryId, select),
                            this._getApplyField('Наименование (Школа иностранных языков "Синьшицзи"', this.form().item.schoolName, textbox),
                            this._getApplyField('Тип учебного заведения', this.form().item.schoolTypeId, select),
                        ]
                    },
                    {
                        title: 'Адрес учебного заведения',
                        fields: [
                            this._getApplyField('Почтовый индекс (9991)', this.form().item.schoolPostIndex, textbox),
                            this._getApplyField('Город (Чэнду)', this.form().item.schoolCityName, textbox),
                            this._getApplyField('Улица, дом (-)', this.form().item.schoolAddress, textbox),
                        ]
                    },
                    {
                        title: 'Контактные данные учебного заведения',
                        fields: [
                            this._getApplyField('Телефон', this.form().item.schoolPhone, textbox),
                            this._getApplyField('Факс', this.form().item.schoolFax, textbox),
                            this._getApplyField('Электронная почта', this.form().item.schoolEmail, textbox),
                        ]
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
                title: 'Сведения о полученном образовании',
                blocks: [
                    {
                        title: 'Период обучения по общеобразовательной программе (аттестат)',
                        fields: [
                            this._getApplyField('С', this.form().item.baseLearnDateBegin, datepicker),
                            this._getApplyField('По', this.form().item.baseLearnDateEnd, datepicker),
                        ]
                    },
                    {
                        title: 'Период обучения по программе(ам) профессионального образования',
                        fields: [
                            this._getApplyField('С', this.form().item.specialLearnDateBegin, datepicker),
                            this._getApplyField('По', this.form().item.specialLearnDateEnd, datepicker),
                        ]
                    },
                    {
                        title: 'Сведения',
                        fields: [
                            this._getApplyField('Направление (специализация)', this.form().item.fixedLearnSpecialityName, textbox),
                            this._getApplyField('Форма обучения', this.form().item.specialLearnFormId, select),
                        ]
                    },
                    {
                        title: 'Цель признания',
                        fields: [
                            this._getApplyField('Цель', this.form().item.aimId, select),
                            this._getApplyField('Организация, запросившая признание', this.form().item.orgCreator, textbox),
                            this._getApplyField('Подробнее...', this.form().item.other, textbox),
                        ]
                    },
                ]
            },
            {
                title: 'Форма приема',
                fields: [
                    this._getApplyField('Форма приема заявления', this.form().item.entryFormId, select),
                    this._getApplyField('ЮВ Украины', this.form().item.isNovorossia, checkbox),
                    this._getApplyField('филиал Ростов', this.form().item.isRostovFilial, checkbox),
                ]
            },
            {
                title: 'Документы по делу',
                fields: [
                    this._getApplyField('', this.form().item.attachments, attachments),
                ],
                blocks: []
            }
        ];

        this.blocks(ko.utils.arrayMap(blocks, b => new ApplyFieldsBlock(b, this)));

        const formItem = this.form().item as {[key:string]:IApplyFormField};
        (formItem.isCreatorSurnameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorSurname));
        (formItem.isCreatorFirstNameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorFirstName));
        (formItem.isCreatorLastNameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorLastName));
        console.log('commit');
        //this.form().commit();
    
        (formItem.byWarrant.value as ko.Observable<boolean>).subscribe(val => {
            formItem.warrantReq.visible(val);
            formItem.warrantDate.visible(val);
            formItem.warrantTerm.visible(val);

            var ownerBlock = formItem.ownerSurname.block;
            if(val) {
                ko.utils.arrayForEach(ownerBlock.fields, f => f.visible(true));
            } else {
                ko.utils.arrayForEach(ownerBlock.fields, f => this.isVisible(false));
            }
        });
        formItem.byWarrant.value.valueHasMutated();
    }

    private _hideField(field:IApplyFormField) {
        field.visible(false);
        field.value(null);
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
        super(data, DossierApi.saveApply, DossierApi.validateApply, {
            'attachments': (apply, applyForm) => {
                const validate = (att: DossierApi.IApplyAttachmentDto) => {
                    applyForm.item.attachments.value.valueHasMutated();
                    return jQuery.Deferred();
                };

                const save = (att: DossierApi.IApplyAttachmentDto) => {
                    return jQuery.Deferred();
                };

                const valueArr = ko.observableArray(ko.utils.arrayMap(<DossierApi.IApplyAttachmentDto[]>data.item.attachments, att => {
                    att.fileInfo = ko.utils.arrayMap(att.fileInfo, fi => new FileData(fi));
                    const formdata: IODataForm<DossierApi.IApplyAttachmentDto> = {
                        item: att,
                        options:{}
                    };
                    const form = new AttachmentForm(formdata, save, validate);
        
                    return form;
                }));

                return {
                    errors:ko.observableArray([]),
                    options: ko.observableArray([]),
                    value: valueArr,
                    hasChanges: ko.computed(() => !!ko.utils.arrayFirst(valueArr(), v => v.hasChanges()))
                };
            }
        });
    }

    get() {
        var apply = super.get();
        var attachments = ko.utils.arrayMap(this.item.attachments.value() as any[], (attForm:Form<DossierApi.IApplyAttachmentDto>) => {
            const files:FileData[] = attForm.item.fileInfo.value();
            const item:DossierApi.IApplyAttachmentDto = {
                id: attForm.item.id.value(),
                attachmentTypeId: attForm.item.attachmentTypeId.value(),
                attachmentTypeName: '',
                description: attForm.item.description.value(),
                required: attForm.item.required.value(),
                given: attForm.item.given.value(),
                error: attForm.item.error.value(),
                fileInfo: ko.utils.arrayMap(files, f => f.getFileInfo())
            };
            
            return item;
        });

        apply.attachments = attachments;
        return apply;
    }
}

class AttachmentForm extends Form<DossierApi.IApplyAttachmentDto> {
    acceptFilesExt = ['.pdf','.png','.docx'];
    fileDesc: ko.Computed<string>;

    private maxSize = 10*1024*1024; //10 MB

    constructor(data: IODataForm<DossierApi.IApplyAttachmentDto>, save:(att: DossierApi.IApplyAttachmentDto) => JQueryPromise<any>, validate:(att: DossierApi.IApplyAttachmentDto) => JQueryPromise<any>){
        super(data, save, validate);

        this.fileDesc = ko.computed(() => {
            const files: FileData[] = this.item.fileInfo.value();
            if(!files.length)
                return '';

            return `${files[0].name} (${files[0].sizeString})`;
        });
    }
    
    deleteFile() {
        this.item.fileInfo.value([]);
    }

    validateSelection(files:FileData[]) {
        if(files[0].size > this.maxSize) {
            Popups.Alert.open('ошибка выбора файла', 'Превышен размер файла');
            return false;
        }

        return true;
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
    parent?: IApplyFieldBlockHolder;
    openPrevBlock?: () => boolean;
    openNextBlock?: () => boolean;
    //focusNextField(field: IApplyFormField);
}

interface IApplyFieldsBlockParams {
    title: string;
    fields?: IApplyFormField[];
    blocks?: IApplyFieldsBlockParams[];
    containerOnly?: boolean;
}

class ApplyFieldsBlock implements IApplyFieldBlockHolder {
    title: string;
    fields: IApplyFormField[];
    blocks = ko.observableArray<ApplyFieldsBlock>();
    parent: IApplyFieldBlockHolder;
    isExpanded = ko.observable(false);
    isVisible: ko.Computed<boolean>;
    afterExpand: () => void;
    containerOnly: boolean;

    constructor(params: IApplyFieldsBlockParams, parent: IApplyFieldBlockHolder) {
        this.parent = parent;
        this.title = params.title;
        this.containerOnly = params.containerOnly || false;
        this.fields = ko.utils.arrayMap(params.fields || [], f => {
            f.block = this;
            f.keyDown = (data:IApplyFormField, event:JQuery.Event) => {
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

    getVisibleFields() {
        return ko.utils.arrayFilter(this.fields, f => f.visible());
    }

    focusPrevField(field: IApplyFormField = null): boolean {
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

        // const visibleChildBlocks = ko.utils.arrayFilter(this.blocks(), b => b.isVisible());
        // if(visibleChildBlocks.length && visibleChildBlocks[0].focusNextField())
        //     return true;

        if(this.openPrevBlock())
            return true;

        return false;
    }

    focusNextField(field: IApplyFormField = null): boolean {
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
        const visibleFields = this.getVisibleFields();
        if(visibleFields.length){
            this.isExpanded(true);
            visibleFields[visibleFields.length - 1].hasFocus(true);
            return true;
        }

        const visibleChildBlocks = ko.utils.arrayFilter(this.blocks(), b => b.isVisible());
        if(visibleChildBlocks.length && visibleChildBlocks[visibleChildBlocks.length - 1].focusLastField())
            return true;

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