import * as ko from 'knockout';
import { Popups } from '../modules/content';

export function init(){
    ko.components.register('cmp-multiselect', {
        viewModel: {
            createViewModel: function(params:IMultiselectParams, componentInfo) {
                return new MultiselectModel(params);
            }
        },
        template: `
            <div class="input-group">
                <div class="form-control" style="min-height: 2.1em; height: 100%; margin: 1px 0 -1px 0;" data-bind="click:open, foreach:values">
                    <a style="display:block;" data-bind="text:$data[$parent.optionsText], title: $data[$parent.optionsText], click:function(){$parent.remove($data);}"></a>
                </div>
                <a class="input-group-addon glyphicon glyphicon-th-list" data-bind="click:open">
                </a>
            </div>`
    });
}

export interface IMultiselectParams {
    title: string;
    values:ko.ObservableArray<any>;
    options: any[] | ko.ObservableArray<any>;
    optionsText?: string;
}

class MultiselectModel {
    title: string;
    values: ko.ObservableArray<string>;
    options: ko.ObservableArray<any>;
    optionsText: string;

    private _isremove: boolean = false;

    constructor(params: IMultiselectParams) {
        this.title = ko.utils.unwrapObservable(params.title);
        this.optionsText = params.optionsText || 'text';
        this.values = params.values;
        this.options = ko.isObservableArray(params.options) ? params.options : ko.observableArray(params.options || []);
    }

    isSelected(option: any): boolean {
        return ko.utils.arrayIndexOf(this.values(), option) > -1;
    }

    remove(option: any) {
        this.values.remove(option);
        this._isremove = true;
    }

    open() {
        if (this._isremove) {
            this._isremove = false;
            return;
        }

        const dialog = new MultiselectDialog(this);
        dialog.show();
    }
}

interface ILine {
    height: number;
    options: ko.ObservableArray<any>;
}

class MultiselectDialog extends Popups.Dialog {
    model: MultiselectModel;
    lines: ko.Computed<ILine[]>;

    constructor(model: MultiselectModel) {
        super({
            title: model.title,
            width: 800,
            height: 600,
            isModal: true,
            templateHtml: `
                <table>
                    <tbody data-bind="foreach:lines">
                        <tr data-bind="foreach:options">
                            <td style="white-space: nowrap; padding-right: 4px; overflow: hidden;" data-bind="style:{height:$parent.height + 'px'}, if: $data">
                                <input type="checkbox" data-bind="value:$data, checked:$parents[1].model.values" />
                                <span data-bind="text:$data[$parents[1].model.optionsText]"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>`
        });

        this.model = model;

        this.lines = ko.computed(this._createLines.bind(this));
    }

    _createLines(): ILine[] {
        const options = this.model.options();
        const linescnt = Math.min(20, options.length);
        const colscnt = linescnt ? Math.floor(options.length / linescnt) + 1 : 1;
        const size = 12;
        const lheight = size * 2;
        const width = Math.min(600, colscnt * 250);
        const height = linescnt * lheight + 10 * size + 30;

        var lines: ILine[] = [];
        for (var l = 0; l < linescnt; l++) {
            var line: ILine = {
                height: lheight,
                options: ko.observableArray<any>()
            };
            for (var c = 0; c < colscnt; c++) {
                var n = c * linescnt + l;
                line.options.push(n < options.length ? options[n] : null);
            }
            lines.push(line);
        }

        this.width(width);
        this.height(height);

        return lines;
    }
}