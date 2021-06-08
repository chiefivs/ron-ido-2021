import * as ko from 'knockout';
import { Control, IControlParams } from '../../../modules/content';

export interface IDossier {
    id: number;
    parts: ko.ObservableArray<DossierPartBase>;
}

export interface IDossierPartBaseParams extends IControlParams {
    owner: IDossier;
}

export abstract class DossierPartBase extends Control {
    isVisible = ko.observable(false);
    priority: number = 0;

    private _owner: IDossier;

    constructor(params: IDossierPartBaseParams) {
        super(params);
        this._owner = params.owner;
    }

    close(): boolean {
        this._owner.parts.remove(this);
        return true;
    }

    width(): string {
        return `${this._round(this._getWidth())}%`;
    }

    left(index:number): string {
        return `${this._round(this._getWidth() * index)}%`;
    }

    private _getWidth() {
        return Math.min(50, 100 / this._owner.parts().length);
    }

    private _round(val: number) {
        return Math.round(100 * val) / 100;
    }
}