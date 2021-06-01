import * as ko from 'knockout';

import { ILeftPage, LeftPageBase, MainPageBase, Popups } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { AdminAccessApi } from '../../../codegen/webapi/adminAccessApi';
import { IODataOrder } from '../../../codegen/webapi/odata';
import { Form } from '../../../modules/forms';

export default class UsersMainPage extends MainPageBase {
    users = ko.observableArray<AdminAccessApi.IUsersPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'fullName asc',
        maxResultCount: 10,
    });

    current = ko.observable<Form<AdminAccessApi.IUserDto>>(null);

    private _searchPage: UsersSearchLeftPage;

    constructor() {
        super({
            pageTitle: 'пользователи',
            templatePath: 'pages/main/admin/users.html'
        });

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable();

        this.isActive.subscribe(active => {if(active) this.onActivated();});
        this.pagerState.subscribe(() => this.update());

        this._searchPage = new UsersSearchLeftPage(this);
        this.leftPages([<ILeftPage>this._searchPage]);
        this.activeLeftPage(this._searchPage);

        this._searchPage.filterStates.subscribe(() => this.update());
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

        const filterStates = this._searchPage.filterStates();

        AdminAccessApi.getUsersPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:filterStates,
            orders:orders
        }).done(page => {
            this.users(page.items);
            this.tableTotalCount(page.total);
        });
    }

    setCurrent(id?:number) {
        AdminAccessApi.getUser(id || 0)
            .done(data => {
                const form = new Form(data, AdminAccessApi.saveUser, AdminAccessApi.validateUser);
                this.current(form);
            });
    }


    save() {
        this.current().save()
            .done(() => {
                this.update();
                this.current(null);
            })
            .fail(() => Popups.Alert.open('ошибка сохранения', 'Не удалось сохранить пользователя'));
    }

    remove() {
        Popups.Confirm.open(
            'запрос на удаление',
            'Вы действительно хотите удалить этуго пользователя ?',
            () => {
                AdminAccessApi.deleteUser(this.current().item.id.value())
                    .done(() => {
                        this.current(null);
                        this.update();
                    })
                    .fail(() => Popups.Alert.open('ошибка удаления', 'Не удалось удалить пользователя'));
            });
    }
}

class UsersSearchLeftPage extends LeftPageBase{
    filters: IFilterParams[];
    filterStates: ko.ObservableArray<IODataFilter> = ko.observableArray([]);

    rolesOptions = ko.observableArray<IFilterOption>([]);

    private _fullNameFilter: IFilterParams = { title: 'ФИО', field:'surName', aliases:['firstName', 'lastName'], valueType:'string', filterType: ODataFilterTypeEnum.Contains };
    private _rolesFilter: IFilterParams = { title: 'Роли', field: 'roles', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.rolesOptions };
    private _blockedFilter: IFilterParams = { title: 'Блокирован', field: 'isBlocked', valueType: 'boolean', filterType: ODataFilterTypeEnum.Equals, initialValues: [''] };
    private _deletedFilter: IFilterParams = { title: 'Удален', field: 'isDeleted', valueType: 'boolean', filterType: ODataFilterTypeEnum.Equals, initialValues: [false] };

    constructor(owner: UsersMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/users-search.html'
        });

        this.owner = owner;
        this.filters = [this._fullNameFilter, this._rolesFilter, this._blockedFilter, this._deletedFilter];

        AdminAccessApi.getUsersDictions().done(dictions => {
            const roleOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.roles, role => 
                <IFilterOption>{value: role.value.toString(), text: role.text});
            this.rolesOptions(roleOptionValues);
        });
    }

    // alert() {
    //     Popups.Alert.open('тестовое предупреждение', 'описание <b>тестового предупреждения</b>', true)
    // }
}

