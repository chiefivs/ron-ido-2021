import './index.less';
import 'bootstrap-less/js/bootstrap.min.js';
import * as $ from 'jquery';
import * as ko from 'knockout';
import * as bindings from './modules/bindings';
import * as cmp from './components/index';


import { App } from './app';

$(() => {
    bindings.init($, ko);
    cmp.init();
    ko.applyBindings(App.instance());
});
