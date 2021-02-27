import * as ko from 'knockout';
import { MainPageBase } from '../../../modules/content';
import { ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection } from '../../../components/index';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { IODataOrder } from '../../../codegen/webapi/odata';

export default class UsersMainPage extends MainPageBase {
    users = ko.observableArray<AdminAccessApi.IUserDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: '',
        maxResultCount: 10,
    });

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });

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

        console.log(state);
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