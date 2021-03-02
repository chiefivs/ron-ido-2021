import * as ko from 'knockout';
import { ILeftPage, LeftPageBase, MainPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, FilterValueType } from '../../../components/index';
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

    private _searchPage: UsersSearchLeftPage;

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });

        this._searchPage = new UsersSearchLeftPage(this);
        this.leftPages = ko.observableArray([<ILeftPage>this._searchPage]);
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

class UsersSearchLeftPage extends LeftPageBase{
    filters: IFilterParams[];

    constructor(owner: UsersMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/admin/users-search.html'
        });

        this.owner = owner;
        this.filters = [
            { field:'surName', valueType:'string', filterType: ODataFilterTypeEnum.Contains, state: ko.observable()},
            { field:'birthDate', valueType:'date', filterType: ODataFilterTypeEnum.BetweenNone, state: ko.observable()},
        ];

        const filterStateChanged = (state:IODataFilter) => console.log(state);
        ko.utils.arrayForEach(this.filters, filter => filter.state.subscribe(filterStateChanged));
    }
}