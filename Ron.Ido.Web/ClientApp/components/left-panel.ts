import * as ko from 'knockout';
import { App } from '../app';
import { ILeftPage } from '../modules/content';
import { Utils } from '../modules/utils';

export function init(){
    ko.components.register('cmp-left-panel', {
        viewModel: {
            createViewModel: function(params, componentInfo) {
                return new LeftPanelModel(params);
            }
        },
        template: `
            <div>
                <div class="left-panel-tabs">
                    <div data-bind="template:{ nodes: tabsTemplateNodes, data: $data, afterRender: afterRender.bind($data) }"></div>
                </div>
                <div class="left-panel-pages" data-bind="style:{width:widthString}">
                    <div class="left-page-container">
                        <div>
                            <div class="left-page-content" data-bind="foreach:pages">
                                <div data-bind="template:{nodes:templateNodes, data:$data}, visible:$data===$parent.active()"></div>
                            </div>
                            <button class="close" data-bind="click:close"><span>&times;</span></button>
                        </div>
                    </div>
                </div>
            </div>`
    });
}

export interface ILeftPanelParams {
    pages: ILeftPage[] | ko.ObservableArray<ILeftPage> | ko.Computed<ILeftPage>;
    active: ko.Observable<ILeftPage>;
    width: number | ko.Observable<number>;
}

export class LeftPanelModel {
    tabsTemplateNodes = Utils.getNodesFromHtml(`
        <!-- ko foreach:pages -->
        <div class="left-tab" data-bind="css:{'active':$parent.isActive($data)}, click:function(){$parent.setActive($data);}">
            <div data-bind="text:pageTitle"></div>
        </div>
        <!-- /ko -->`);

    pages: ko.ObservableArray<ILeftPage> | ko.Computed<ILeftPage>;
    active: ko.Observable<ILeftPage>;
    widthString: ko.Computed<string>;

    private width: ko.Observable<number>;
    private tabElements: JQuery<Element> = null;
    //private defaultWidth: number;

    constructor(params: ILeftPanelParams){
        this.pages = ko.isObservable(params.pages)
            ? params.pages
            : ko.isComputed(params.pages)
                ? params.pages
                : ko.observableArray(params.pages);

        this.active = params.active || ko.observable(null);

        this.width = ko.isObservable(params.width) ? params.width : ko.observable(330);
        this.widthString = ko.computed(() => `${(this.width()-30)}px`);

        App.instance().contentVisible.subscribe(() => this._refresh());
    }

    afterRender(elements:Element[]) {
        this.tabElements = $(elements);
        this._refresh();
    }

    setActive(item: ILeftPage): void {
        if(!this.isActive(item))
            this.active(item);

        if(this.width() < App.LEFT_PANEL_WIDTH_DEFAULT)
            Utils.animate(this.width(), App.LEFT_PANEL_WIDTH_DEFAULT, v => this._setWidth(v));
    }

    close() {
        Utils.animate(this.width(), 38, w => this._setWidth(w));
    }

    isActive(item: ILeftPage): boolean {
        return item === this.active();
    }

    private _setWidth(width: number) {
        this.width(width);
    }

    private _refresh() {
        if(!App.instance().contentVisible)
            return;
            
        let top = 0;
        this.tabElements
            .filter('div')
            .each((i, e) => {
                var divElem = $('div', e);
                divElem.removeAttr('style');
                const divRect = divElem[0].getBoundingClientRect();
                const height = Math.ceil(divRect.width) + 20;

                $(e).height(height).css('top', `${top}px`);
                top += height + 1;

                divElem
                    .css('left', `${20 - height/2}px`)
                    .css('top', `${(height - divRect.height)/2}px`)
                    .css('transform', 'rotate(-90deg)');
            });

    }
}