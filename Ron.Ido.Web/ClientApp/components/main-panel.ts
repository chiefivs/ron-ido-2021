import * as ko from 'knockout';
import { App } from '../app';
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
                <div class="glyphicon glyphicon-menu-hamburger" data-bind="visible:tail().length, click:toggleTail"></div>
            </div>
            <div class="main-panel-pages" data-bind="css:{'no-active':!active(), 'no-pages':!pages().length}">
                <div class="main-page-container" data-bind="with:active">
                    <!-- ko template: {nodes: $data ? templateNodes : [], data:$data} -->
                    <!-- /ko -->
                 </div>
            </div>
            <div class="tail-list" data-bind="click:tailInsideClick">
                <div class="tail-list-inner" data-bind="visible:isTailVisible, style:{width: tailWidth()+'px', height: tailHeight()+'px'}">
                    <div data-bind="foreach:tail">
                        <div class="tail-item" draggable="true" data-bind="css:{'active':$parent.isActive($data), 'dragover':$parent.isDragOver($data)},
                          event:{dragover:$parent.tabDragOver.bind($parent), dragleave:$parent.tabDragLeave.bind($parent), dragend:$parent.tabDragEnd.bind($parent)}">
                            <span data-bind="text:pageTitle, click:function(){$parent.setActiveFromTail($data);}"></span>
                            <a class="close" data-bind="click:function(page,evt){$parent.closePage(page,evt);}"><span>&times;</span></a>
                        </div>
                    </div>
                </div>
            </div>`
    });
}

export interface IMainPanelParams {
    pages: ko.ObservableArray<IMainPage>;
    active: ko.Observable<IMainPage>;
}

class MainPanelModel {
    tabsTemplateNodes = Utils.getNodesFromHtml(`
        <!-- ko foreach:pages -->
        <div class="main-tab" draggable="true" data-bind="css:{'active':$parent.isActive($data), 'dragover':$parent.isDragOver($data)},
          event:{dragover:$parent.tabDragOver.bind($parent), dragleave:$parent.tabDragLeave.bind($parent), dragend:$parent.tabDragEnd.bind($parent)},
          click:function(){$parent.setActive($data);}">
            <div data-bind="text:pageTitle"></div>
            <a class="close" data-bind="click:function(page,evt){$parent.closePage(page,evt);}"><span>&times;</span></a>
            </div>
        <!-- /ko -->`);

    pages: ko.ObservableArray<IMainPage>;
    pageDragOver = ko.observable<IMainPage>(null);
    tail: ko.ObservableArray<IMainPage> = ko.observableArray([]);
    active: ko.Observable<IMainPage>;
    isTailVisible: ko.Computed<boolean>;
    tailWidth = ko.observable(0);
    tailHeight = ko.observable(0);

    private _tabsPanelElement: JQuery<Element>;
    private _tailListElement: JQuery<Element>;
    private _tailVisible = ko.observable(false);

    constructor(params: IMainPanelParams){
        this.pages = params.pages;
        this.pages.subscribe(() => setTimeout(() => this._reorderTabs(), 100));

        this.active = params.active || ko.observable(null);
        this.isTailVisible = ko.computed(() => this.tail().length && this.tailWidth() > 0 && this.tailHeight() > 0);
        
        const outsideTailClick = () => this._tailVisible(false);
        this._tailVisible.subscribe(visible => {
            if(visible) {
                const rect = this._getTailRect();
                Utils.animate(0, 1,
                v => {
                    this.tailWidth(Math.round(v*rect.width));
                    this.tailHeight(Math.round(v*rect.height));
                },
                () => this._tailListElement.css('width', '').css('height', ''));
                setTimeout(() => $(document).on('click', outsideTailClick), 100);
            } else {
                $(document).off('click', outsideTailClick);
                const rect = this._getTailRect();
                Utils.animate(1, 0, 
                v => {
                    this.tailWidth(Math.round(v*rect.width));
                    this.tailHeight(Math.round(v*rect.height));
                },
                () => this._tailListElement.css('width', '').css('height', ''));
            }
        });

        App.instance().windowWidth.subscribe(() => this._reorderTabs());
        App.instance().leftPanelWidth.subscribe(() => this._reorderTabs());
        App.instance().contentVisible.subscribe(() => this._reorderTabs());
    }

    afterRender(elements:Element[]) {
        this._tabsPanelElement = $(elements).closest('.main-panel-tabs');
        this._tailListElement = $('.tail-list-inner', this._tabsPanelElement.siblings('.tail-list'));
        this._reorderTabs();
    }

    setActive(item: IMainPage) {
        if(this.isActive(item))
            return;

        this.active(item);
    }

    setActiveFromTail(item: IMainPage) {
        this.setActive(item);
        this._tailVisible(false);
    }

    isActive(page: IMainPage): boolean {
        return page === this.active();
    }

    closePage(page:IMainPage, evt:JQuery.Event) {
        evt.stopPropagation();
        page.close();
    }

    isDragOver(page: IMainPage): boolean {
        return page === this.pageDragOver();
    }

    toggleTail() {
        this._tailVisible(!this._tailVisible());
    }

    tailInsideClick(data:any, evt:JQuery.Event) {
        evt.stopPropagation();
        return false;
    }

    tabDragOver(page:IMainPage) {
        this.pageDragOver(page);
    }

    tabDragLeave(page:IMainPage) {
        this.pageDragOver(null);
    }

    tabDragEnd(page:IMainPage) {
        if(this.pageDragOver() && page !== this.pageDragOver()) {
            const pages = this.pages();
            const index = ko.utils.arrayIndexOf(pages, this.pageDragOver());
            ko.utils.arrayRemoveItem(pages, page);
            pages.splice(index, 0, page);
            this.pages(pages);
            this._reorderTabs();
        }

        this.pageDragOver(null);
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

        maxRight -= 40;
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

    private _getTailRect() {
        const displayVal = this._tailListElement.css('display');
        this._tailListElement.css('display', 'block').css('visibility', 'hidden').css('width', '').css('height', '');
        const rect = this._tailListElement[0].getBoundingClientRect();
        this._tailListElement.css('display', displayVal).css('visibility', '');

        return rect;
    }
}