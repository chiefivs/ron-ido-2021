﻿import * as ko from 'knockout';
import { Identity,LoginDialog } from './modules/identity';
import { ILeftPage, IMainPage, Control, MainPageBase, Popups, IPageParams } from './modules/content';
import { Utils } from './modules/utils';
import { AccountApi } from './codegen/webapi/accountApi';
import MainMenuLeftPage from './pages/left/main-menu';

export class App {
    templateNodes: Element[];
    title: ko.Observable<string>;
    popups: Popups.PopupsCollection;
    userName: ko.Computed<string>;
    isAuthorized: ko.Computed<boolean>;

    //  глобальные пераметры приложения, которые отслеживают компоненты
    windowWidth = ko.observable(0);
    windowHeight = ko.observable(0);
    leftPanelWidth = ko.observable(App.LEFT_PANEL_WIDTH_DEFAULT);
    contentVisible: ko.Computed<boolean>;
    static LEFT_PANEL_WIDTH_DEFAULT = 330;

    leftPages: ko.Observable<ILeftPage[]>;
    mainPages: ko.Observable<IMainPage[]>;
    activeLeftPage = ko.observable<ILeftPage>();
    activeMainPage = ko.observable<IMainPage>();

    private static _instance: App = null;
    private _mainMenu: MainMenuLeftPage = null
    
    constructor() {
        this.templateNodes = Utils.getNodesFromFile('app.html');
        this.popups = new Popups.PopupsCollection();

        this.userName = ko.computed(() => {
            const user = Identity.user();
            return user ? user.name : '';
        })
        this.isAuthorized = 
        this.contentVisible = ko.computed(() => !!Identity.user());


        Identity.user.subscribe(this._openLoginDialog);
        Identity.restoreIdentity();

        this._openLoginDialog();

        this._mainMenu = new MainMenuLeftPage();
        this.leftPages = ko.observable([this._mainMenu]);
        this.activeLeftPage(this._mainMenu);

        this.mainPages = ko.observable([]);

        $(window).on('resize', () => {
            this.windowWidth($(window).width());
            this.windowHeight($(window).height());
        });

        this.isAuthorized.subscribe(() => this._updateMainMenu());
        this._updateMainMenu();
    }

    static instance(): App {
        if(!App._instance)
            App._instance = new App();

        return App._instance;
    }

    logout() {
        Identity.setIdentity(null);
    }

    openMainPage(path: string, key: string = '') {
        const pageKey = path + (key ? '/'+key : '');
        let page = ko.utils.arrayFirst(this.mainPages(), page => page.pageKey === pageKey);
        if(!page) {
            const type = require(`@pages/main/${path}`).default;
            page = <IMainPage>(new type());
            page.pageKey = pageKey;
            this.mainPages().unshift(page);
            this.mainPages.valueHasMutated();
        }

        this.activeMainPage(page);
    }

    closeMainPage(page: IMainPage) {
        const pages = this.mainPages();
        let index = ko.utils.arrayIndexOf(pages, page);
        ko.utils.arrayRemoveItem(pages, page);
        this.mainPages(pages);

        if(this.activeMainPage() === page) {
            index = Math.min(index, pages.length - 1);
            this.activeMainPage(index >= 0 ? pages[index] : null);
        }

    }

    private _openLoginDialog() {
        if(!Identity.user())
            LoginDialog.open();
    }

    private _updateMainMenu() {
        if(this.isAuthorized())
            AccountApi.getMenu().done(items => this._mainMenu.update(items));
    }
}

class TestMainPage extends MainPageBase{

    constructor(title: string, html: string) {
        super({
            templateHtml: html,
            pageTitle: title
        });
    }

    close() {
        console.log(`TestMainPage closing "${this.pageTitle()}"`);
        super.close();
    }
}