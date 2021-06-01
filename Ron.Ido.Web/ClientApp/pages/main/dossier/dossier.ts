import * as ko from 'knockout';
import { ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';

export default class DossierMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'дело',
            templatePath: 'pages/main/dossier/dossier.html'
        });
    }

    openApply(id:number){
        this.pageTitle('дело' + id.toString());
    }
}