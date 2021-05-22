import * as ko from 'knockout';
import { MainPageBase } from '../../../modules/content';

export default class SearchMainPage extends MainPageBase {
    expanderTitle = 'expander title';
    isExpanded = ko.observable(false);

    constructor() {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/main/applies/search.html'
        });
    }

    toggle() {
        this.isExpanded(!this.isExpanded());
    }
}