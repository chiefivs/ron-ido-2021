import { DossierPartBase, IDossier } from './dossier-part-base';

export class Comments extends DossierPartBase {

    constructor(owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-comments.html',
            owner: owner
        });

        this.priority = 2;
    }
}