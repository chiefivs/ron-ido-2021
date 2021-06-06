import { DossierPartBase } from './dossier-part-base';

export class Apply extends DossierPartBase {

    constructor() {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html'
        })
    }
}