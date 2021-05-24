import * as ko from 'knockout';
import { App } from '../../app';
import { LeftPageBase } from '../../modules/content';
import { Utils } from '../../modules/utils';
import  { AccountApi } from '../../codegen/webapi/accountApi';

export default class MainMenuLeftPage extends LeftPageBase {
    menuitems = ko.observableArray<MenuItem>([]);

    constructor() {
        super({
            pageTitle: 'меню',
            templatePath: 'pages/left/main-menu-root.html'
        });
    }

    update(items: AccountApi.IMenuItem[]) {
        this.menuitems(ko.utils.arrayMap(items, item => new MenuItem(item)));
    }

    private static _itemTemplateNodes: Element[];
    static getItemTemplateNodes() {
        if(!MainMenuLeftPage._itemTemplateNodes)
            this._itemTemplateNodes = Utils.getNodesFromFile('pages/left/main-menu-item.html');

        return this._itemTemplateNodes;
    }
}

class MenuItem {
    title: string;
    submenu: MenuItem[];
    itemTemplateNodes: Element[];
    isExpanded = false;

    private _data:AccountApi.IMenuItem;

    constructor(data: AccountApi.IMenuItem, owner: MenuItem = null) {
        this._data = data;
        this.title = data.title;
        this.itemTemplateNodes = MainMenuLeftPage.getItemTemplateNodes();
        this.submenu = data.submenu && data.submenu.length
            ? ko.utils.arrayMap(data.submenu, item => new MenuItem(item, this))
            : [];
    }

    click(data:any, evt:any) {
        if(this._data.path) {
            App.instance().openMainPage(this._data.path);
        } else {
            this._toggle($(evt.target).siblings('div'));
        }
    }

    private _toggle(element:JQuery) {
        if(this.isExpanded) {
            element.slideUp();
        } else {
            element.slideDown();
        }

        this.isExpanded = !this.isExpanded;
    }
}