import * as ko from 'knockout';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { ODataOrderTypeEnum, IODataOrder } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { MainPageBase, Popups } from '../../../modules/content';
import { Form } from '../../../modules/forms';

export default class RolesMainPage extends MainPageBase {
    current = ko.observable<Form<AdminAccessApi.IRoleDto>>(null);
    roles = ko.observableArray<AdminAccessApi.IRolesPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'name asc',
        maxResultCount: 10
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

    setCurrent(id?:number) {
        AdminAccessApi.getRole(id || 0)
            .done(data => {
                const form = new Form(data, AdminAccessApi.saveRole, AdminAccessApi.validateRole);
                this.current(form);
            });
    }

    save() {
        this.current().save()
            .done(() => this.update())
            .fail(() => Popups.Alert.open('ошибка сохранения', 'Не удалось сохранить роль'));
    }

    remove() {
        Popups.Confirm.open(
            'запрос на удаление',
            'Вы действительно хотите удалить эту роль?',
            () => {
                AdminAccessApi.deleteRole(this.current().item.id.value())
                    .done(() => {
                        this.current(null);
                        this.update();
                    })
                    .fail(() => Popups.Alert.open('ошибка удаления', 'Не удалось удалить роль'));
            });
    }
}