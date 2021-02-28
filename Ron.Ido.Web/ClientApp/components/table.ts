import * as ko from 'knockout';
import { App } from '../app';
import { Utils } from '../modules/utils';

const myepTable = 'cmp-table';
const myepTableColumn = 'cmp-table-column';
const myepTableTitle = 'cmp-table-title';
const myepTableData = 'cmp-table-data';
const myepTableChildTitle = 'cmp-table-child-title';
const myepTableChildData = 'cmp-table-child-data';
const myepTableFooter = 'cmp-table-footer';
const myepTablePager = 'cmp-table-pager';
const templatesTablesFolderPath = 'components/table/';

export function init(){
    ko.components.register(myepTable,
        {
            viewModel: {
                createViewModel(params: ITableParams, componentInfo: any) {
    
                    return new TableModel(params, componentInfo);
                }
            },
            template: `
                <!-- ko template:{nodes:[], afterRender:function(){setParentContext.bind($data)($parent);}} --><!-- /ko -->
    
                <table class="table large-view js-table">
                    <thead>
                        <tr>
                            <th class="control" data-bind="visible:hasChildColumns()"></th>
                            <!-- ko foreach:columns -->
                            <!-- ko with:parent -->
    
                            <!-- ko if:$parent.order -->
                            <th class="sorting" data-bind="style:{'width':$parent.width+'%'}">
                                <div>
                                    <div class="sorting-title" data-bind="template:{nodes:$parent.nodes, data:$parent.data}"></div>
                                    <div class="sorting-arrows" data-bind="with:$parent.order, css:{red:$parent.order.current()}">
                                        <span class="ico-web-arrow-up" data-bind="click:descAction, visible:current()==='asc'">^</span>
                                        <span class="ico-web-arrow-down" data-bind="click:ascAction, visible:current()==='desc'">v</span>
                                        <span class="ico-web-arrow-up" data-bind="click:ascAction, visible:!current()">^</span>
                                        <span class="ico-web-arrow-down" data-bind="click:descAction, visible:!current()">v</span>
                                    </div>
                                </div>
                            </th>
                            <!-- /ko -->
                            <!-- ko ifnot:$parent.order -->
                            <th data-bind="template:{nodes:$parent.nodes, data:$parent.data}, style:{'width':$parent.width+'%'}"></th>
                            <!-- /ko -->
    
    
                            <!-- /ko -->
                            <!-- /ko -->
                        </tr>
                    </thead>
                    <tbody data-bind="foreach:rows">
                        <tr data-bind="css:{expanded:isExpanded()}">
                            <td class="control" data-bind="visible:hasChildColumns(), css:{expanded:isExpanded}, click:toggle"></td>
                            <!-- ko foreach:cells -->
                            <!-- ko with:parent -->
                            <td data-bind="template:{nodes:$parent.nodes, data:$parent.data}"></td>
                            <!-- /ko -->
                            <!-- /ko -->
                        </tr>
    
                        <!-- ko children.length -->
                        <tr class="children" data-bind="visible:isExpanded() && children().length, css:{expanded:isExpanded()}">
                            <td data-bind="attr:{colspan:cells().length+1}">
                                <ul data-bind="foreach:children">
                                    <li>
                                        <div>
                                            <!-- ko with:title -->
                                            <!-- ko with:parent -->
                                            <div class="child-title" data-bind="template:{nodes:$parent.nodes, data:$parent.data}"></div>
                                            <!-- /ko -->
                                            <!-- /ko -->
                                            <!-- ko with:content -->
                                            <!-- ko with:parent -->
                                            <div class="child-data" data-bind="template:{nodes:$parent.nodes, data:$parent.data}"></div>
                                            <!-- /ko -->
                                            <!-- /ko -->
                                        </div>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <!-- /ko -->
                    </tbody>
                </table>
       
                <div class="table-pager js-pager" data-bind="with:pager">
                    <ul>
                        <li class="page-nav first" data-bind="click:first, css:{disabled:!firstEnabled()}"></li>
                        <li class="page-nav prev" data-bind="click:prev, css:{disabled:!firstEnabled()}"></li>
                        <!-- ko foreach:buttons -->
                        <!-- ko if:action -->
                        <li data-bind="css:{'active':isActive}, text:title, click:action"></li>
                        <!-- /ko -->
                        <!-- ko ifnot:action -->
                        <li class="space disabled"></li>
                        <!-- /ko -->
                        <!-- /ko -->
                        <li class="page-nav next" data-bind="click:next, css:{disabled:!lastEnabled()}"></li>
                        <li class="page-nav last" data-bind="click:last, css:{disabled:!lastEnabled()}"></li>
                    </ul>

                    <div class="current-page-desc">
                        <span data-bind="text:skipCount() + 1"></span>
                        <span>-</span>
                        <span data-bind="text:Math.min(skipCount() + maxResultCountValue().value, totalCount())"></span>
                        <span>&nbsp;из&nbsp;</span>
                        <span data-bind="text:totalCount()"></span>
                    </div>

                    <div class="max-result-select pull-right">
                        <span class="title">строк на странице</span>
                        <cmp-select params="value:maxResultCountValue, options:maxResultCountOptions"></cmp-select>
                    </div>
                </div>
            `
        });
    
    ko.components.register(myepTableColumn,
        {
            viewModel: {
                createViewModel(params: ITableColumnParams, componentInfo: any) {
    
                    return new TableColumnModel(params, componentInfo);
                }
            },
            template: `
                <!-- ko template:{nodes:[], data:$data, afterRender:function(){afterRender.bind($data)($parent);}} -->
                <!-- /ko -->
            `
        });
}


export interface ITableParams {
    columns: ITableColumnParams[] | ko.ObservableArray<ITableColumnParams>;
    rows: any[] | ko.ObservableArray<any>;
    pagerState: ko.Observable<ITablePagerState>;
    totalCount: ko.Observable<number>;
}

export interface ITableColumnParams {
    title?: string | ko.Observable<string>;
    data?: string;
    orderable?: boolean;
    priority?: number;
    titleTemplate?: string;
    cellTemplate?: string;
    childTitleTemplate?: string;
    childCellTemplate?: string;
}

export interface ITablePagerState {
    maxResultCount: number;
    skipCount: number;
    sorting: string;
}

export const TableColumnOrderDirection = {
    Asc: 'asc',
    Desc: 'desc'
}

interface IDataDescriptor {
    nodes: Node[];
    parent: any;
    data: any;
}

interface IOrderDescriptor {
    current: ko.Observable<string>;
    ascAction: () => void;
    descAction: () => void;
}

interface IParentColDescriptor extends IDataDescriptor {
    order: IOrderDescriptor;
    width:number;
}

interface IChildCellDescriptor {
    title?: IDataDescriptor;
    content: IDataDescriptor;
}

interface ITablePagerButton {
    title: string;
    isActive: boolean;
    action: () => void;
}

interface ITablePagerOption {
    value: number;
    text: string;
}

class TableModel {
    cellsTemplateNodes: Node[];
    columns: ko.Computed<IParentColDescriptor[]>;
    rows: ko.Computed<TableRow[]>;
    hasChildColumns: ko.Computed<boolean>;
    currentOrder: ko.Observable<string>;

    allColumns = ko.observableArray<TableColumnModel>([]);
    parentColumns = ko.observableArray<TableColumnModel>([]);
    childColumns = ko.observableArray<TableColumnModel>([]);
    pager: TablePager;
    parentContext: any;
    columnsTemplatesCount = 0;

    private allRows: ko.Observable<any>;
    private componentElement: JQuery;
    private componentWidth = ko.observable(0);
    private recalcColumnsMode: RecalcColumnsMode = RecalcColumnsMode.ByMaxColumnWidth;

    constructor(params: ITableParams, componentInfo: any) {
        this.componentElement = $(componentInfo.element);
        this.currentOrder = ko.observable(params.pagerState ? params.pagerState().sorting || '' : '');

        if (params.columns) {
            this.allColumns(this._mapColumns(params.columns));
            if (ko.isObservable(params.columns)) {
                params.columns.subscribe(cols => this.allColumns(this._mapColumns(cols)));
            }
        } else {
            const columnsTemplates =
                $(componentInfo.templateNodes).filter((i, e) => e.localName === myepTableColumn);
            this.columnsTemplatesCount = columnsTemplates.length;
            $(componentInfo.element).prepend(columnsTemplates);
        }
        this.allColumns.subscribe(() => this.recalcColumns(true));

        this.allRows = ko.isObservable(params.rows) ? params.rows : ko.observableArray(params.rows || []);
        if (this.recalcColumnsMode === RecalcColumnsMode.ByTableWidth) {
            this.allRows.subscribe(() => setTimeout(() => this.recalcColumns(), 10));
        }

        this.columns = ko.computed(() => {
            const cnt = this.parentColumns().length;
            const width = cnt ? Math.floor(100 / cnt) : 100;
            return ko.utils.arrayMap(this.parentColumns(), col => {
                const order: IOrderDescriptor = col.orderable
                    ? {
                        current: col.currentOrder.bind(col),
                        ascAction: col.asc.bind(col),
                        descAction: col.desc.bind(col)
                    }
                    : null;

                return <IParentColDescriptor>{
                    order: order,
                    width: width,
                    data: col.title,
                    parent: this.parentContext,
                    nodes: col.titleTemplateNodes || []
                };
            });
        });

        this.hasChildColumns = ko.computed(() => !!this.childColumns().length);
        this.rows = ko.computed(() => ko.utils.arrayMap(this.allRows(), (row: any) => new TableRow(this, row)));

        this.pager = new TablePager(this, params.pagerState, params.totalCount);
        this.currentOrder.subscribe(order => this.pager.setSorting(order));

        this.componentWidth.subscribe(() => this.recalcColumns());
        App.instance().windowWidth.subscribe(() => this._startUpdateComponentWidth());
        App.instance().leftPanelWidth.subscribe(() => this._startUpdateComponentWidth());
        App.instance().windowHeight.subscribe(() => this._updateBodyHeight());
    }

    setParentContext(context: any) {
        this.parentContext = context;
        this._updateComponentWidth();
        this._updateBodyHeight();
        this.allColumns.valueHasMutated();
    }

    recalcColumns(force?: boolean) {
        if (!force && this.columnsTemplatesCount > 0 && this.allColumns().length !== this.columnsTemplatesCount ||
            this.componentWidth() === 0) {
            //  not all awaitable 'cmp-table-column' are rendered yet
            return;
        }

        switch(this.recalcColumnsMode) {
            case RecalcColumnsMode.ByTableWidth:
                this._recalcColumnsByTableWidth();
                break;
            case RecalcColumnsMode.ByMaxColumnWidth:
            default:
                this._recalcColumnsByMaxColumsWidth();
                break;
        }

        this.columnsTemplatesCount = 0;
    }

    getSortingForField(fieldname: string): string {
        var parts = this.currentOrder().split(' ');
        if (parts[0] !== fieldname) {
            return '';
        }

        return parts.length > 1 && parts[1] === 'desc'
            ? 'desc'
            : 'asc';
    }

    private _updateComponentWidth() {
        const displayMode = this.componentElement.css('display');
        this.componentElement.css('display', 'block');
        const width = this.componentElement.width();
        this.componentElement.css('display', displayMode);

        this.componentWidth(width);
    }

    private _updateWidthComponentTimeout = null;
    private _startUpdateComponentWidth() {
        if(this._updateWidthComponentTimeout) {
            clearTimeout(this._updateWidthComponentTimeout);
        }

        this._updateWidthComponentTimeout = setTimeout(() => {
            this._updateComponentWidth();
            this._updateWidthComponentTimeout = null;
        }, 100);
    }

    private _updateBodyHeightTimeout = null;
    private _updateBodyHeight() {
        if(this._updateBodyHeightTimeout) {
            clearTimeout(this._updateBodyHeightTimeout);
        }

        this._updateBodyHeightTimeout = setTimeout(() => {
            const componentRect = this.componentElement[0].getBoundingClientRect();
            const theadRect = $('.js-table > thead', this.componentElement)[0].getBoundingClientRect();
            const pagerRect = $('.js-pager', this.componentElement)[0].getBoundingClientRect();

            const tbodyElement = $('.js-table > tbody', this.componentElement);
            tbodyElement.height(Math.trunc(componentRect.height - theadRect.height - pagerRect.height));
            this._updateBodyHeightTimeout = null;
        }, 100);
    }

    private _mapColumns(columns: ITableColumnParams[] | ko.ObservableArray<ITableColumnParams>):
        TableColumnModel[] {
        var colsArr = ko.utils.unwrapObservable(columns);
        return ko.utils.arrayMap(colsArr, colpars => {
            const column: TableColumnModel = new TableColumnModel(colpars);
            column.table = this;
            return column;
        });
    }

    private _recalcColumnsByMaxColumsWidth() {
        const maxColsCount = Math.floor((this.componentWidth() - 54) / 150);
        ko.utils.arrayForEach(this.allColumns(), col => col.isChild = false);
        const largeCols = this.allColumns();
        for (let notChildrenCols = ko.utils.arrayFilter(largeCols, col => !col.isChild);
            notChildrenCols.length > maxColsCount;
            notChildrenCols = ko.utils.arrayFilter(largeCols, col => !col.isChild)) {
            const minPriority = Math.min(...ko.utils.arrayMap(notChildrenCols, col => col.priority));
            const colsForDropdown = ko.utils.arrayFilter(notChildrenCols, col => col.priority === minPriority);
            if (colsForDropdown.length) {
                colsForDropdown.pop().isChild = true;
            } else {
                break;
            }
        }

        this.parentColumns(ko.utils.arrayFilter(largeCols, col => !col.isChild));
        this.childColumns(ko.utils.arrayFilter(largeCols, col => col.isChild));
    }

    private _recalcColumnsByTableWidth() {
        this.componentElement.css('display', 'block').css('position', 'absolute');
        const $table = $('table.large-view', this.componentElement);
        $table.css('visibility', 'hidden').css('position', 'absolute');
        ko.utils.arrayForEach(this.allColumns(), col => col.isChild = false);
        let largeCols = this.allColumns();
        this.parentColumns(largeCols);
        while ($table.width() > this.componentWidth()) {
            const notChildCols = ko.utils.arrayFilter(largeCols, col => !col.isChild);
            const minPriority = Math.min(...ko.utils.arrayMap(notChildCols, col => col.priority));
            const colsForHide = ko.utils.arrayFilter(notChildCols, col => col.priority === minPriority);
            if (colsForHide.length) {
                colsForHide.pop().isChild = true;
                this.parentColumns(ko.utils.arrayFilter(largeCols, col => !col.isChild));
            } else {
                break;
            }
        }

        $table.css('visibility', '').css('position', '');
        this.componentElement.css('display', '').css('position', '');

        this.childColumns(ko.utils.arrayFilter(largeCols, col => col.isChild));
    }
}

enum RecalcColumnsMode {
    ByMaxColumnWidth = 1,
    ByTableWidth
}

class TableRow {
    cells: ko.Computed<IDataDescriptor[]>;
    children: ko.Computed<IChildCellDescriptor[]>;
    hasChildColumns: ko.Computed<boolean>;
    isExpanded = ko.observable(false);

    constructor(table: TableModel, row: any) {
        this.hasChildColumns = table.hasChildColumns;

        this.cells = ko.computed(() => {
            return ko.utils.arrayMap(table.parentColumns(), col => {
                return <IDataDescriptor>{
                    parent: row,
                    data: row[col.fieldName],
                    nodes: col.dataTemplateNodes
                };
            });
        });

        this.children = ko.computed(() => {
            return ko.utils.arrayMap(table.childColumns(), col => <IChildCellDescriptor>{
                title: {
                    data: ko.utils.unwrapObservable(col.title),
                    parent: table.parentContext,
                    nodes: col.childTitleTemplateNodes
                },
                content: {
                    parent: row,
                    data: row[col.fieldName],
                    nodes: col.childDataTemplateNodes
                }
            });
        });
    }

    toggle() {
        this.isExpanded(!this.isExpanded());
    }
}

class TablePager {
    skipCount = ko.observable(0);
    maxResultCountValue: ko.Observable<ITablePagerOption>;
    maxResultCountOptions: ko.ObservableArray<ITablePagerOption>;
    totalCount = ko.observable(0);
    buttons = ko.observableArray<ITablePagerButton>([]);

    currentPage: ko.Computed<number>;
    firstEnabled: ko.Computed<boolean>;
    lastEnabled: ko.Computed<boolean>;

    private table: TableModel;
    private state: ko.Observable<ITablePagerState>;
    private isUpdating = false;
    private options: ITablePagerOption[] = [
        { value: 10, text: '10'},
        { value: 25, text: '25'},
        { value: 50, text: '50'},
        { value: 100, text: '100'},
    ];

    constructor(table: TableModel, state: ko.Observable<ITablePagerState>, totalCount: ko.Observable<number>) {
        this.table = table;
        this.state = ko.isObservable(state) ? state : ko.observable<ITablePagerState>({
            skipCount: 0,
            maxResultCount: 10,
            sorting: ''
        });
        this.totalCount = ko.isObservable(totalCount) ? totalCount : ko.observable(0);

        this.maxResultCountOptions = ko.observableArray(this.options);
        this.maxResultCountValue = ko.observable(ko.utils.arrayFirst(this.options, opt => opt.value === this.state().maxResultCount) || this.options[0]);

        this.state.subscribe(() => this.updateState());
        this.totalCount.subscribe(() => {
            this.updateMaxResultOptions();
            this.updateState();
        });
        this.updateState();

        this.skipCount.subscribe(val => {
            if (this.isUpdating) {
                return;
            }

            state().skipCount = val;
            state.valueHasMutated();
        });

        this.maxResultCountValue.subscribe(val => {
            if (this.isUpdating) {
                return;
            }

            state().maxResultCount = val.value;
            state.valueHasMutated();
        });

        this.currentPage = ko.computed(() => Math.floor(this.skipCount() / this.maxResultCountValue().value) + 1);
        this.firstEnabled = ko.computed(() => Math.floor(this.skipCount() / this.maxResultCountValue().value) > 0);
        this.lastEnabled = ko.computed(() => Math.floor(this.skipCount() / this.maxResultCountValue().value) < Math.ceil(this.totalCount() / this.maxResultCountValue().value - 1));
    }

    setSorting(sorting: string) {
        this.state().sorting = sorting;
        this.state.valueHasMutated();
    }

    first() {
        if (this.skipCount() > 0) {
            this.skipCount(0);
        }
    }

    prev() {
        const activePageNum = Math.floor(this.skipCount() / this.maxResultCountValue().value);
        const skipCount = Math.max(0, activePageNum - 1) * this.maxResultCountValue().value;
        this.skipCount(skipCount);
    }

    next() {
        const activePageNum = Math.floor(this.skipCount() / this.maxResultCountValue().value);
        const totalPagesCount = Math.ceil(this.totalCount() / this.maxResultCountValue().value);
        const skipCount = Math.min(totalPagesCount - 1, activePageNum + 1) * this.maxResultCountValue().value;
        this.skipCount(skipCount);
    }

    last() {
        const totalPagesCount = Math.ceil(this.totalCount() / this.maxResultCountValue().value);
        const skipCount = (totalPagesCount - 1) * this.maxResultCountValue().value;
        this.skipCount(skipCount);
    }

    private updateState() {
        if (this.isUpdating) {
            return;
        }

        this.isUpdating = true;
        this.skipCount(this.state().skipCount);
        this.maxResultCountValue(ko.utils.arrayFirst(this.options, opt => opt.value === this.state().maxResultCount) || this.options[0]);
        this.updateButtons();
        this.isUpdating = false;
    }

    updateMaxResultOptions() {
        const options = [];
        options.push(this.options[0])
        if (this.totalCount() > 25) {
            options.push(this.options[1]);
        }
        if (this.totalCount() > 50) {
            options.push(this.options[2]);
        }
        if (this.totalCount() > 100) {
            options.push(this.options[3]);
        }

        this.maxResultCountOptions(options);
    }

    private updateButtons() {
        const activePageNum = Math.floor(this.skipCount() / this.maxResultCountValue().value);
        const totalPagesCount = Math.ceil(this.totalCount() / this.maxResultCountValue().value);
        const buttons: ITablePagerButton[] = [];

        if (totalPagesCount < 8) {
            for (let n = 0; n < totalPagesCount; n++) {
                buttons.push(this.createPageButton(n, n === activePageNum));
            }
        } else {
            if (activePageNum < 4) {
                for (let n = 0; n < 5; n++) {
                    buttons.push(this.createPageButton(n, n === activePageNum));
                }
                buttons.push(this.createEmptyButton());
                buttons.push(this.createPageButton(totalPagesCount - 1, false));
            } else {
                buttons.push(this.createPageButton(0, false));
                buttons.push(this.createEmptyButton());

                if (activePageNum > totalPagesCount - 5) {
                    for (let n = totalPagesCount - 5; n < totalPagesCount; n++) {
                        buttons.push(this.createPageButton(n, n === activePageNum));
                    }
                } else {
                    buttons.push(this.createPageButton(activePageNum - 1, false));
                    buttons.push(this.createPageButton(activePageNum, true));
                    buttons.push(this.createPageButton(activePageNum + 1, false));
                    buttons.push(this.createEmptyButton());
                    buttons.push(this.createPageButton(totalPagesCount - 1, false));
                }
            }
        }

        this.buttons(buttons);
    }

    private createPageButton(pageNum: number, isActive: boolean): ITablePagerButton {
        return {
            title: (pageNum + 1).toString(),
            isActive: isActive,
            action: () => {
                const state = this.state();
                state.skipCount = pageNum * state.maxResultCount;
                this.state.valueHasMutated();
            }
        }
    }

    private createEmptyButton(): ITablePagerButton {
        return {
            title: 'space',
            isActive: false,
            action: null
        };
    }
}

class TableColumnModel {
    titleTemplateNodes: Node[];
    dataTemplateNodes: Node[];
    childTitleTemplateNodes: Node[];
    childDataTemplateNodes: Node[];
    title: string | ko.Observable<string>;
    orderable: boolean;
    priority: number;
    fieldName: string;
    isChild = false;
    table: TableModel;

    currentOrder = ko.observable('');

    constructor(params: ITableColumnParams, componentInfo?: any) {

        this.titleTemplateNodes = this.getTemplateNodes(
            componentInfo ? componentInfo.templateNodes : [],
            params.titleTemplate,
            'table-parent-title-default.html',
            myepTableTitle);
        this.dataTemplateNodes = this.getTemplateNodes(
            componentInfo ? componentInfo.templateNodes : [],
            params.cellTemplate,
            'table-parent-data-default.html',
            myepTableData,
            null,
            true);
        this.childTitleTemplateNodes = this.getTemplateNodes(
            componentInfo ? componentInfo.templateNodes : [],
            params.childTitleTemplate,
            'table-child-title-default.html',
            myepTableChildTitle);
        this.childDataTemplateNodes = this.getTemplateNodes(
            componentInfo ? componentInfo.templateNodes : [],
            params.childCellTemplate,
            'table-child-data-default.html',
            myepTableChildData,
            this.dataTemplateNodes);

        this.title = params.title || null;
        this.priority = params.priority || 0;
        this.fieldName = params.data || null;
        this.orderable = params.orderable || false;

        if (this.table) {
            this.subscribeToCurrentOrder();
        }
    }

    afterRender(table: TableModel) {
        this.table = table;
        this.table.allColumns.push(this);
        this.subscribeToCurrentOrder();
    }

    asc() {
        if (this.orderable) {
            this.table.currentOrder(`${this.fieldName} asc`);
        }
    }

    desc() {
        if (this.orderable) {
            this.table.currentOrder(`${this.fieldName} desc`);
        }
    }

    private subscribeToCurrentOrder() {
        this.table.currentOrder.subscribe(order => {
            const currentOrderParts =order.split(' ');

            if (currentOrderParts[0] !== this.fieldName) {
                this.currentOrder('');
            } else {
                this.currentOrder(currentOrderParts.length > 1 && currentOrderParts[1] === 'desc' ? 'desc' : 'asc');
            }
        });
    }

    private getTemplateNodes(
        nodes: Node[],
        template: string,
        defaultTemplate: string,
        templateTagName: string,
        inheritedNodes?: Node[],
        rootNodesByDefault?:boolean): Node[] {
        if (ko.utils.arrayFirst(nodes, n => n.nodeName !== '#text')) {
            let result = ko.utils.arrayFilter(nodes, n => (<Element>n).localName === templateTagName);
            if (result.length) {
                return result;
            }

            if (rootNodesByDefault && !ko.utils.arrayFilter(nodes, n => ((<Element>n).localName||'').indexOf('cmp-table-') === 0).length) {
                return nodes;
            }
        }

        if (template) {
            return getTemplateNodesByUrlOrId(template);
        }

        if(inheritedNodes) {
            return inheritedNodes;
        }

        return getTemplateNodesByUrlOrId(templatesTablesFolderPath + defaultTemplate);
    }
}

function getTemplateNodesByUrlOrId(template: string): Node[] {
    if (template.indexOf('.html') > -1) {
        return Utils.getNodesFromFile(template);
    } else {
        return Utils.getNodesFromScriptElement(template);
    }
}

