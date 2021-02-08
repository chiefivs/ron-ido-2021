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
            data: JSON.stringify(data),
            url: url
        });
    }

    function request<TResponse>(options: JQueryAjaxSettings):any {
        if(Identity.user())
            options.headers = { Authorization: 'Bearer ' + Identity.user().token };

        return <JQueryPromise<TResponse>><any>$.ajax(options);
    }
}