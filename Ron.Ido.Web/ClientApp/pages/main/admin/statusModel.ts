import * as ko from 'knockout';
import { AdminSettingsApi } from '../../../codegen/webapi/adminSettingsApi';
import { ODataOrderTypeEnum, IODataOrder, IODataForm, IODataOption } from '../../../codegen/webapi/odata';
import { ITablePagerState, TableColumnOrderDirection, IFilterParams, IFilterOption, FilterValueType } from '../../../components/index';
import { MainPageBase, Popups } from '../../../modules/content';
import { Form } from '../../../modules/forms';

export default class ApplyStatusMainPage extends MainPageBase {
    current = ko.observable<Form<AdminSettingsApi.IApplyStatusDto>>(null);
    statuses = ko.observableArray<AdminSettingsApi.IApplyStatusPageItemDto>([]);
    tableTotalCount = ko.observable(0);
    pagerState = ko.observable<ITablePagerState>({
        skipCount: 0,
        sorting: 'name asc',
        maxResultCount: 10
    });

    constructor() {
        super({
            pageTitle: 'роли',
            templatePath: 'pages/main/admin/statusModel.html'
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

        AdminSettingsApi.getApplyStatusesPage({
            skip: state.skipCount,
            take: state.maxResultCount,
            filters:[],
            orders:orders
        }).done(page => {
            this.statuses(page.items);
            this.tableTotalCount(page.total);
        });
    }

    setCurrent(id?:number) {
        AdminSettingsApi.getStatus(id || 0)
            .done(data => {
                const form = new StatusForm(data, AdminSettingsApi.saveStatus, AdminSettingsApi.validateApplyStatus);
                this.current(form);
            });
    }

    save() {
        this.current().save()
            .done(() => {
                this.update();
                this.current(null);
            })
            .fail(() => Popups.Alert.open('ошибка сохранения', 'Не удалось сохранить статус'));
    }

    remove() {
        Popups.Confirm.open(
            'запрос на удаление',
            'Вы действительно хотите удалить эту роль?',
            () => {
                AdminSettingsApi.deleteStatus(this.current().item.id.value())
                    .done(() => {
                        this.current(null);
                        this.update();
                    })
                    .fail(() => Popups.Alert.open('ошибка удаления', 'Не удалось удалить статус'));
            });
    }
}

class StatusForm extends Form<AdminSettingsApi.IApplyStatusDto>{
    statuses: IODataOption[];
    allowStepToStatuses : ko.ObservableArray<any>;
    constructor(
        data:IODataForm<AdminSettingsApi.IApplyStatusDto>,
        saveApi?:(item:AdminSettingsApi.IApplyStatusDto) => JQueryPromise<any>,
        validateApi?:(item:AdminSettingsApi.IApplyStatusDto) => JQueryPromise<{[key:string]:string[]}>) {
        super(data, saveApi, validateApi);
        this.statuses = data.options.statuses;
        this.allowStepToStatuses = this.item.allowStepToStatuses.value as ko.ObservableArray<any>;;
    }

}

