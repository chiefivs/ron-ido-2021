import { MainPageBase } from '../../../modules/content';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';

export default class UsersMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });
    }

    onActivated() {
        AdminAccessApi.getUsers({
            skip:2,
            take:10,
            filters:[],
            orders:[]
        }).done(page => console.log(page));
    }
}