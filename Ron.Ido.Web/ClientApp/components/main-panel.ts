import * as ko from 'knockout';
import { IMainPage } from '../modules/content';
import { Utils } from '../modules/utils';

export function init(){
    ko.components.register('cmp-main-panel', {
        viewModel: {
            createViewModel: function(params, componentInfo) {
                return new MainPanelModel(params);
            }
        },
        template: `
            <div class="main-panel-tabs" data-bind="css:{'no-pages':!pages().length}">
                <div data-bind="template:{ nodes: tabsTemplateNodes, data: $data, afterRender: afterRender.bind($data) }"></div>
                <div class="glyphicon glyphicon-menu-hamburger" data-bind="visible:tail().length"></div>
            </div>
            <div class="main-panel-pages" data-bind="css:{'no-active':!active(), 'no-pages':!pages().length}">
                <div class="main-page-container" data-bind="with:active">
                    <!-- ko template: {nodes: $data ? templateNodes : [], data:$data} -->
                    <!-- /ko -->
                 </div>
            </div>
            <div class="tail-list">
                <div>
                    <div data-bind="foreach:tail">
                        <div data-bind="text:pageTitle"></div>
                    </div>
                </div>
            </div>`
    });
}

export interface IMainPanelParams {
    pages: IMainPage[] | ko.ObservableArray<IMainPage>;
    active: ko.Observable<IMainPage>;
}

class MainPanelModel {
    tabsTemplateNodes = Utils.getNodesFromHtml(`
        <!-- ko foreach:pages -->
        <div class="main-tab" data-bind="css:{'active':$parent.isActive($data)}, click:function(){$parent.setActive($data);}">
            <div data-bind="text:pageTitle"></div>
            <button class="close" data-bind="click:function(){$parent.close($data);}"><span>&times;</span></button>
            </div>
        <!-- /ko -->`);

    pages: ko.ObservableArray<IMainPage>;
    tail: ko.ObservableArray<IMainPage> = ko.observableArray([]);
    active: ko.Observable<IMainPage>;

    private _tabsPanelElement: JQuery<Element>;

    constructor(params: IMainPanelParams){
        this.pages = ko.isObservable(params.pages)
            ? params.pages
            : ko.observableArray(params.pages);

        this.active = params.active || ko.observable(null);
        this.tail.subscribe(v => console.log('tail', v));

        $(window).on('resize', () => this._reorderTabs());
        $(document).on('left-panel-resize', () => this._reorderTabs());

        this.active.subscribe(page => console.log('main panel active page', page));
    }

    afterRender(elements:Element[]) {
        this._tabsPanelElement = $(elements).closest('.main-panel-tabs');
        this._reorderTabs();
    }

    setActive(item: IMainPage): void {
        if(this.isActive(item))
            return;

        this.active(item);
    }

    close(item: IMainPage) {
        if(item.close && !item.close())
            return;

        const pages = this.pages();
        let index = ko.utils.arrayIndexOf(pages, item);
        ko.utils.arrayRemoveItem(pages, item);

        if(this.isActive(item)){
            index = Math.min(index, pages.length - 1);
            this.setActive(index >= 0 ? pages[index] : null);
        }

        this.pages(pages);
        this._reorderTabs();
    }

    isActive(item: IMainPage): boolean {
        return item === this.active();
    }

    private _reorderTabs() {
        if(!this._tabsPanelElement || !this._tabsPanelElement.length)
            return;
        
        let maxRight = this._tabsPanelElement[0].getBoundingClientRect().right;
        const tabs = $('.main-tab', this._tabsPanelElement);
        if(!tabs.length || tabs[tabs.length - 1].getBoundingClientRect().right <= maxRight){
            tabs.css('visibility', '');
            this.tail([]);
            return;
        }

        maxRight -= 30;
        const tail: IMainPage[] = [];
        tabs.each((i, e) => {
            if(e.getBoundingClientRect().right < maxRight) {
                $(e).css('visibility', '');
            } else {
                $(e).css('visibility', 'hidden');
                tail.push(this.pages()[i]);
            }
        });
        this.tail(tail);
    }
}