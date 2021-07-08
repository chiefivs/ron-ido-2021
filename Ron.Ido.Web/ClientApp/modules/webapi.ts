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

    export function upload(url: string, file: any):JQueryPromise<any> {

        const formData = new FormData();
        formData.append("file", file);

        const options: JQueryAjaxSettings = {
            type: "POST",
            url: url,
            async: true,
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            //timeout: 60000
        };
    
        if(Identity.user())
            options.headers = { Authorization: 'Bearer ' + Identity.user().token };

        loading.add(options.url);
        return $.ajax(options)
            .always(() => loading.remove(options.url));
    }

    export function download(url: string, file:any): void {
        const options: JQueryAjaxSettings = {
            method: 'GET',
            url: `${url}/${file.uid}`
        };
    
        if(Identity.user())
            options.headers = { Authorization: 'Bearer ' + Identity.user().token };

        console.log(options.url);
        loading.add(options.url);
        $.ajax(options).done(base64 => {
            var bytechars = atob(base64);
            var slicesize = slicesize || 512;
            var bytearrays = [];
            for (var offset = 0; offset < bytechars.length; offset += slicesize) {
                var slice = bytechars.slice(offset, offset + slicesize);
                var bytenums = new Array(slice.length);
                for (var i = 0; i < slice.length; i++) {
                    bytenums[i] = slice.charCodeAt(i);
                }
                var bytearray = new Uint8Array(bytenums);
                bytearrays[bytearrays.length] = bytearray;
            }

            const blob = new Blob(bytearrays, {type:file.contentType});
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = file.name;
            a.click();
            window.URL.revokeObjectURL(url);
            loading.remove(options.url);
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

