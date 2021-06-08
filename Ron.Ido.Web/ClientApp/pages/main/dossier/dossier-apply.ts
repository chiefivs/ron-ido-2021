import { DossierPartBase, IDossier } from './dossier-part-base';

export class Apply extends DossierPartBase {

    constructor(owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html',
            owner: owner
        })
    }
}