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

        const filterStates = this._searchPage.filterStates();

        AppliesAcceptanceApi.getAppliesPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:filterStates,
            orders:orders
        }).done(page => {
            this.applies(page.items);
            this.tableTotalCount(page.total);
        });
    }

    open(item: AppliesAcceptanceApi.IAppliesAcceptancePageItemDto) {
        console.log(item);
        const page = <DossierMainPage>App.instance().openMainPage('dossier/dossier', item.dossierId.toString());
        page.openApply(item.id);
    }

}

class AcceptanceSearchLeftPage extends LeftPageBase{
    filters: IFilterParams[];
    filterStates: ko.ObservableArray<IODataFilter> = ko.observableArray([]);

    statusesOptions = ko.observableArray<IFilterOption>([]);
    educationLevelOptions = ko.observableArray<IFilterOption>([]);
    entryFormOptions = ko.observableArray<IFilterOption>([]);
    stagesOptions = ko.observableArray<IFilterOption>([]);

    private _creatorFullNameFilter: IFilterParams = { title: 'ФИО заявителя', field:'creatorSurname', aliases:['creatorFirstname', 'creatorLastname'], valueType:'string', filterType: ODataFilterTypeEnum.Contains };
    private _ownerFullNameFilter: IFilterParams = { title: 'ФИО владельца', field:'ownerSurname', aliases:['ownerFirstName', 'ownerLastName'], valueType:'string', filterType: ODataFilterTypeEnum.Contains };
    private _barCodeFilter: IFilterParams = { title: 'Номер заявления', field:'barCode', aliases:['primaryBarCode'], valueType:'string', filterType: ODataFilterTypeEnum.Contains };
    private _createDateFilter: IFilterParams = { title: 'Дата создания', field:'createTime', valueType:'date', filterType: ODataFilterTypeEnum.BetweenLeft };
    private _statusesFilter: IFilterParams = { title: 'Статусы', field: 'statuses', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.statusesOptions };
    private _educationLevelFilter: IFilterParams = { title: 'Уровень образования', field: 'learnLevel', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.educationLevelOptions };
    private _entryFormFilter: IFilterParams = { title: 'Форма приема', field: 'entryForm', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.entryFormOptions };
    private _stagesFilter: IFilterParams = { title: 'Этапы', field: 'stages', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.stagesOptions };
    constructor(owner: AcceptanceMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/applies-acceptance-search.html'
        });

        this.owner = owner;
        this.filters = [this._creatorFullNameFilter, this._ownerFullNameFilter, this._barCodeFilter, this._createDateFilter, this._statusesFilter, this._educationLevelFilter, this._entryFormFilter, this._stagesFilter];

        AppliesAcceptanceApi.getAcceptanceDictions().done(dictions => {
            const statusOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.statuses, status => 
                 <IFilterOption>{value: status.value.toString(), text: status.text});
            this.statusesOptions(statusOptionValues);

            const educationLevelOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.educationLevels, level => 
                <IFilterOption>{value: level.value.toString(), text: level.text});
            this.educationLevelOptions(educationLevelOptionValues);

            const entryFormOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.applyEntryForm, eForm => 
                <IFilterOption>{value: eForm.value.toString(), text: eForm.text});
            this.entryFormOptions(entryFormOptionValues);

            const stagesOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.stages, stage => 
                <IFilterOption>{value: stage.value.toString(), text: stage.text});
            this.stagesOptions(stagesOptionValues);
           });
    }
}