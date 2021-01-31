import { Identity,LoginDialog } from './modules/identity';
import { Popups } from './modules/content';
import { Template } from './modules/template';
import { WebApi } from './modules/webapi';
import { AccountApi } from './codegen/webapi/accountApi';
import * as ko from 'knockout';
import * as cmp from './components/index';

export default class App {
    templateNodes: Element[];
    title: ko.Observable<string>;
    popups: Popups.PopupsCollection;
    userName: ko.Computed<string>;
    isAuthorized: ko.Computed<boolean>;

    leftTabs: ko.Observable<cmp.ILeftTab[]>;
    
    constructor() {
        //leftTabs.init();
        this.templateNodes = Template.getNodes('app.html');
        this.popups = new Popups.PopupsCollection();

        this.userName = ko.computed(() => {
            const user = Identity.user();
            return user ? user.name : '';
        })
        this.isAuthorized = ko.computed(() => !!Identity.user());

        Identity.user.subscribe(this._openLoginDialog);
        Identity.restoreIdentity();

        this._openLoginDialog();

        this.leftTabs = ko.observable([
            { title: 'меню' },
            { title: 'параметры' },
            { title: 'настройки' },
        ]);
    }

    logout() {
        Identity.setIdentity(null);
    }

    private _openLoginDialog() {
        if(!Identity.user())
            LoginDialog.open();
    }
}
