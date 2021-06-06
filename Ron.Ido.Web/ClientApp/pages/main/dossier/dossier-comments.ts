import { DossierPartBase } from './dossier-part-base';

export class Comments extends DossierPartBase {

    constructor() {
        super({
            templatePath: 'pages/main/dossier/dossier-comments.html'
        })
    }
}