import * as ko from 'knockout';
import { ILeftPage, LeftPageBase, MainPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, FilterValueType } from '../../../components/index';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { IODataOrder } from '../../../codegen/webapi/odata';
import { Utils } from '../../../modules/utils';
import { App } from '../../../app';

export default class UsersMainPage extends MainPageBase {
    users = ko.observableArray<AdminAccessApi.IUserDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: '',
        maxResultCount: 10,
    });
    filters: IFilterParams[] = [
        { title: 'фамилия', field:'surName', valueType:'string', filterType: ODataFilterTypeEnum.Contains, state: ko.observable()},
        { title: 'дата рожд.', field:'birthDate', valueType:'date', filterType: ODataFilterTypeEnum.BetweenNone, state: ko.observable()},
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
    }

    onActivated() {
        this.update();
    }

    afterRender() {
        if(!this._isLeftPagesCreated) {
            this._searchPage = new UsersSearchLeftPage(this);
            this.leftPages([<ILeftPage>this._searchPage]);
            this.activeLeftPage(this._searchPage);
            App.instance().activeMainPage.valueHasMutated();
            this._isLeftPagesCreated = true;
        }
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
            templateId: 'users-search-leftpage'
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