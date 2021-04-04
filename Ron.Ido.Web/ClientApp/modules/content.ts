import { readyException } from 'jquery';
import * as ko from 'knockout';
import { App } from '../app';
import { Utils } from './utils';

interface IControl {
    templateNodes: Element[];
}

export interface ILeftPage extends IControl {
    pageTitle: string | ko.Observable<string>;
    owner?: IMainPage;
}

export interface IMainPage extends IControl {
    pageTitle: ko.Observable<string>;
    pageKey: string;
    leftPages: ko.ObservableArray<ILeftPage>;
    activeLeftPage: ko.Observable<ILeftPage>;
    close: () => void;
    afterRender: (nodes:Node[]) => void;
}

export interface IPageParams extends IControlParams {
    pageTitle: string | ko.Observable<string>;
}

export interface IControlParams {
    templatePath?: string;
    templateHtml?: string;
}

export abstract class Control implements IControl {
    public templateNodes: Element[];

    constructor(params: IControlParams) {
        if(!params.templatePath && params.templateHtml === undefined)
            throw 'Control должен обязательно получить templatePath или templateHtml';

        this.templateNodes = params.templatePath
            ? Utils.getNodesFromFile(params.templatePath)
            : Utils.getNodesFromHtml(params.templateHtml);
    }
}

export abstract class LeftPageBase extends Control implements ILeftPage {
    pageTitle: string | ko.Observable<string>;
    owner?: IMainPage;

    constructor(params: IPageParams) {
        super(params);

        this.pageTitle = ko.isObservable(params.pageTitle) ? params.pageTitle : ko.observable(params.pageTitle);
    }
}

export abstract class MainPageBase extends Control implements IMainPage {
    pageTitle: ko.Observable<string>;
    pageKey: string;
    isActive: ko.Computed<boolean>;
    leftPages: ko.ObservableArray<ILeftPage> = null;
    activeLeftPage: ko.Observable<ILeftPage> = null;

    constructor(params: IPageParams) {
        super(params);

        this.pageTitle = ko.isObservable(params.pageTitle) ? params.pageTitle : ko.observable(params.pageTitle);
        this.isActive = ko.computed(() => App.instance().activeMainPage() === this);
    }

    close(): void {
        App.instance().closeMainPage(this);
    }

    afterRender(nodes:Node[]) {}
}

export namespace Popups {
    const minZIndex: number = 2001
    const popupInstancesName = 'app-popup-instances';

    function getInstances(): ko.ObservableArray<Popup> {
        if(!document[popupInstancesName])
        document[popupInstancesName] = ko.observableArray<Popup>([]);

        return document[popupInstancesName];
    }
 
    export interface IPopupParams extends IControlParams {
        width: number;
        height: number;
        left?: number;
        top?: number;
        isModal?: boolean;
        isDraggable?: boolean;
    }

    export interface IDialogButton {
        title: string;
        action: () => void;
        disabled: () => boolean;
    }

    export interface IDialogParams extends IPopupParams {
        title: string;
        image?: string;
    }

    export class PopupsCollection {
        modalZIndex: ko.Computed<number>;
        hasModals: ko.Computed<boolean>;
        instances: ko.ObservableArray<Popup>;

        constructor() {
            this.instances = getInstances();

            this.modalZIndex = ko.computed(() => {
                var z: number = 0;
                ko.utils.arrayForEach(this.instances(), (d) => {
                    if (d.isModal)
                        z = d.zIndex();
                });
        
                return z >= minZIndex ? z - 1 : 0;
            });

            this.hasModals = ko.computed(() => this.modalZIndex() > 0);
        }
    
        close() {
            if (this.instances().length <= 0)
                return;
    
            var popup = <Dialog>this.instances()[this.instances().length - 1];
            if (popup.close)
                popup.close();
        }
    } 

    export abstract class Popup extends Control {
        width: ko.Observable<number>;
        height: ko.Observable<number>;
        left: ko.Observable<number>;
        top: ko.Observable<number>;
        zIndex: ko.Observable<number>;
        isModal: boolean;
        isDraggable: boolean;
        draggableHandle: string = '';
    
        protected _instances: ko.ObservableArray<Popup>;

        constructor(params: IPopupParams) {
            super(params);

            this._instances = getInstances();

            this.width = ko.observable(params.width || 300);
            this.height = ko.observable(params.height || 200);
            this.left = ko.observable(params.left || ($(window).width() - params.width) / 2);
            this.top = ko.observable(params.top || ($(window).height() - params.height) / 2);
            this.isModal = params.isModal;
            this.isDraggable = params.isDraggable;
            this.zIndex = ko.observable(this._instances.length ? this._instances()[this._instances().length - 1].zIndex() + 10 : minZIndex);
        }

        protected _remove() {
            var i = this._instances.indexOf(this);
            for (var n: number = i + 1; n < this._instances.length; n++)
            this._instances()[n].zIndex(this._instances()[n - 1].zIndex());
    
            this._instances.remove(this);
        }
    
        protected _activate() {
            if (this._instances.length < 2)
                return;
    
            var i = this._instances.indexOf(this);
            this._instances.remove(this);
            this._instances.push(this);
    
            for (var n: number = i; n < this._instances().length; n++)
            this._instances()[n].zIndex(n > 0 ? this._instances()[n - 1].zIndex() : minZIndex);
        }
    }

    export class Dialog extends Popup {
        title = ko.observable('');
        image = ko.observable('');
        buttons = ko.observableArray<IDialogButton>([]);
        contentTemplateNodes: Node[];

        constructor(params: IDialogParams) {
            super({
                height: params.height,
                width: params.width,
                isDraggable: params.isDraggable,
                isModal: params.isModal,
                left: params.left,
                top: params.top,
                templateHtml: `
                    <div class="modal-header">
                        <button type="button" class="close" data-bind="click:close">×</button>
                        <h5>
                            <i class="img img-size-20" data-bind="class:image, visible:image"></i>
                            <span data-bind="text:title, style:{'padding-left':image() ? '2.5em' : ''}"></span>
                        </h5>
                    </div>
                    <div class="modal-body" data-bind="template:{nodes:contentTemplateNodes, data:$data}, style:{width:width() + 'px', 'max-height':height() + 'px', 'margin-bottom':buttons && buttons().length ? '' : '0'}"></div>
                    <!-- ko if:buttons && buttons().length -->
                    <div class="modal-footer">
                        <!-- ko foreach:buttons -->
                        <button class="btn btn-primary" data-bind="text:title, click:action, disable:disabled()"></button>
                        <!-- /ko -->
                    </div>
                    <!-- /ko -->`
            });

            this.title(params.title || '');
            this.image(params.image || '');

            this.contentTemplateNodes = params.templatePath
                ? Utils.getNodesFromFile(params.templatePath)
                : Utils.getNodesFromHtml(params.templateHtml);
        }

        show() {
            this._instances.push(this);
        }

        close(): void {
            this._remove();
        }
    }

    export class Alert extends Dialog {
        private constructor(title: string, message: string, isModal: boolean) {
            super({
                title: title,
                image: 'img-error',
                width:500,
                height:300,
                isModal: isModal,
                templateHtml: `<div>${message}</div>`
            });

            this.buttons([
                {title:'ЗАКРЫТЬ', action:() => this.close(), disabled:() => false}
            ]);
        }

        static open(title: string, message: string, isModal: boolean = false) {
            const alert = new Alert(title, message, isModal);
            alert.show();
        }
    }
}