import * as ko from 'knockout';
import { Control, IControlParams, ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';
import { DuplicatesSearchApi } from '../../../codegen/webapi/duplicatesSearchApi';
import { App } from '../../../app';
//import  { IDuplicate, DuplicatePartBase } from '../dossier/dossier-part-base';
import { Duplicate, DuplicatePartBase, IDuplicate } from '../duplicates/duplicate-main';
//import { Comments } from '../dossier/dossier-comments';
//import { Conclusion } from '../dossier/dossier-conclusion';
import { IODataForm } from '../../../codegen/webapi/odata';

export default class DuplicateMainPage extends MainPageBase implements IDuplicate {
    id: number;
    data = ko.observable<DuplicatesSearchApi.IDuplicateDto>();
    parts: ko.ObservableArray<DuplicatePartBase>;
    sortedParts: ko.Computed<DuplicatePartBase[]>;
    duplicate = ko.observable<DuplicatePartDescriptor<DuplicatesSearchApi.IDuplicateDto>>();

    //apply = ko.observable<DuplicatePartDescriptor<DuplicatesSearchApi.IDuplicateDto>>();
    //comments = ko.observable<DuplicatePartDescriptor<string>>();
    //conclusions = ko.observableArray<DuplicatePartDescriptor<any>>();

    private _loadDuplicatePromise: JQueryPromise<IODataForm< DuplicatesSearchApi.IDuplicateDto>>;
    private _dataPage: DuplicateDataLeftPage;

    constructor(duplicateId:string) {
        super({
            pageTitle: 'дубликат',
            templatePath: 'pages/main/duplicates/duplicate.html'
        });

        this.id = parseInt(duplicateId) || 0;
        this.parts = ko.observableArray();
        this.sortedParts = ko.computed(() => this.parts().sort((a, b) => a.priority < b.priority ? -1 : a.priority > b.priority ? 1 : 0));

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable(null);
        this._dataPage = new DuplicateDataLeftPage(this);
        this._dataPage.isVisible(false);
        this.leftPages([<ILeftPage>this._dataPage]);

        if(this.id) {
            this._loadDuplicatePromise = DuplicatesSearchApi.getDuplicate(this.id)
            .done(data => {
                this.pageTitle(data.item.barCode);

                this.data(data.item);
                this._dataPage.isVisible(true);
                App.instance().activeLeftPage(this._dataPage);

                this.duplicate(new DuplicatePartDescriptor(data.item, this, () => new Duplicate(data.item.id, this)));
                // this.comments(new DuplicatePartDescriptor('Комментарии', this, () => new Comments(this)));

                // this.conclusions.push(
                //     new DuplicatePartDescriptor('123', this, () => new Conclusion(this)),
                //     new DuplicatePartDescriptor('45', this, () => new Conclusion(this))
                // )
            });
        }
    }

    openDuplicate(){
        if(!this._loadDuplicatePromise)
            return;

        this._loadDuplicatePromise.done(() => {
            if(this.duplicate())
                this.duplicate().isVisible(true);
        });
    }

    afterActivate() {
        //console.log('dossier after activate');
    }
}

class DuplicateDataLeftPage extends LeftPageBase {
    constructor(owner: DuplicateMainPage) {
        super({
            pageTitle: 'дубликат',
            templatePath: 'pages/left/duplicate-data.html'
        });

        this.owner = owner;
    }
}


class DuplicatePartDescriptor<TPartData> {
    item: TPartData;
    isVisible = ko.observable(false);
    allowOpen: ko.Computed<boolean>;

    private _create: () => DuplicatePartBase;
    private _part: DuplicatePartBase = null;

    constructor(iitem: TPartData, owner: DuplicateMainPage, create: () => DuplicatePartBase) {
        this.item = iitem;
        this._create = create;

        this.allowOpen = ko.computed(() => {
            return owner.parts().length < 3 || this.isVisible();
        });

        this.isVisible.subscribe(visible => {
            if(visible) {
                this._part = this._create();
                owner.parts.push(this._part);
            } else if(this._part && this._part.close()) {
                this._part = null;
            }
        });
    }
}

