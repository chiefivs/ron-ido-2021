import { DossierPartBase, IDossier } from './dossier-part-base';
import { Form, IFormField } from '../../../modules/forms';
import { DossierApi } from '../../../codegen/webapi/dossierApi';
import { IODataForm } from '../../../codegen/webapi/odata';

export class Apply extends DossierPartBase {
    form: ApplyForm;

    constructor(id: number, owner: IDossier) {
        super({
            templatePath: 'pages/main/dossier/dossier-apply.html',
            owner: owner
        });

        this.priority = 0;

        DossierApi.getApply(id).done(data => this.form = new ApplyForm(data));
    }
}

class ApplyForm extends Form<DossierApi.IApplyDto> {
    constructor(data: IODataForm<DossierApi.IApplyDto>) {
        super(data, DossierApi.validateApply, DossierApi.saveApply);
    }
}

interface IApplyFormField extends IFormField {
    title: string;
    readonly: boolean;
}

