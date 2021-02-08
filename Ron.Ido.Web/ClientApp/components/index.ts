import * as LeftTabsComponent from './left-panel';
import * as MainTabsComponent from './main-panel';

export function init() {
    LeftTabsComponent.init();
    MainTabsComponent.init();
}

export { ILeftPanelParams } from './left-panel';
export { IMainPanelParams } from './main-panel';