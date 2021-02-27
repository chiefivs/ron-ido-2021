import * as LeftTabsComponent from './left-panel';
import * as MainTabsComponent from './main-panel';
import * as SelectComponent from './select';
import * as TableComponent from './table';

export function init() {
    LeftTabsComponent.init();
    MainTabsComponent.init();
    SelectComponent.init();
    TableComponent.init();
}

export { ILeftPanelParams } from './left-panel';
export { IMainPanelParams } from './main-panel';
export { ISelectParams } from './select';
export { ITableParams, ITableBlockParams, ITableColumnParams, ITablePagerState, TableBlockVisibleOptions, TableColumnOrderDirection } from './table';