﻿import { Identity,LoginDialog } from './modules/identity';
import { ILeftPage, IMainPage, Control, MainPageBase, Popups, IMainPageParams } from './modules/content';
import { Utils } from './modules/utils';
import * as ko from 'knockout';

export class App {
    templateNodes: Element[];
    title: ko.Observable<string>;
    popups: Popups.PopupsCollection;
    userName: ko.Computed<string>;
    isAuthorized: ko.Computed<boolean>;

    windowWidth = ko.observable(0);
    windowHeight = ko.observable(0);
    leftPanelWidth = ko.observable(330);
    leftPages: ko.Observable<ILeftPage[]>;
    mainPages: ko.Observable<IMainPage[]>;
    activeMainPage = ko.observable<IMainPageParams>();

    private static _instance: App = null;
    
    constructor() {
        //leftTabs.init();
        this.templateNodes = Utils.getNodesFromFile('app.html');
        this.popups = new Popups.PopupsCollection();

        this.userName = ko.computed(() => {
            const user = Identity.user();
            return user ? user.name : '';
        })
        this.isAuthorized = ko.computed(() => !!Identity.user());

        Identity.user.subscribe(this._openLoginDialog);
        Identity.restoreIdentity();

        this._openLoginDialog();

        this.leftPages = ko.observable([
            new TestLeftPage('меню', `
                <div>пункт меню 1</div>
                <div>пункт меню 2</div>
                <div>пункт меню 3</div>
                <div>пункт меню 4</div>
                <div>пункт меню 5</div>
                <div>пункт меню 6</div>
                <div>пункт меню 7</div>
                <div>пункт меню 8</div>
                <div>пункт меню 9</div>
                <div>пункт меню10</div>
            `),
            new TestLeftPage('параметры', '<div>параметры</div>'),
            new TestLeftPage('настройки', '<div>настройки</div>')
        ]);

        this.mainPages = ko.observable([
            new TestMainPage('страница 1', '<div>страница 1</div>'),
            new TestMainPage('страница 2', '<div>страница 2</div>'),
            new TestMainPage('страница 3', '<div>страница 3</div>'),
            new TestMainPage('страница 4', '<div>страница 4</div>'),
            new TestMainPage('страница 5', '<div>страница 5</div>'),
            new TestMainPage('страница 6', '<div>страница 6</div>'),
            new TestMainPage('страница 7', '<div>страница 7</div>'),
            new TestMainPage('страница 8', '<div>страница 8</div>'),
            new TestMainPage('страница 9', '<div>страница 9</div>'),
            new TestMainPage('страница 10', '<div>страница 10</div>'),
            new TestMainPage('страница 11', '<div>страница 11</div>'),
            new TestMainPage('страница 12', '<div>страница 12</div>'),
            new TestMainPage('страница 13', '<div>страница 13</div>'),
            new TestMainPage('страница 14', '<div>страница 14</div>'),
            new TestMainPage('страница 15', '<div>страница 15</div>'),
            new TestMainPage('страница 16', '<div>страница 16</div>'),
            new TestMainPage('страница 17', '<div>страница 17</div>'),
            new TestMainPage('страница 18', '<div>страница 18</div>'),
            new TestMainPage('страница 19', '<div>страница 19</div>'),
            new TestMainPage('страница 20', '<div>страница 20</div>'),
        ]);

        $(window).on('resize', () => {
            this.windowWidth($(window).width());
            this.windowHeight($(window).height());
        });
    }

    static instance(): App {
        if(!App._instance)
            App._instance = new App();

        return App._instance;
    }

    logout() {
        Identity.setIdentity(null);
    }

    closeMainPage(page: IMainPage) {
        ko.utils.arrayRemoveItem(this.mainPages(), page);
        this.mainPages.valueHasMutated();



        const pages = this.mainPages();
        let index = ko.utils.arrayIndexOf(pages, page);
        ko.utils.arrayRemoveItem(pages, page);

        if(this.activeMainPage() === page) {
            index = Math.min(index, pages.length - 1);
            this.activeMainPage(index >= 0 ? pages[index] : null);
        }

        this.mainPages(pages);
    }

    private _openLoginDialog() {
        if(!Identity.user())
            LoginDialog.open();
    }
}

class TestLeftPage extends Control implements ILeftPage{
    pageTitle: string;

    constructor(title: string, html: string) {
        super({
            templateHtml: html
        });

        this.pageTitle = title;
    }
}

class TestMainPage extends MainPageBase{

    constructor(title: string, html: string) {
        super({
            templateHtml: html,
            pageTitle: title
        });
    }
}
