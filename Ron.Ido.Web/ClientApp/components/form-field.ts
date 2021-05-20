import * as ko from 'knockout';
import { Form, IFormField } from '../modules/forms';
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
                        <i class="img img-error img-size-20" data-bind="visible:errors, attr: { title:errors }"></i>
                    </div>
                    <div data-bind="template:{nodes:templateNodes, data:data}"></div>
                </div>
            <div>`
    });

    ko.components.register('cmp-form-errors', {
        viewModel: {
            createViewModel(params:IFormErrorsParams) {
                return params;
            }
        },
        template: `
            <div class="form-field-container" data-bind="visible:form.errors().length">
                <div>
                    <div class="errors"><i class="img img-error img-size-20"></i></div>
                    <div class="form-control" style="padding:4px 0;">
                        <ul data-bind="foreach:form.errors" style="padding-left:20px;">
                            <li data-bind="text:$data"></li>
                        </ul>
                    </div>
                </div>
            </div>`
    });
}

export interface IFormFieldParams {
    field: IFormField;
}

export interface IFormErrorsParams {
    form: Form<any>;
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

        this.errors = ko.computed(() => {
            return this.data.errors().join('\n');
        });

        this.css = {
            'has-changes': ko.computed(() => this.data.hasChanges()),
            'has-errors': ko.computed(() => !!this.data.errors().length)
        };
    }
}
