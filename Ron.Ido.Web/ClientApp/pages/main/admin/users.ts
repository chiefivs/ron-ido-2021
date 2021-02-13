import { MainPageBase } from '../../../modules/content';

export default class UsersMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });
    }
}