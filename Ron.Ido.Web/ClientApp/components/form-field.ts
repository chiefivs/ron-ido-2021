import * as ko from 'knockout';
import { IFormField } from '../modules/forms';
import { Utils } from '../modules/utils';

export function init() {
    ko.components.register('cmp-form-field',
    {
        viewModel: {
            createViewModel(params: IFormFieldParams, componentInfo: any) {
                return new FormFieldModel(params, componentInfo);
            }
        },
        template: `
            <div>
                <div>
                    <div class="errors">
                        <i class="img img-error img-size-20" data-bind="visible:errors().length, attr: { title:errors }"></i>
                    </div>
                    <div data-bind="template:{nodes:templateNodes, data:data}"></div>
                </div>
            <div>`
    });
}

export interface IFormFieldParams {
    field: IFormField;
}

class FormFieldModel {
    templateNodes: Node[];
    data: IFormField;
    errors: ko.Computed<string>;
    css: { [key:string]:ko.Computed<boolean> };

    private _defaultTemplate = '<cmp-textbox params="value:value, css:$parent.css"></cmp-textbox>';
    constructor(params: IFormFieldParams, componentInfo: any) {
        this.data = params.field;
        this.templateNodes = componentInfo.templateNodes.length
            ? componentInfo.templateNodes
            : Utils.getNodesFromHtml(this._defaultTemplate);

        this.errors = ko.computed(() => this.data.errors().join('\n'));

        this.css = {
            'has-changes': ko.computed(() => this.data.hasChanges()),
            'has-errors': ko.computed(() => !!this.data.errors().length)
        };
    }
}