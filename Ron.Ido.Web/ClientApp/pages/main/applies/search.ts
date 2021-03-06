import * as ko from 'knockout';
import { App } from '../../../app';
import { default as DossierMainPage } from '../dossier/dossier';
import { ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { AppliesSearchApi } from '../../../codegen/webapi/appliesSearchApi';
import { IODataOrder } from '../../../codegen/webapi/odata';

export default class SearchMainPage extends MainPageBase {
    applies = ko.observableArray<AppliesSearchApi.IAppliesSearchPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'createDate desc',
        maxResultCount: 10,
    });
    private _searchPage: AppliesSearchLeftPage;

    constructor() {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/main/applies/search.html'
        });

        this.isActive.subscribe(active => { if (active) this.onActivated(); });
        this.pagerState.subscribe(() => this.update());

        this.leftPages = ko.observableArray([]);
        this.activeLeftPage = ko.observable();
        this._searchPage = new AppliesSearchLeftPage(this);
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

        AppliesSearchApi.getAppliesSearchPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters: filterStates,
            orders: orders
        }).done(page => {
            this.applies(page.items);
            this.tableTotalCount(page.total);
        });
    }

    open(item: AppliesSearchApi.IAppliesSearchPageItemDto) {
        console.log(item);
        const page = <DossierMainPage>App.instance().openMainPage('dossier/dossier', item.dossierId.toString());
        page.openApply();
    }
}

class AppliesSearchLeftPage extends LeftPageBase {
    filters: IFilterParams[];
    filterStates: ko.ObservableArray<IODataFilter> = ko.observableArray([]);

    statusesOptions = ko.observableArray<IFilterOption>([]);
    learnLevelOptions = ko.observableArray<IFilterOption>([]);
    entryFormOptions = ko.observableArray<IFilterOption>([]);
    stagesOptions = ko.observableArray<IFilterOption>([]);
    countriesOptions = ko.observableArray<IFilterOption>([]);

    constructor(owner: SearchMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/applies-search.html'
        });

        this.owner = owner;
        this.filters = [
            { title: 'ФИО заявителя', field: 'creatorSurname', aliases: ['creatorFirstname', 'creatorLastname'], valueType: 'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'ФИО владельца', field: 'ownerSurname', aliases: ['ownerFirstName', 'ownerLastName'], valueType: 'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'Номер заявления', field: 'barCode', aliases: ['primaryBarCode'], valueType: 'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'Дата создания', field: 'createTime', valueType: 'date', filterType: ODataFilterTypeEnum.BetweenLeft },
            { title: 'Статусы', field: 'statuses', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.statusesOptions },
            { title: 'Уровень образования', field: 'learnLevels', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.learnLevelOptions },
            { title: 'Страна выдачи ИДО', field: 'docCountries', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.countriesOptions },
            { title: 'Форма приема', field: 'entryForms', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.entryFormOptions },
            { title: 'Этапы', field: 'stages', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.stagesOptions }
        ];

        AppliesSearchApi.getAppliesSearchDictions().done(dictions => {
            const statusOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.statuses, status =>
                <IFilterOption>{ value: status.value.toString(), text: status.text });
            this.statusesOptions(statusOptionValues);

            const learnLevelOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.learnLevels, level =>
                <IFilterOption>{ value: level.value.toString(), text: level.text });
            this.learnLevelOptions(learnLevelOptionValues);

            const entryFormOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.entryForms, eForm =>
                <IFilterOption>{ value: eForm.value.toString(), text: eForm.text });
            this.entryFormOptions(entryFormOptionValues);

            const stagesOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.stages, stage =>
                <IFilterOption>{ value: stage.value.toString(), text: stage.text });
            this.stagesOptions(stagesOptionValues);

            this.countriesOptions(ko.utils.arrayMap(dictions.countries, country =>
                <IFilterOption>{ value: country.value.toString(), text: country.text }));
        });
    }
}