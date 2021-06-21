import './index.less';
import 'bootstrap-less/js/bootstrap.min.js';
import * as $ from 'jquery';
// import 'jquery-ui-widget';
// import 'jquery-ui';
// import 'jquery-file-upload';
import * as ko from 'knockout';
import * as cmp from './components/index';


import { App } from './app';

$(() => {
    cmp.init();
    ko.applyBindings(App.instance());
});
