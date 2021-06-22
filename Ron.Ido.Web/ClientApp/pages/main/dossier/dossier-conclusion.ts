import { DossierPartBase, IDossier } from './dossier-part-base';

export class Conclusion extends DossierPartBase {

    constructor(owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-conclusion.html',
            owner: owner
        });

        this.priority = 5;
    }
}