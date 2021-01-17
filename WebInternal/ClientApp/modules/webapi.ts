export default class WebApi {
    static get<TResponse>(url:string):JQueryPromise<TResponse> {
        return <JQueryPromise<TResponse>><any>$.get(url);
    }
}