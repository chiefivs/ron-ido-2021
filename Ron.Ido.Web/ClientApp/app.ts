import { Identity,LoginDialog } from './modules/identity';
import { Popups } from './modules/content';
import { Template } from './modules/template';
import { WebApi } from './modules/webapi';
import { AccountApi } from './codegen/webapi/accountApi';
import * as ko from 'knockout';

export default class App {
    templateNodes: Element[];
    title: ko.Observable<string>;
    popups: Popups.PopupsCollection;
    userName = ko.observable('');
    
    constructor() {
        this.templateNodes = Template.getNodes('app.html');
        this.popups = new Popups.PopupsCollection();

        Identity.user.subscribe(u => {
            this.userName( u ? u.name : '');
            this._openLoginDialog();
        });
        Identity.restoreIdentity();

        this._openLoginDialog();
    }

    logout() {
        Identity.setIdentity(null);
    }

    private _openLoginDialog() {
        if(!this.userName())
            LoginDialog.open();
    }
}
