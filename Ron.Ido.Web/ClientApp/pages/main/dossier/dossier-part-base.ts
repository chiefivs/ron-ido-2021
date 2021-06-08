import * as ko from 'knockout';
import { Control, IControlParams } from '../../../modules/content';

export interface IDossier {
    id: number;
}

export interface IDossierPartBaseParams extends IControlParams {
    owner: IDossier;
}

export abstract class DossierPartBase extends Control {
    isVisible = ko.observable(false);

    private _owner: IDossier;

    constructor(params: IDossierPartBaseParams) {
        super(params);
    }

    onClose(): boolean {
        return true;
    }
}