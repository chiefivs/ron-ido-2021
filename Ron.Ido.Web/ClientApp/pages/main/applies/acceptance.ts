import * as ko from 'knockout';
import { App } from '../../../app';
import { default as DossierMainPage } from '../dossier/dossier';
import { ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { AppliesAcceptanceApi } from '../../../codegen/webapi/appliesAcceptanceApi';
import { IODataOrder } from '../../../codegen/webapi/odata';

export default class AcceptanceMainPage extends MainPageBase {
    applies = ko.observableArray<AppliesAcceptanceApi.IAppliesAcceptancePageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'createDate desc',
        maxResultCount: 10,
    });
    private _searchPage: AcceptanceSearchLeftPage;

    constructor() {
        super({
            pageTitle: 'прием заявлений',
            templatePath: 'pages/main/applies/acceptance.html'
        });

        this.isActive.subscribe(active => {if(active) this.onActivated();});
        this.pagerState.subscribe(() => this.update());

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable();
        this._searchPage = new AcceptanceSearchLeftPage(this);
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

        //const filterStates = this._searchPage.filterStates();

        AppliesAcceptanceApi.getAppliesPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:[],//filterStates,
            orders:orders
        }).done(page => {
            this.applies(page.items);
            this.tableTotalCount(page.total);
        });
    }

    open(item: AppliesAcceptanceApi.IAppliesAcceptancePageItemDto) {
        console.log(item);
        const page = <DossierMainPage>App.instance().openMainPage('dossier/dossier', item.dossierId.toString());
        page.openApply(item.dossierId);
    }

}

class AcceptanceSearchLeftPage extends LeftPageBase{
    filters: IFilterParams[];
    filterStates: ko.ObservableArray<IODataFilter> = ko.observableArray([]);

    // rolesOptions = ko.observableArray<IFilterOption>([]);

    // private _fullNameFilter: IFilterParams = { title: 'ФИО', field:'surName', aliases:['firstName', 'lastName'], valueType:'string', filterType: ODataFilterTypeEnum.Contains };
    // private _rolesFilter: IFilterParams = { title: 'Роли', field: 'roles', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.rolesOptions };
    // private _blockedFilter: IFilterParams = { title: 'Блокирован', field: 'isBlocked', valueType: 'boolean', filterType: ODataFilterTypeEnum.Equals, initialValues: [''] };
    // private _deletedFilter: IFilterParams = { title: 'Удален', field: 'isDeleted', valueType: 'boolean', filterType: ODataFilterTypeEnum.Equals, initialValues: [false] };

    constructor(owner: AcceptanceMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/applies-acceptance-search.html'
        });

        this.owner = owner;
        //this.filters = [this._fullNameFilter, this._rolesFilter, this._blockedFilter, this._deletedFilter];

        // AdminAccessApi.getUsersDictions().done(dictions => {
        //     const roleOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.roles, role => 
        //         <IFilterOption>{value: role.value.toString(), text: role.text});
        //     this.rolesOptions(roleOptionValues);
        // });
    }
}