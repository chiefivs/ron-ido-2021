import { MainPageBase } from '../../../modules/content';

export default class PaymentsMainPage extends MainPageBase {

    constructor() {
        super({
            pageTitle: 'реестр платежей',
            templatePath: 'pages/main/applies/payments.html'
        });
    }
}