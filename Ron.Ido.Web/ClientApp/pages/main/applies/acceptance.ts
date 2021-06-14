import * as ko from 'knockout';
import { App } from '../../../app';
import { default as DossierMainPage } from '../dossier/dossier';
import { ILeftPage, MainPageBase, LeftPageBase } from '../../../modules/content';
import { IODataFilter, ODataFilterTypeEnum, ODataOrderTypeEnum } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { AppliesAcceptanceApi } from '../../../codegen/webapi/appliesAcceptanceApi';
import { IODataOrder } from '../../../codegen/webapi/odata';

export default class AcceptanceMainPage extends MainPageBase {
    applies = ko.observableArray<AppliesAcceptanceApi.IAcceptancePageItemDto>([]);
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

    open(item: AppliesAcceptanceApi.IAcceptancePageItemDto) {
        console.log(item);
        const page = <DossierMainPage>App.instance().openMainPage('dossier/dossier', item.dossierId.toString());
        page.openApply(item.id);
    }

}

class AcceptanceSearchLeftPage extends LeftPageBase{
    filters: IFilterParams[];
    filterStates: ko.ObservableArray<IODataFilter> = ko.observableArray([]);

    statusesOptions = ko.observableArray<IFilterOption>([]);
    learnLevelOptions = ko.observableArray<IFilterOption>([]);
    entryFormOptions = ko.observableArray<IFilterOption>([]);
    stagesOptions = ko.observableArray<IFilterOption>([]);

    constructor(owner: AcceptanceMainPage) {
        super({
            pageTitle: 'поиск',
            templatePath: 'pages/left/applies-acceptance-search.html'
        });

        this.owner = owner;
        this.filters = [
            { title: 'ФИО заявителя', field:'creatorSurname', aliases:['creatorFirstname', 'creatorLastname'], valueType:'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'ФИО владельца', field:'ownerSurname', aliases:['ownerFirstName', 'ownerLastName'], valueType:'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'Номер заявления', field:'barCode', aliases:['primaryBarCode'], valueType:'string', filterType: ODataFilterTypeEnum.Contains },
            { title: 'Дата создания', field:'createTime', valueType:'date', filterType: ODataFilterTypeEnum.BetweenLeft },
            { title: 'Статусы', field: 'statuses', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.statusesOptions },
            { title: 'Уровень образования', field: 'learnLevel', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.learnLevelOptions },
            { title: 'Форма приема', field: 'entryForm', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.entryFormOptions },
            { title: 'Этапы', field: 'stages', valueType: 'number', filterType: ODataFilterTypeEnum.In, options: this.stagesOptions }
        ];

        AppliesAcceptanceApi.getAcceptanceDictions().done(dictions => {
            const statusOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.statuses, status => 
                 <IFilterOption>{value: status.value.toString(), text: status.text});
            this.statusesOptions(statusOptionValues);

            const learnLevelOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.learnLevels, level => 
                <IFilterOption>{value: level.value.toString(), text: level.text});
            this.learnLevelOptions(learnLevelOptionValues);

            const entryFormOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.entryForms, eForm => 
                <IFilterOption>{value: eForm.value.toString(), text: eForm.text});
            this.entryFormOptions(entryFormOptionValues);

            const stagesOptionValues: IFilterOption[] = ko.utils.arrayMap(dictions.stages, stage => 
                <IFilterOption>{value: stage.value.toString(), text: stage.text});
            this.stagesOptions(stagesOptionValues);
           });
    }
}