import Template from './modules/template';
import { Observable, observable } from 'knockout';
//import * as ko from 'knockout';

export default class App {
    templateNodes: Element[];
    title: Observable<string>;
    
    constructor() {
        //this.templateNodes = Template.getNodes(require('@templates/app.html').default);
        this.templateNodes = Template.getNodes('app.html');
        console.log('sample template nodes', Template.getNodes('samples/sample.html'));
        this.title = observable('main app');

        $.ajax('/api/account')
            .done(res => console.log('done', res))
            .fail(err => console.log('fail', err));
    }
}
