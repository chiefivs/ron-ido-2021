import * as ko from 'knockout';

export function init(){
    ko.components.register('cmp-left-tabs', {
        viewModel: {
            createViewModel: function(params, componentInfo) {
                return new LeftTabsComponent(params);
            }
        },
        template: `<div data-bind="template:{ nodes: templateNodes, data: $data, afterRender: afterRender }"></div>`
    });
}

export interface ILeftTab {
    title: string | ko.Observable<string>;
}

export interface ILeftTabsParams {
    tabs: ILeftTab[] | ko.ObservableArray<ILeftTab> | ko.Computed<ILeftTab>;
    active: ko.Observable<ILeftTab>;
}

export class LeftTabsComponent {
    templateNodes: Element[];
    tabs: ko.ObservableArray<ILeftTab> | ko.Computed<ILeftTab>;
    active: ko.Observable<ILeftTab>;

    constructor(params: ILeftTabsParams){
        this._createTemplateNodes();
        this.tabs = ko.isObservable(params.tabs)
            ? params.tabs
            : ko.isComputed(params.tabs)
                ? params.tabs
                : ko.observableArray(params.tabs);

        this.active = params.active || ko.observable(null);
    }

    afterRender(elements:Element[]) {
        let top = 0;
        $(elements)
            .filter('div')
            .each((i, e) => {
                var divElem = $('div', e);
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

    setActive(item: ILeftTab): void {
        if(!this.isActive(item))
            this.active(item);
    }

    isActive(item: ILeftTab): boolean {
        return item === this.active();
    }

    private _createTemplateNodes() {
        
        const template = $(`
            <!-- ko foreach:tabs -->
            <div class="left-tab" data-bind="css:{'active':$parent.isActive($data)}, click:function(){$parent.setActive($data);}">
                <div data-bind="text:title"></div>
            </div>
            <!-- /ko -->`
        );

        this.templateNodes = template.toArray();
    }
}