import * as ko from 'knockout';
import { Template } from './template';

abstract class Control {
    public templateNodes: Element[];

    constructor(templatePath: string) {
        this.templateNodes = Template.getNodes(templatePath);
    }
}

export namespace Popups {
    const minZIndex: number = 2001
    const popupInstancesName = 'app-popup-instances';

    function getInstances(): ko.ObservableArray<Popup> {
        if(!document[popupInstancesName])
        document[popupInstancesName] = ko.observableArray<Popup>([]);

        return document[popupInstancesName];
    }
 
    export interface IPopupParams {
        width: number;
        height: number;
        templatePath: string;
        left?: number;
        top?: number;
        isModal?: boolean;
        isDraggable?: boolean;
     }

     export class PopupsCollection {
        modalZIndex: ko.Computed<number>;
        hasModals: ko.Computed<boolean>;
        instances: ko.ObservableArray<Popup>;

        constructor() {
            this.instances = getInstances();
            this.instances.subscribe(i => console.log(i));

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
            super(params.templatePath);

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

        close(): void {

        }
    }
}