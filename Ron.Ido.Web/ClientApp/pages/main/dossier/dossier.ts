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
    parts: ko.ObservableArray<DossierPartBase>;
    partWidth: ko.Computed<number>;

    private _dataPage: DossierDataLeftPage;

    constructor() {
        super({
            pageTitle: 'дело',
            templatePath: 'pages/main/dossier/dossier.html'
        });


        this.isActive.subscribe(active => {if(active) this.onActivated();});

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable(null);
        this._dataPage = new DossierDataLeftPage(this);
        this._dataPage.isVisible(false);
        this.leftPages([<ILeftPage>this._dataPage]);

        this.parts = ko.observableArray();
        this.partWidth = ko.computed(() => {
            const len = this.parts().length;
            if(!len)
                return 100;

            return Math.round(100/len);
        });

        this.parts.push(new Apply(this));
        this.parts.push(new Comments(this));
        this.parts.push(new Conclusion(this));
    }

    openApply(id:number){
        DossierApi.getDossier(id)
            .done(data => {
                this.pageTitle(data.apply.barCode);
                console.log(data);

                this._dataPage.setData(data);
                this._dataPage.isVisible(true);
                App.instance().activeLeftPage(this._dataPage);
            });
    }


    onActivated() {
    }
}

class DossierDataLeftPage extends LeftPageBase {
    data = ko.observable<DossierApi.IDossierDataDto>();

    constructor(owner: DossierMainPage) {
        super({
            pageTitle: 'дело',
            templatePath: 'pages/left/dossier-data.html'
        });

        this.owner = owner;
    }

    setData(dossierData:DossierApi.IDossierDataDto) {
        this.data(dossierData);
    }
}
