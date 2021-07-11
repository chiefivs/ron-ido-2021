import * as ko from 'knockout';
import { Identity } from './identity';

export namespace WebApi {
    export function get<TResponse>(url:string):JQueryPromise<TResponse> {
        return <JQueryPromise<TResponse>><any>request({
            method: 'GET',
            url: url
        });
   }

    export function post<TRequest, TResponse>(url: string, data: TRequest) {
        return <JQueryPromise<TResponse>>request({
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data || {}),
            url: url
        });
    }

    export function del<TResponse>(url:string):JQueryPromise<TResponse> {
        return <JQueryPromise<TResponse>><any>request({
            method: 'DELETE',
            url: url
        });
    }

    function request<TResponse>(options: JQueryAjaxSettings):any {
        if(Identity.user())
            options.headers = { Authorization: 'Bearer ' + Identity.user().token };

        loading.add(options.url);
        return <JQueryPromise<TResponse>><any>$.ajax(options)
            .always(() => loading.remove(options.url));
    }

    class Loading {
        hasProcesses = ko.observable(false);

        private _processes: {[key:string]:number} = {};

        add(key:string) {
            if(!this._processes[key])
                this._processes[key] = 0;

            this._processes[key]++;
            this._update();
        }

        remove(key:string) {
            if(!this._processes[key])
                return;

            this._processes[key]--;

            if(this._processes[key] <= 0)
                delete this._processes[key];

            this._update();
        }

        private _update() {
            this.hasProcesses(!!Object.keys(this._processes).length);
        }
    }

    export const loading = new Loading();
}

