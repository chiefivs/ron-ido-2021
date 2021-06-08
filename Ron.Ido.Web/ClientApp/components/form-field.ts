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

    ko.components.register('cmp-form-richtext-field',
    {
        viewModel: {
            createViewModel(params: IFormFieldParams, componentInfo: any) {
                return new FormRichTextModel(params, componentInfo);
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

    ko.components.register('cmp-form-checkbox-field',
    {
        viewModel: {
            createViewModel(params: IFormFieldParams, componentInfo: any) {
                return new FormCheckBoxFieldModel(params, componentInfo);
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
    placeholder?: string;
}

export interface IFormErrorsParams {
    form: Form<any>;
}

class FormFieldModel {
    templateNodes: Node[];
    data: IFormField;
    errors: ko.Computed<string>;
    css: { [key:string]:ko.Computed<boolean> };
    placeholder?:string;

    private _defaultTemplate = '<cmp-textbox params="value:value, css:$parent.css, placeholder:$parent.placeholder"></cmp-textbox>';
    constructor(params: IFormFieldParams, componentInfo: any) {
        this.data = params.field;
        this.placeholder = params.placeholder;
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

class FormCheckBoxFieldModel {
    templateNodes: Node[];
    data: IFormField;
    errors: ko.Computed<string>;
    css: { [key:string]:ko.Computed<boolean> };
    placeholder?:string;

    private _defaultTemplate = '<cmp-checkbox params="value:value, css:$parent.css,placeholder:$parent.placeholder"></cmp-checkbox>';
    constructor(params: IFormFieldParams, componentInfo: any) {
        this.data = params.field;
        this.templateNodes = componentInfo.templateNodes.length
            ? componentInfo.templateNodes
            : Utils.getNodesFromHtml(this._defaultTemplate);
        this.placeholder = params.placeholder;

        this.errors = ko.computed(() => {
            return this.data.errors().join('\n');
        });

        this.css = {
            'has-changes': ko.computed(() => this.data.hasChanges()),
            'has-errors': ko.computed(() => !!this.data.errors().length)
        };
    }
}

class FormRichTextModel {
    templateNodes: Node[];
    data: IFormField;
    errors: ko.Computed<string>;
    css: { [key:string]:ko.Computed<boolean> };
    placeholder?:string;

    private _defaultTemplate = '<cmp-richtext params="value:value, css:$parent.css, placeholder:$parent.placeholder"></cmp-textbox>';
    constructor(params: IFormFieldParams, componentInfo: any) {
        this.data = params.field;
        this.placeholder = params.placeholder;
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
