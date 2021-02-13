import { MainPageBase } from '../../../modules/content';

export default class AcceptanceMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'прием заявлений',
            templatePath: 'pages/main/applies/acceptance.html'
        });
    }
}