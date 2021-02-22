import * as ko from 'knockout';
import { MainPageBase } from '../../../modules/content';
//import { IODataPage } from '../../../codegen/webapi/odata';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';

export default class UsersMainPage extends MainPageBase {
    users = ko.observableArray<AdminAccessApi.IUserDto>([]);

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });

        this.isActive.subscribe(active => {if(active) this.onActivated();});
    }

    onActivated() {
        AdminAccessApi.getUsers({
            skip:0,
            take:10,
            filters:[],
            orders:[]
        }).done(page => {
            this.users(page.items)
            console.log(page);
        });
    }
}