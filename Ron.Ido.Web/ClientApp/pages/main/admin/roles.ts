import * as ko from 'knockout';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { ODataOrderTypeEnum, IODataOrder, IODataForm, IODataOption } from '../../../codegen/webapi/odata';
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

        this.pagerState.subscribe(() => this.update());
    }

    afterActivate() {
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
                const form = new RoleForm(data, AdminAccessApi.saveRole, AdminAccessApi.validateRole);
                this.current(form);
            });
    }

    save() {
        this.current().save()
            .done(() => {
                this.update();
                this.current(null);
            })
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

class RoleForm extends Form<AdminAccessApi.IRoleDto>{
    permissionGroups: PermissionGroup[];
    statuses: IODataOption[];
    stepStatuses : ko.ObservableArray<any>;
    viewStatuses : ko.ObservableArray<any>;
    constructor(
        data:IODataForm<AdminAccessApi.IRoleDto>,
        saveApi?:(item:AdminAccessApi.IRoleDto) => JQueryPromise<any>,
        validateApi?:(item:AdminAccessApi.IRoleDto) => JQueryPromise<{[key:string]:string[]}>) {
        super(data, saveApi, validateApi);
        this.permissionGroups = ko.utils.arrayMap(data.options.permissionGroups, group => {
            const perms = ko.utils.arrayFilter(data.options.rolePermissions, perm => perm.parent === group.value);
            return new PermissionGroup(group.value, perms, this);
        });
        this.statuses = data.options.statuses;
        this.viewStatuses = this.item.viewStatuses.value  as ko.ObservableArray<any>;
        this.stepStatuses = this.item.stepStatuses.value as ko.ObservableArray<any>;;
    }

    closeGroupsExcludingThis(group: PermissionGroup) {
        const groups = ko.utils.arrayFilter(this.permissionGroups, g => g !== group && g.isExpanded());
        ko.utils.arrayForEach(groups, g => g.isExpanded(false));
    }
}

interface IPermissionGroup {
    groupName: string;
    permissions: IODataOption[];
    allRoles: ko.ObservableArray<any>;
    isExpanded: ko.Observable<boolean>;
}

class PermissionGroup {
    groupName: ko.Computed<string>;
    permissions: IODataOption[];
    allRoles: ko.ObservableArray<any>;
    isExpanded: ko.Observable<boolean>;

    constructor(groupName: string, permissions: IODataOption[], owner: RoleForm) {
        this.permissions = permissions;
        this.allRoles = owner.item.rolePermissions.value as ko.ObservableArray<any>;
        this.isExpanded = ko.observable(false);
        
        this.groupName = ko.computed(() => {
            const cnt = ko.utils.arrayFilter(this.permissions, p => owner.item.rolePermissions.value().indexOf(p.value) > -1).length;
            return cnt ? `${groupName} (${cnt})` : groupName;
        });

        this.isExpanded.subscribe(expanded => {
            if(expanded) {
                owner.closeGroupsExcludingThis(this);
            }
        });
    }
}