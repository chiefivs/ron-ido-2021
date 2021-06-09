import * as ko from 'knockout';
import { Control, IControlParams, ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';
import { DossierApi } from '../../../codegen/webapi/dossierApi';
import { App } from '../../../app';
import  { IDossier, DossierPartBase } from './dossier-part-base';
import { Apply } from './dossier-apply';
import { Comments } from './dossier-comments';
import { Conclusion } from './dossier-conclusion';

export default class DossierMainPage extends MainPageBase implements IDossier {
    id: number;
    data = ko.observable<DossierApi.IDossierDataDto>();
    parts: ko.ObservableArray<DossierPartBase>;
    sortedParts: ko.Computed<DossierPartBase[]>;

    apply = ko.observable<DossierPartDescriptor<DossierApi.IApplyData>>();
    comments = ko.observable<DossierPartDescriptor<string>>();
    conclusions = ko.observableArray<DossierPartDescriptor<any>>();

    private _loadDossierPromise: JQueryPromise<DossierApi.IDossierDataDto>;
    private _dataPage: DossierDataLeftPage;

    constructor(dossierId:string) {
        super({
            pageTitle: 'дело',
            templatePath: 'pages/main/dossier/dossier.html'
        });

        this.id = parseInt(dossierId) || 0;
        this.parts = ko.observableArray();
        this.sortedParts = ko.computed(() => this.parts().sort((a, b) => a.priority < b.priority ? -1 : a.priority > b.priority ? 1 : 0));

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable(null);
        this._dataPage = new DossierDataLeftPage(this);
        this._dataPage.isVisible(false);
        this.leftPages([<ILeftPage>this._dataPage]);

        if(this.id) {
            this._loadDossierPromise = DossierApi.getDossier(this.id)
            .done(data => {
                this.pageTitle(data.apply.barCode);

                this.data(data);
                this._dataPage.isVisible(true);
                App.instance().activeLeftPage(this._dataPage);

                this.apply(new DossierPartDescriptor(data.apply, this, () => new Apply(data.apply.id, this)));
                // this.comments(new DossierPartDescriptor('Комментарии', this, () => new Comments(this)));

                // this.conclusions.push(
                //     new DossierPartDescriptor('123', this, () => new Conclusion(this)),
                //     new DossierPartDescriptor('45', this, () => new Conclusion(this))
                // )
            });
        }
    }

    openApply(){
        if(!this._loadDossierPromise)
            return;

        this._loadDossierPromise.done(() => {
            if(this.apply())
                this.apply().isVisible(true);
        });
    }

    afterActivate() {
        //console.log('dossier after activate');
    }
}

class DossierDataLeftPage extends LeftPageBase {
    constructor(owner: DossierMainPage) {
        super({
            pageTitle: 'дело',
            templatePath: 'pages/left/dossier-data.html'
        });

        this.owner = owner;
    }
}

class DossierPartDescriptor<TPartData> {
    item: TPartData;
    isVisible = ko.observable(false);
    allowOpen: ko.Computed<boolean>;

    private _create: () => DossierPartBase;
    private _part: DossierPartBase = null;

    constructor(item: TPartData, owner: DossierMainPage, create: () => DossierPartBase) {
        this.item = item;
        this._create = create;

        this.allowOpen = ko.computed(() => {
            console.log(owner.parts().length);
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