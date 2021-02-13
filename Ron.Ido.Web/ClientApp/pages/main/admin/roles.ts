import { MainPageBase } from '../../../modules/content';

export default class RolesMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'роли',
            templatePath: 'pages/main/admin/roles.html'
        });
    }
}