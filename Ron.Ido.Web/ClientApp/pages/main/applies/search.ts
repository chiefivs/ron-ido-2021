import { MainPageBase } from '../../../modules/content';

export default class SearchMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/main/applies/search.html'
        });
    }
}