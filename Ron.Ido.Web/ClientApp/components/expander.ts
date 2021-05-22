import * as ko from 'knockout';
import { App } from '../app';
import { Utils } from '../modules/utils';

const cmpExpander = 'cmp-expander';

export function init(){
    ko.components.register(cmpExpander,
        {
            viewModel: {
                createViewModel(params: IExpanderParams, componentInfo: any) {
    
                    return new ExpanderModel(params, componentInfo);
                }
            },
            template: `
                <div class="expander-header">
                    <a class="glyphicon glyphicon-pushpin expander-pin" data-bind="click:toggleFixed, visible:isFixable, css:{'fixed':isFixed}"></a>
                    <a class="expander-title" data-bind="html:title, click:toggleExpanded"></a>
                </div>
                <div class="expander-content" data-bind="template:{nodes:templateNodes, data:data, afterRender:afterRender.bind($data)}"></div>
            `
        });
}

export interface IExpanderParams {
    title: string | ko.Observable<string> | ko.Computed<string>;
    data: any;
    expanded?: boolean | ko.Observable<boolean>;
    fixed?: boolean | ko.Observable<boolean>;
}

export class ExpanderModel {
    templateNodes: Node[];
    data: any;
    title: ko.Observable<string> | ko.Computed<string>;
    isExpanded: ko.Observable<boolean>;
    isFixable: boolean;
    isFixed: ko.Observable<boolean>;
    contentElement: JQuery<Node[]> = null;

    constructor(params: IExpanderParams, componentInfo: any) {
        this.templateNodes = componentInfo.templateNodes;
        this.title = ko.isObservable(params.title) || ko.isComputed(params.title) ? params.title : ko.observable(params.title);
        this.isExpanded = ko.isObservable(params.expanded) ? params.expanded : ko.observable(params.expanded || false);
        this.data = params.data;

        this.isFixable = params.fixed !== undefined;
        this.isFixed = ko.isObservable(params.fixed) ? params.fixed : ko.observable(params.fixed || false);

        this.isExpanded.subscribe(expanded => {
            if(this.isFixed()) {
                return;
            }

            if(expanded) {
                this.contentElement.slideDown();
            } else {
                this.contentElement.slideUp();
            }
        });
    }

    afterRender(nodes:Node[]) {
        this.contentElement = $(nodes).parent();

        if(!this.isExpanded()) {
            this.contentElement.hide();
        }
    }

    toggleFixed() {
        const fixed = !this.isFixed();

        if(fixed) {
            this.isExpanded(true);
        } else {
            this.isExpanded.valueHasMutated();
        }

        this.isFixed(fixed);
    }

    toggleExpanded() {
        this.isExpanded(!this.isExpanded());
    }
}
