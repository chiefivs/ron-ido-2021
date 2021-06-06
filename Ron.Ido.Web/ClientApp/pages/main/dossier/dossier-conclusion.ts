import { DossierPartBase } from './dossier-part-base';

export class Conclusion extends DossierPartBase {

    constructor() {
        super({
            templatePath: 'pages/main/dossier/dossier-conclusion.html'
        })
    }
}