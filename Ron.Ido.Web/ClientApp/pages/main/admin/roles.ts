import * as ko from 'knockout';
import { MainPageBase } from '../../../modules/content';
import { ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { IODataOrder } from '../../../codegen/webapi/odata';

export default class RolesMainPage extends MainPageBase {
    roles = ko.observableArray<AdminAccessApi.IRolesPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'name asc',
        maxResultCount: 10,
    });

    constructor() {
        super({
            pageTitle: 'роли',
            templatePath: 'pages/main/admin/roles.html'
        });

        this.isActive.subscribe(active => {if(active) this.onActivated();});
        this.pagerState.subscribe(() => this.update());
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

        AdminAccessApi.getRolesPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:[],
            orders:orders
        }).done(page => {
            this.roles(page.items);
            this.tableTotalCount(page.total);
        });
    }
}