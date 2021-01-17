import Template from './modules/template';
import WebApi from './modules/webapi';
import { AccountApi } from './codegen/webapi/accountApi';
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

        // $.ajax('/api/WeatherForecast')
        //     .done(res => console.log('done', res))
        //     .fail(err => console.log('fail', err));
        var getperm = AccountApi.getUserPermission('test', 12); //WebApi.get<IWeather>('/api/WeatherForecast');
        console.log(getperm);
        getperm.done(res => console.log('done', res)).fail(err => console.log('fail', err));
        var getuser = AccountApi.getUserInfo();
        console.log(getuser);
        let info:AccountApi.IUserInfo;
        getuser.done(res => console.log('done', res)).fail(err => console.log('fail', err));

    }
}

interface IWeather {
     date:string;
     summary:string;
     temperatureC:number;
     tempetatureF:number;
}