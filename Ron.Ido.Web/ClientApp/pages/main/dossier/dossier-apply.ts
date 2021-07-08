import * as ko from 'knockout';
import { DossierPartBase, IDossier } from './dossier-part-base';
import { Form, IFormField, IFormBlockHolder, IFormBlockField, IFormBlockParams, FormBlock } from '../../../modules/forms';
import { Utils } from '../../../modules/utils';
import { FileData } from '../../../components/index';
import { DossierApi } from '../../../codegen/webapi/dossierApi';
import { IODataForm } from '../../../codegen/webapi/odata';
import { Popups } from '../../../modules/content';
import { WebApi } from '../../../modules/webapi';

export class Apply extends DossierPartBase implements IFormBlockHolder {
    form = ko.observable<ApplyForm>();;
    blocks = ko.observableArray<FormBlock>();

    private _getApplyPromise: JQueryPromise<IODataForm<DossierApi.IApplyDto>>

    constructor(id: number, owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html',
            owner: owner
        });

        this.priority = 0;
        this._getApplyPromise = this._loadApply(id);
    }

    private isLoaded = false;
    afterRender() {
        if(this.isLoaded)
            return;

        this._getApplyPromise.done(() => {
            setTimeout(() => (this.form().item.creatorSurname as IFormBlockField).hasFocus(true));
            this.isLoaded = true;
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
            DossierApi.saveApply(data)
                .done(dossier => {
                    Popups.Alert.open('сохранение заявки', 'Заявка успешно сохранена');
                    this._loadApply(dossier.apply.id);
                    this._owner.update(dossier);
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

    addAttachment() {
        this.form().addAttachment();
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

        const blocks: IFormBlockParams[] = [
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
                blocks: [
                    {
                        title: 'Ф.И.О. на русском языке по паспорту РФ, по переводу документа, удостоверяющего личность, по въездной визе или по карточке регистрации',
                        fields: [
                            this._getApplyField('Фамилия (Хоу)', this.form().item.ownerSurname, textbox),
                            this._getApplyField('фамилия отсутствует', this.form().item.isOwnerSurnameAbsent, checkboxRight),
                            this._getApplyField('Имя (Юйсин)', this.form().item.ownerFirstName, textbox),
                            this._getApplyField('имя отсутствует', this.form().item.isOwnerFirstNameAbsent, checkboxRight),
                            this._getApplyField('Отчество (-)', this.form().item.ownerLastName, textbox),
                            this._getApplyField('отчество отсутствует', this.form().item.isOwnerLastNameAbsent, checkboxRight),
                            this._getApplyField('Дата рождения (11.12.1987)', this.form().item.ownerBirthDate, datepicker),
                            this._getApplyField('Место рождения', this.form().item.ownerBirthPlace, textbox),
                        ]
                    },
                    {
                        title: 'Гражданство',
                        fields: [
                            this._getApplyField('Гражданство (КНР)', this.form().item.ownerCitizenshipId, select),
                        ]
                    },
                    {
                        title: 'Место жительства',
                        fields: [
                            this._getApplyField('Страна жительства (КНР)', this.form().item.ownerCountryId, select),
                            this._getApplyField('Индекс (131231)', this.form().item.ownerMailIndex, textbox),
                            this._getApplyField('Область, район, населенный пункт', this.form().item.ownerCityName, textbox),
                            this._getApplyField('Улица (ул.Маркстская)', this.form().item.ownerStreet, textbox),
                            this._getApplyField('Дом (113-А)', this.form().item.ownerBlock, textbox),
                            this._getApplyField('Квартира (111)', this.form().item.ownerFlat, textbox),
                            this._getApplyField('Корпус (1-А)', this.form().item.ownerCorpus, textbox),
                            this._getApplyField('Строение (2)', this.form().item.ownerBuilding, textbox),
                        ]
                    },
                    {
                        title: 'Контактные данные',
                        fields: [
                            this._getApplyField('Телефоны (89200000000)', this.form().item.ownerPhone, textbox),
                            this._getApplyField('Электронная почта (van_1985@gmail.com)', this.form().item.ownerEmail, textbox),
                        ]
                    },
                    {
                        title: 'Документ, удостоверяющий личность',
                        fields: [
                            this._getApplyField('Тип документа (Код типа документа)', this.form().item.ownerPassportTypeId, select),
                            this._getApplyField('Реквизиты документа (ВА123131)', this.form().item.ownerPassportReq, textbox),
                        ]
                    }
                ],
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

        this.blocks(ko.utils.arrayMap(blocks, b => new FormBlock(b, this)));

        const formItem = this.form().item as {[key:string]:IFormBlockField};
        (formItem.isCreatorSurnameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorSurname));
        (formItem.isCreatorFirstNameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorFirstName));
        (formItem.isCreatorLastNameAbsent.value as ko.Observable<boolean>).subscribe(checked => this._setCanAbsentField(checked, formItem.creatorLastName));
        //this.form().commit();
    
        (formItem.byWarrant.value as ko.Observable<boolean>).subscribe(val => {
            formItem.warrantReq.visible(val);
            formItem.warrantDate.visible(val);
            formItem.warrantTerm.visible(val);

            const ownerBlock = formItem.ownerSurname.block.parent as FormBlock;
            this._setVisibleForAllFields(ownerBlock, val);
        });
        formItem.byWarrant.value.valueHasMutated();
    }

    private _setVisibleForAllFields(block:FormBlock, visible:boolean) {

        ko.utils.arrayForEach(block.fields(), f => f.visible(visible));
        ko.utils.arrayForEach(block.blocks(), b => this._setVisibleForAllFields(b, visible));
    }

    private _getApplyField(title: string, field: IFormField, templateNodes: Node[]): IFormBlockField {
        const applyField = field as IFormBlockField;
        applyField.title = title;
        applyField.templateNodes = templateNodes;
        
        return applyField;
    }

    private _setCanAbsentField(absent:boolean, field:IFormBlockField) {
        field.readonly(absent);
        if(absent)
            field.value('');
    }
}

class ApplyForm extends Form<DossierApi.IApplyDto> {
    constructor(data: IODataForm<DossierApi.IApplyDto>) {
        super(data, DossierApi.saveApply, DossierApi.validateApply, {
            'attachments': (apply, parentForm) => {
                const applyForm = parentForm as ApplyForm;
                applyForm.errorsDic.attachments = ko.observableArray();

                const validate = (att: DossierApi.IApplyAttachmentDto) => {
                    applyForm.item.attachments.value.valueHasMutated();
                    return jQuery.Deferred();
                };

                const save = (att: DossierApi.IApplyAttachmentDto) => {
                    return jQuery.Deferred();
                };

                const valueArr = ko.observableArray<AttachmentForm>();
                const attachments = ko.utils.arrayMap(<DossierApi.IApplyAttachmentDto[]>apply.attachments, att => {
                    att.fileInfo = ko.utils.arrayMap(att.fileInfo, fi => new FileData(fi));
                    const formdata: IODataForm<DossierApi.IApplyAttachmentDto> = {
                        item: att,
                        options:{}
                    };
                    const form = new AttachmentForm(formdata, save, validate);
                    form.applyForm = applyForm;
        
                    return form;
                });

                valueArr(attachments);

                applyForm.errorsDic.attachments.subscribe(errors => {
                    ko.utils.arrayForEach(valueArr(), att => att.errors([]));

                    ko.utils.arrayForEach(errors, err => {
                        //  для прилагаемых документов ошибки приходят в виде "индекс:описание"
                        //  где индекс - индекс документа в коллекции 
                        const parts = err.split(':');
                        const index = parseInt(parts[0]);
                        const message = parts[1];
                        if(valueArr()[index])
                            valueArr()[index].errors.push(message);
                    });
                });

                return {
                    errors:applyForm.errorsDic.attachments,
                    options: ko.observableArray([]),
                    value: valueArr,
                    hasChanges: ko.computed(() => {
                        const origStamps = ko.utils.arrayMap(
                            ko.utils.arrayFilter(apply.attachments, att => !att.attachmentTypeId),
                            att => JSON.stringify(att)
                        );
                        const valueStamps = ko.utils.arrayMap(
                            ko.utils.arrayFilter(valueArr(), attform => !attform.item.attachmentTypeId.value()),
                            attform => JSON.stringify(attform.get())
                        );
                        const diff = ko.utils.compareArrays(origStamps, valueStamps);
                        if(ko.utils.arrayFirst(diff, d => d.status === 'added' || d.status === 'deleted'))
                            return true;

                        return !!ko.utils.arrayFirst(valueArr(), v => v.hasChanges());
                    })
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

    addAttachment() {
        const attDto: DossierApi.IApplyAttachmentDto = {
            attachmentTypeId: null,
            attachmentTypeName: '',
            description: '',
            error: '',
            fileInfo: [],
            given: false,
            required: false,
            id: 0
        };

        const form = this._createAttachmentForm(attDto);

        (this.item.attachments.value as ko.ObservableArray).push(form);
    }

    private _createAttachmentForm(att: DossierApi.IApplyAttachmentDto) {
        const validate = (att: DossierApi.IApplyAttachmentDto) => {
            this.item.attachments.value.valueHasMutated();
            return jQuery.Deferred();
        };

        const save = (att: DossierApi.IApplyAttachmentDto) => {
            return jQuery.Deferred();
        };

        att.fileInfo = ko.utils.arrayMap(att.fileInfo, fi => new FileData(fi));
        const formdata: IODataForm<DossierApi.IApplyAttachmentDto> = {
            item: att,
            options:{}
        };
        const form = new AttachmentForm(formdata, save, validate);
        form.applyForm = this;

        return form;
    }
}

class AttachmentForm extends Form<DossierApi.IApplyAttachmentDto> {
    acceptFilesExt = ['.pdf','.png','.docx'];
    fileDesc: ko.Computed<string>;
    fileUrl: ko.Computed<string>;

    private maxSize = 10*1024*1024; //10 MB
    applyForm: ApplyForm;

    constructor(
        data: IODataForm<DossierApi.IApplyAttachmentDto>,
        save:(att: DossierApi.IApplyAttachmentDto) => JQueryPromise<any>,
        validate:(att: DossierApi.IApplyAttachmentDto) => JQueryPromise<any>){
        super(data, save, validate);

        this.fileDesc = ko.computed(() => {
            const files: FileData[] = this.item.fileInfo.value();
            if(!files.length)
                return '';

            return `${files[0].name} (${files[0].sizeString})`;
        });

        this.fileUrl = ko.computed(() => {
            const files: FileData[] = this.item.fileInfo.value();
            if(!files.length || !files[0].uid)
                return '';

            return `api/storage/download/${files[0].uid}`;
        });
    }
    
    deleteFile() {
        this.item.fileInfo.value([]);
    }

    downloadFile() {
        console.log(this.item.fileInfo.value());
        if(!this.item.fileInfo.value().length)
            return;

        WebApi.download('api/storage/download', this.item.fileInfo.value()[0]);
    }

    validateSelection(files:FileData[]) {
        if(files[0].size > this.maxSize) {
            Popups.Alert.open('ошибка выбора файла', 'Превышен размер файла');
            return false;
        }

        return true;
    }
}
