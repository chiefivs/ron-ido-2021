import './index.less';
import 'bootstrap-less/js/bootstrap.min.js';
import * as $ from 'jquery';
import * as ko from 'knockout';

import App from './app';

$(() => {
    ko.applyBindings(new App());
});
