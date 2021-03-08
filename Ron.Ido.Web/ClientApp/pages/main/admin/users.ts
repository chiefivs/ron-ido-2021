import * as ko from 'knockout';
import { ILeftPage, LeftPageBase, MainPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, FilterValueType } from '../../../components/index';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { IODataOrder } from '../../../codegen/webapi/odata';
import { Utils } from '../../../modules/utils';
import { App } from '../../../app';

export default class UsersMainPage extends MainPageBase {
    users = ko.observableArray<AdminAccessApi.IUsersPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'fullName asc',
        maxResultCount: 10,
    });
    filters: IFilterParams[] = [
        { title: 'фамилия', field:'surName', aliases:['firstName', 'lastName'], valueType:'string', filterType: ODataFilterTypeEnum.Contains, state: ko.observable()},
        //{ title: 'дата рожд.', field:'birthDate', valueType:'date', filterType: ODataFilterTypeEnum.BetweenNone, state: ko.observable()},
    ];


    private _searchPage: UsersSearchLeftPage;
    private _isLeftPagesCreated = false;

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable();

        const filterStateChanged = (state:IODataFilter) => {};
        ko.utils.arrayForEach(this.filters, filter => filter.state.subscribe(filterStateChanged));
        
        this.isActive.subscribe(active => {if(active) this.onActivated();});
        this.pagerState.subscribe(() => this.update());

        this._searchPage = new UsersSearchLeftPage(this);
        this.leftPages([<ILeftPage>this._searchPage]);
        this.activeLeftPage(this._searchPage);
    }

    onActivated() {
        this.update();
    }

    update() {
        const state = this.pagerState();
        const orders:IODataOrder[] = [];
        if(state.sorting){
            const sortingParts = state.sorting.split(' ');
            orders.push({
                field:sortingParts[0],
                direct: sortingParts[1] === TableColumnOrderDirection.Asc ? ODataOrderTypeEnum.Asc : ODataOrderTypeEnum.Desc})
        }

        const filters = ko.utils.arrayFilter(this.filters, filter => !!filter.state())

        AdminAccessApi.getUsers({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:ko.utils.arrayMap(filters, f => f.state()),
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
            templatePath: 'pages/main/admin/users-search.html'
        });

        this.owner = owner;
        this.filters = owner.filters;
    }

    
    search() {
        const usersPage = <UsersMainPage>this.owner;
        usersPage.pagerState().skipCount = 0;
        usersPage.update();
    }

}