import * as ko from 'knockout';
import { AccountApi } from '../codegen/webapi/accountApi';
import { Popups } from './content';

export class LoginDialog extends Popups.Popup {
    login: ko.Observable<string> = ko.observable('');
    password: ko.Observable<string> = ko.observable('');
    error: ko.Observable<string> = ko.observable('');
    isWait: ko.Observable<boolean> = ko.observable(false);

    private static _instance: LoginDialog;

    constructor() {
        super({ 
            width: 720,
            height: 380,
            templatePath: 'login-dialog.html'
        });
    }

    private static _isopened = false;
    static open(): void {
        if (LoginDialog._isopened)
            return;

        var dialog = new LoginDialog();
        dialog._instances.push(dialog);
        LoginDialog._isopened = true;
    }

    logon(): void {
        if (this.isWait())
            return;

        if (!this.login().trim() || !this.password().trim()) {
            this.error('Укажите логин и пароль');
            return;
        }

        this.isWait(true);
        this.error('');
        AccountApi.login(this.login().trim(), this.password().trim())
            .done(identity => {
                Identity.setIdentity(identity);
                LoginDialog._isopened = false;
                this._remove();

                AccountApi.getMenu()
                    .done(res => console.log('getmenu done', res))
                    .fail(error => console.log('getmenu fail', error));
            })
            .fail(error => {
                if(error.status === 401)
                    this.error(error.responseText);
                else
                    this.error('Ошибка авторизации');
            })
            .always(() => this.isWait(false));
    }
}

export namespace Identity {
    const identityKey = 'ido-app-identity';
    export const user: ko.Observable<AccountApi.IIdentity> = ko.observable(null);

    export function setIdentity(identity: AccountApi.IIdentity) {
        if(identity)
            sessionStorage.setItem(identityKey, JSON.stringify(identity));
        else
            sessionStorage.removeItem(identityKey);
        
        user(identity);
    }

    export function restoreIdentity(){
        var json = sessionStorage.getItem(identityKey);
        user(json ? JSON.parse(json) : null);
    }
}