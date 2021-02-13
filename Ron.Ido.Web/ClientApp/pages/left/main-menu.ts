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
    submenuHeight = ko.observable('0');

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
            const element = $(evt.target).siblings('div');
            const height = Utils.getElementRect(element).height;

            if(this.submenuHeight() === '') {
                Utils.animate(height, 0, h => this.submenuHeight(`${Math.round(h)}px`));
            } else {
                Utils.animate(0, height, h => this.submenuHeight(`${Math.round(h)}px`), () => this.submenuHeight(''));
            }
        }
    }
}