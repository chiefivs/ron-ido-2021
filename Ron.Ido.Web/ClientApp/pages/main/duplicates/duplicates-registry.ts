import * as ko from 'knockout';
import { App } from '../../../app';
import { default as DuplicateMainPage } from './duplicate';
import { ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { DuplicatesSearchApi } from '../../../codegen/webapi/duplicatesSearchApi';
import { IODataOrder } from '../../../codegen/webapi/odata';

export default class DuplicateSearchMainPage extends MainPageBase {
    applies = ko.observableArray<DuplicatesSearchApi.IDuplicatesSearchPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'Duplicate.createTime desc',
        maxResultCount: 10,
    });
    private _searchPage: DuplicatesSearchLeftPage;

    constructor() {
        super({
            pageTitle: 'дубликаты',
            templatePath: 'pages/main/duplicates/duplicates.html'
        });

        this.isActive.subscribe(active => { if (active) this.onActivated(); });
        this.pagerState.subscribe(() => this.update());

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable();
        this._searchPage = new DuplicatesSearchLeftPage(this);
        this.leftPages([<ILeftPage>this._searchPage]);
        this.activeLeftPage(this._searchPage);

        this._searchPage.filterStates.subscribe(() => this.update());
    }
    onActivated() {
        this.update();
    }
    update() {
        const state = this.pagerState();
        const orders: IODataOrder[] = [];
        if (state.sorting) {
            const sortingParts = state.sorting.split(' ');
            orders.push({
                field: sortingParts[0],
                direct: sortingParts[1] === TableColumnOrderDirection.Asc ? ODataOrderTypeEnum.Asc : ODataOrderTypeEnum.Desc
            })
        }

        const filterStates = this._searchPage.filterStates();

        DuplicatesSearchApi.getDuplicatesSearchPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters: filterStates,
            orders: orders
        }).done(page => {
            this.applies(page.items);
            this.tableTotalCount(page.total);
        });
    }

    open(item: DuplicatesSearchApi.IDuplicatesSearchPageItemDto) {
        console.log(item);
        const page = <DuplicateMainPage>App.instance().openMainPage('duplicates/duplicate', item.id.toString());
        page.openDuplicate();
    }
}

class DuplicatesSearchLeftPage extends LeftPageBase {
    filters: IFilterParams[];
    filterStates: ko.ObservableArray<IODataFilter> = ko.observableArray([]);

    statusesOptions = ko.observableArray<IFilterOption>([]);

    constructor(owner: DuplicateSearchMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/duplicates-search.html'
        });

        this.owner = owner;
        this.filters = [
            { title: 'ФИО заявителя', field: 'creatorFullName', valueType: 'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'ФИО владельца', field: 'ownerFullName', aliases: ['ownerFirstName', 'ownerLastName'], valueType: 'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'Номер заявления', field: 'barCode', aliases: ['BarCode'], valueType: 'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'Дата создания', field: 'createTime', valueType: 'date', filterType: ODataFilterTypeEnum.BetweenLeft },
            { title: 'Статусы', field: 'statuses', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.statusesOptions },
        ];

        DuplicatesSearchApi.getDuplicatesSearchDictions().done(dictions => {
            const statusOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.statuses, status =>
                <IFilterOption>{ value: status.value.toString(), text: status.text });
            this.statusesOptions(statusOptionValues);

        });
    }
}