import * as ko from 'knockout';
import { LeftPageBase, MainPageBase } from '../../../modules/content';
import { ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, FilterType, FilterValueType } from '../../../components/index';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { IODataOrder } from '../../../codegen/webapi/odata';
import { Utils } from '../../../modules/utils';

export default class UsersMainPage extends MainPageBase {
    users = ko.observableArray<AdminAccessApi.IUserDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: '',
        maxResultCount: 10,
    });
    filters: IFilterParams[] = [
        { field:'surName', values: ko.observableArray([]), filterType:'cn', valueType:'string'}
    ];

    private _searchPage: UsersSearchLeftPage;

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });

        this._searchPage = new UsersSearchLeftPage(this);
        this.leftPages = ko.observableArray([this._searchPage]);
        this.activeLeftPage = ko.observable(this._searchPage);
        this.isActive.subscribe(active => {if(active) this.onActivated();});
        this.pagerState.subscribe(() => this._update());
    }

    onActivated() {
        this._update();
    }

    private _update() {
        const state = this.pagerState();
        const orders:IODataOrder[] = [];
        if(state.sorting){
            const sortingParts = state.sorting.split(' ');
            orders.push({
                field:sortingParts[0],
                direct: sortingParts[1] === TableColumnOrderDirection.Asc ? ODataOrderTypeEnum.Asc : ODataOrderTypeEnum.Desc})
        }

        AdminAccessApi.getUsers({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:[],
            orders:orders
        }).done(page => {
            this.users(page.items);
            this.tableTotalCount(page.total);
        });
    }
}

class UsersSearchLeftPage extends LeftPageBase {
    constructor(owner: UsersMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/admin/users-search.html'
        });

        this.owner = owner;
    }
}