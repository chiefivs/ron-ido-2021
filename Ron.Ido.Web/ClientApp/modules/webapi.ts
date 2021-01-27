export namespace WebApi {
    export function get<TResponse>(url:string):JQueryPromise<TResponse> {
        return <JQueryPromise<TResponse>><any>$.get(url);
    }

    export function post<TRequest, TResponse>(url: string, data: TRequest) {
        return <JQueryPromise<TResponse>><any>$.ajax({
            method: 'POST',
            contentType: 'application/json',
            url: url,
            data: JSON.stringify(data)
        });
    }
}